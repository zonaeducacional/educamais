using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EducaMais.Models;
using EducaMais.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace EducaMais.ViewModels;

public partial class StudentAttendanceViewModel : ObservableObject
{
    public Student Student { get; set; } = new();
    
    [ObservableProperty]
    private bool _isPresent = true;

    [ObservableProperty]
    private bool _isJustified = false;
    
    [ObservableProperty]
    private string _attendanceId = "";
}

// Suporta tanto o tradicional quanto o contínuo
public partial class StudentGradeViewModel : ObservableObject
{
    public Student Student { get; set; } = new();
    public string GradeModel { get; set; } = "continuous";
    public double PassingGrade { get; set; } = 5.0;

    // Tradicional
    [ObservableProperty] private double? _tradU1;
    [ObservableProperty] private double? _tradU2;
    [ObservableProperty] private double? _tradU3;
    [ObservableProperty] private double? _tradU4;
    public string Grade4Id { get; set; } = "";

    // Contínuo - Unit 1
    [ObservableProperty] private string _grade1Id = "";
    [ObservableProperty] private double? _u1A1;
    [ObservableProperty] private double? _u1A2;
    [ObservableProperty] private double? _u1A3;
    [ObservableProperty] private double? _u1A4;
    public double TotalU1 => (U1A1 ?? 0) + (U1A2 ?? 0) + (U1A3 ?? 0) + (U1A4 ?? 0);
    public string TotalU1Color => TotalU1 > 10.0 ? "Red" : "#28a745";

    // Contínuo - Unit 2
    [ObservableProperty] private string _grade2Id = "";
    [ObservableProperty] private double? _u2A1;
    [ObservableProperty] private double? _u2A2;
    [ObservableProperty] private double? _u2A3;
    [ObservableProperty] private double? _u2A4;
    public double TotalU2 => (U2A1 ?? 0) + (U2A2 ?? 0) + (U2A3 ?? 0) + (U2A4 ?? 0);
    public string TotalU2Color => TotalU2 > 10.0 ? "Red" : "#28a745";

    // Contínuo - Unit 3
    [ObservableProperty] private string _grade3Id = "";
    [ObservableProperty] private double? _u3A1;
    [ObservableProperty] private double? _u3A2;
    [ObservableProperty] private double? _u3A3;
    [ObservableProperty] private double? _u3A4;
    public double TotalU3 => (U3A1 ?? 0) + (U3A2 ?? 0) + (U3A3 ?? 0) + (U3A4 ?? 0);
    public string TotalU3Color => TotalU3 > 10.0 ? "Red" : "#28a745";

    public string DisplayU1 => GradeModel == "traditional" ? (TradU1?.ToString("F1") ?? "-") : (TotalU1 > 0 ? TotalU1.ToString("F1") : "-");
    public string DisplayU2 => GradeModel == "traditional" ? (TradU2?.ToString("F1") ?? "-") : (TotalU2 > 0 ? TotalU2.ToString("F1") : "-");
    public string DisplayU3 => GradeModel == "traditional" ? (TradU3?.ToString("F1") ?? "-") : (TotalU3 > 0 ? TotalU3.ToString("F1") : "-");
    public string DisplayU4 => GradeModel == "traditional" ? (TradU4?.ToString("F1") ?? "-") : "-";

    // Média Final e Status
    public double FinalAverage => GradeModel == "traditional" 
        ? ((TradU1 ?? 0) + (TradU2 ?? 0) + (TradU3 ?? 0) + (TradU4 ?? 0)) / 4.0 
        : (TotalU1 + TotalU2 + TotalU3) / 3.0;

    public string Status => FinalAverage >= PassingGrade ? "Aprovado" : "Reprovado"; 
    public string StatusColor => FinalAverage >= PassingGrade ? "#28a745" : "#dc3545";

    public void RefreshStatus()
    {
        OnPropertyChanged(nameof(Status));
        OnPropertyChanged(nameof(StatusColor));
    }

    protected override void OnPropertyChanged(System.ComponentModel.PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
                // Prevenindo StackOverflowException
        if (e.PropertyName == nameof(TotalU1) || e.PropertyName == nameof(TotalU2) || e.PropertyName == nameof(TotalU3) ||
            e.PropertyName == nameof(TotalU1Color) || e.PropertyName == nameof(TotalU2Color) || e.PropertyName == nameof(TotalU3Color) ||
            e.PropertyName == nameof(FinalAverage) || e.PropertyName == nameof(Status) || e.PropertyName == nameof(StatusColor))
        {
            return;
        }

        OnPropertyChanged(nameof(TotalU1));
        OnPropertyChanged(nameof(TotalU1Color));
        OnPropertyChanged(nameof(TotalU2));
        OnPropertyChanged(nameof(TotalU2Color));
        OnPropertyChanged(nameof(TotalU3));
        OnPropertyChanged(nameof(TotalU3Color));
        OnPropertyChanged(nameof(FinalAverage));
        OnPropertyChanged(nameof(Status));
        OnPropertyChanged(nameof(StatusColor));
    }
}

public partial class TeacherDashboardViewModel : ViewModelBase
{
        private List<string> _teacherClassIds = new();
public Action? OnLogout { get; set; }

    [ObservableProperty]
    private string _syncMessage = "Verificando rede...";

    [ObservableProperty]
    private string _networkStatusColor = "#6c757d"; // Gray by default

    private Avalonia.Threading.DispatcherTimer? _networkTimer;

    private void StartNetworkWatcher()
    {
        _networkTimer = new Avalonia.Threading.DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
        _networkTimer.Tick += async (s, e) => await CheckNetworkAndSyncAsync();
        _networkTimer.Start();
        _ = CheckNetworkAndSyncAsync();
    }

    private async Task CheckNetworkAndSyncAsync()
    {
        try 
        {
            using var client = new System.Net.Http.HttpClient { Timeout = TimeSpan.FromSeconds(3) };
            var ping = await client.GetAsync($"{AppwriteService.Endpoint}/health");
            if (ping.IsSuccessStatusCode || ping.StatusCode == System.Net.HttpStatusCode.Unauthorized || ping.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                NetworkStatusColor = "#d4edda"; // Light green background
                
                var db = LocalDbService.Instance.GetConnection();
                var pendAtt = await db.Table<Attendance>().Where(a => a.SyncStatus == "pending").CountAsync();
                var pendGr = await db.Table<Grade>().Where(g => g.SyncStatus == "pending").CountAsync();
                
                if (pendAtt > 0 || pendGr > 0)
                {
                    SyncMessage = "🟡 Sincronizando dados...";
                    await SyncService.Instance.SyncPendingDataAsync();
                    SyncMessage = "🟢 Online (Sincronizado)";
                }
                else
                {
                    SyncMessage = "🟢 Online";
                }
            }
            else
            {
                NetworkStatusColor = "#f8d7da"; // Light red
                SyncMessage = "🔴 Offline";
            }
        }
        catch
        {
            NetworkStatusColor = "#f8d7da";
            SyncMessage = "🔴 Offline";
        }
    }


    


    [ObservableProperty] private string _teacherName = "Professor(a)";
    [ObservableProperty] private string _teacherId = "";
    [ObservableProperty] private string _schoolId = "";

    // Filtros Globais
    public ObservableCollection<SchoolClass> Classes { get; } = new();
    public ObservableCollection<string> Disciplines { get; } = new();

    [ObservableProperty] private SchoolClass? _selectedClass;
    [ObservableProperty] private string? _selectedDiscipline;

    // Abas de navegação
    [ObservableProperty] private bool _isAttendanceVisible = true;
    [ObservableProperty] private bool _isGradesVisible = false;
    [ObservableProperty] private bool _isLessonPlansVisible = false;

    // --- FREQUÊNCIA ---
    [ObservableProperty] private DateTimeOffset _attendanceDate = DateTimeOffset.Now;
    public ObservableCollection<StudentAttendanceViewModel> AttendanceList { get; } = new();
    [ObservableProperty] private bool _isSavingAttendance = false;
    
    // Novo: Assunto da Aula e Importação
    [ObservableProperty] private string _currentLessonTopic = "";
    public ObservableCollection<string> ImportedTopics { get; } = new();
    [ObservableProperty] private string? _selectedImportedTopic;

    // --- NOTAS ---
    public ObservableCollection<StudentGradeViewModel> GradeList { get; } = new();
    [ObservableProperty] private string _gradesErrorMessage = "";
    [ObservableProperty] private bool _isSavingGrades = false;
    
    // Modelos de Planilha
    public ObservableCollection<string> GradeModels { get; } = new() { "Tradicional (4 Bimestres)", "Avaliação Contínua (3 Unidades)" };
    [ObservableProperty] private string _selectedGradeModel = "Avaliação Contínua (3 Unidades)";
    
    public bool IsTraditionalGradesVisible => SelectedGradeModel == "Tradicional (4 Bimestres)";
    public bool IsContinuousGradesVisible => SelectedGradeModel == "Avaliação Contínua (3 Unidades)";

    // Novo: Média de Aprovação Customizável
    public ObservableCollection<string> PassingGradeOptions { get; } = new() { "5.0", "7.0", "Outra" };
    [ObservableProperty] private string _selectedPassingGradeOption = "5.0";
    [ObservableProperty] private string _customPassingGrade = "6.0";
    [ObservableProperty] private bool _isCustomPassingGradeVisible = false;
    [ObservableProperty] private double _currentPassingGradeValue = 5.0;

    // --- PLANEJAMENTO ---
    public ObservableCollection<LessonPlan> LessonPlans { get; } = new();
    [ObservableProperty] private bool _isLessonPlanModalOpen = false;
    [ObservableProperty] private LessonPlan _editingLessonPlan = new();
    [ObservableProperty] private DateTimeOffset _lessonPlanDate = DateTimeOffset.Now;
    
    // Novo: Tipo de Plano e Anexo
    public ObservableCollection<string> PlanTypes { get; } = new() { "Plano de Aula", "Plano de Unidade", "Plano de Curso" };

    [RelayCommand] private void ShowAttendance() { IsAttendanceVisible = true; IsGradesVisible = false; IsLessonPlansVisible = false; _ = LoadAttendanceAsync(); }
    [RelayCommand] private void ShowGrades() { IsAttendanceVisible = false; IsGradesVisible = true; IsLessonPlansVisible = false; _ = LoadGradesAsync(); }
    [RelayCommand] private void ShowLessonPlans() { IsAttendanceVisible = false; IsGradesVisible = false; IsLessonPlansVisible = true; _ = LoadLessonPlansAsync(); }
    
    [RelayCommand] private void Logout() { OnLogout?.Invoke(); }
    
    public async Task InitializeAsync(Teacher teacher)
    {
        TeacherId = teacher.Id;
        TeacherName = teacher.Name;
        SchoolId = teacher.SchoolId;
        if (teacher.ClassIds != null) _teacherClassIds = teacher.ClassIds;
        
        Disciplines.Clear();
        if (teacher.Subjects != null)
            foreach (var s in teacher.Subjects) Disciplines.Add(s);
            
        await LoadClassesAsync();
        
        if (Classes.Count > 0) SelectedClass = Classes[0];
        if (Disciplines.Count > 0) SelectedDiscipline = Disciplines[0];
        
        StartNetworkWatcher();
    }

    partial void OnSelectedClassChanged(SchoolClass? value) { _ = LoadActiveTabAsync(); }
    partial void OnSelectedDisciplineChanged(string? value) { _ = LoadActiveTabAsync(); }
    partial void OnAttendanceDateChanged(DateTimeOffset value) { if (IsAttendanceVisible) _ = LoadAttendanceAsync(); }
    
    partial void OnSelectedImportedTopicChanged(string? value)
    {
        if (!string.IsNullOrEmpty(value)) CurrentLessonTopic = value;
    }

    partial void OnSelectedPassingGradeOptionChanged(string value)
    {
        IsCustomPassingGradeVisible = (value == "Outra");
        UpdatePassingGradeValue();
    }
    
    partial void OnCustomPassingGradeChanged(string value)
    {
        UpdatePassingGradeValue();
    }

    partial void OnSelectedGradeModelChanged(string value)
    {
        OnPropertyChanged(nameof(IsTraditionalGradesVisible));
        OnPropertyChanged(nameof(IsContinuousGradesVisible));
        
        var internalModel = value == "Tradicional (4 Bimestres)" ? "traditional" : "continuous";
        foreach (var vm in GradeList)
        {
            vm.GradeModel = internalModel;
            vm.RefreshStatus();
        }
    }
    private void UpdatePassingGradeValue()
    {
        if (SelectedPassingGradeOption == "5.0") CurrentPassingGradeValue = 5.0;
        else if (SelectedPassingGradeOption == "7.0") CurrentPassingGradeValue = 7.0;
        else if (double.TryParse(CustomPassingGrade.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double val)) 
            CurrentPassingGradeValue = val;
            
        // Atualizar VMs existentes
        foreach (var vm in GradeList)
        {
            vm.PassingGrade = CurrentPassingGradeValue;
            vm.RefreshStatus();
            
        }
    }

    private async Task LoadActiveTabAsync()
    {
        if (IsAttendanceVisible) await LoadAttendanceAsync();
        else if (IsGradesVisible) await LoadGradesAsync();
        else if (IsLessonPlansVisible) await LoadLessonPlansAsync();
    }
    
    
    private async Task LoadClassesAsync()
    {
        Classes.Clear();
        try
        {
            var db = LocalDbService.Instance.GetConnection();
            var localClasses = await db.Table<SchoolClass>().Where(c => c.SchoolId == SchoolId).ToListAsync();
            foreach (var c in localClasses)
            {
                if (_teacherClassIds.Contains(c.Id))
                    Classes.Add(c);
            }
        }
        catch { }
    }

    private async Task<List<Student>> GetStudentsAsync()
    {
        if (SelectedClass == null) return new List<Student>();
        var queries = new List<string> { $"{{\"method\":\"equal\",\"attribute\":\"class_id\",\"values\":[\"{SelectedClass.Id}\"]}}" };
        var res = await AppwriteService.Instance.RawListDocumentsAsync("students", queries);
        var docs = res.GetProperty("documents");
        var students = new List<Student>();
        foreach (var doc in docs.EnumerateArray())
            students.Add(JsonSerializer.Deserialize<Student>(doc.GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!);
        return students.OrderBy(s => s.Name).ToList();
    }

    [RelayCommand]
    private async Task ImportCsvTopicsAsync()
    {
        // Mock de importação de CSV (Simula abrir um arquivo e carregar os temas)
        await Task.Delay(1000);
        ImportedTopics.Clear();
        ImportedTopics.Add("Introdução à Matéria");
        ImportedTopics.Add("Conceitos Fundamentais - Parte 1");
        ImportedTopics.Add("Revisão Geral");
        ImportedTopics.Add("Atividade Prática no Laboratório");
    }

    private async Task LoadAttendanceAsync()
    {
        if (SelectedClass == null || string.IsNullOrEmpty(SelectedDiscipline)) { AttendanceList.Clear(); return; }
        var students = await GetStudentsAsync();
        var dateStr = AttendanceDate.ToString("yyyy-MM-dd");
        var queries = new List<string> { 
            $"{{\"method\":\"equal\",\"attribute\":\"class_id\",\"values\":[\"{SelectedClass.Id}\"]}}",
            $"{{\"method\":\"equal\",\"attribute\":\"discipline\",\"values\":[\"{SelectedDiscipline}\"]}}",
            $"{{\"method\":\"startsWith\",\"attribute\":\"date\",\"values\":[\"{dateStr}\"]}}"
        };
        var res = await AppwriteService.Instance.RawListDocumentsAsync("attendances", queries);
        var docs = res.GetProperty("documents");
        
        AttendanceList.Clear();
        CurrentLessonTopic = ""; // Reseta

        foreach (var s in students)
        {
            var att = docs.EnumerateArray().FirstOrDefault(d => d.GetProperty("student_id").GetString() == s.Id);
            if (att.ValueKind != JsonValueKind.Undefined)
            {
                var isPresent = att.GetProperty("is_present").GetBoolean();
                var isJustified = att.TryGetProperty("is_justified", out var jProp) && jProp.ValueKind != JsonValueKind.Null ? jProp.GetBoolean() : false;
                if (string.IsNullOrEmpty(CurrentLessonTopic) && att.TryGetProperty("lesson_topic", out var tProp) && tProp.ValueKind != JsonValueKind.Null)
                    CurrentLessonTopic = tProp.GetString() ?? "";

                AttendanceList.Add(new StudentAttendanceViewModel { 
                    Student = s, 
                    AttendanceId = att.GetProperty("$id").GetString()!,
                    IsPresent = isPresent,
                    IsJustified = isJustified
                });
            }
            else
            {
                AttendanceList.Add(new StudentAttendanceViewModel { Student = s, IsPresent = true }); 
            }
        }
    }

    [RelayCommand]
    private async Task SaveAttendanceBatchAsync()
    {
        if (SelectedClass == null || string.IsNullOrEmpty(SelectedDiscipline)) return;
        IsSavingAttendance = true;
        var dateStr = AttendanceDate.ToString("o"); 
        foreach (var vm in AttendanceList)
        {
            var data = new {
                student_id = vm.Student.Id, class_id = SelectedClass.Id, teacher_id = TeacherId,
                discipline = SelectedDiscipline, date = dateStr, is_present = vm.IsPresent,
                is_justified = vm.IsJustified, lesson_topic = CurrentLessonTopic
            };
            try {
                if (string.IsNullOrEmpty(vm.AttendanceId))
                    await AppwriteService.Instance.RawCreateDocumentAsync("attendances", data, new List<string> { "read(\"any\")", "update(\"any\")", "delete(\"any\")" });
                else
                    await AppwriteService.Instance.RawUpdateDocumentAsync("attendances", vm.AttendanceId, data);
            } catch { }
        }
        await LoadAttendanceAsync();
        IsSavingAttendance = false;
    }

    private async Task LoadGradesAsync()
    {
        if (SelectedClass == null || string.IsNullOrEmpty(SelectedDiscipline)) { GradeList.Clear(); return; }
        var students = await GetStudentsAsync();
        var queries = new List<string> { 
            $"{{\"method\":\"equal\",\"attribute\":\"class_id\",\"values\":[\"{SelectedClass.Id}\"]}}",
            $"{{\"method\":\"equal\",\"attribute\":\"discipline\",\"values\":[\"{SelectedDiscipline}\"]}}"
        };
        var res = await AppwriteService.Instance.RawListDocumentsAsync("grades", queries);
        var docs = res.GetProperty("documents");
        GradesErrorMessage = "";
        var grades = new List<Grade>();
        foreach (var doc in docs.EnumerateArray())
            grades.Add(JsonSerializer.Deserialize<Grade>(doc.GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!);

        GradeList.Clear();
        foreach (var s in students)
        {
            var sGrades = grades.Where(g => g.StudentId == s.Id).ToList();
            var vm = new StudentGradeViewModel { Student = s, PassingGrade = CurrentPassingGradeValue };
            
            var g1 = sGrades.FirstOrDefault(g => g.Unit == 1);
            if (g1 != null) { vm.Grade1Id = g1.Id; vm.U1A1 = g1.A1; vm.U1A2 = g1.A2; vm.U1A3 = g1.A3; vm.U1A4 = g1.A4; vm.TradU1 = g1.Value; }
            var g2 = sGrades.FirstOrDefault(g => g.Unit == 2);
            if (g2 != null) { vm.Grade2Id = g2.Id; vm.U2A1 = g2.A1; vm.U2A2 = g2.A2; vm.U2A3 = g2.A3; vm.U2A4 = g2.A4; vm.TradU2 = g2.Value; }
            var g3 = sGrades.FirstOrDefault(g => g.Unit == 3);
            if (g3 != null) { vm.Grade3Id = g3.Id; vm.U3A1 = g3.A1; vm.U3A2 = g3.A2; vm.U3A3 = g3.A3; vm.U3A4 = g3.A4; vm.TradU3 = g3.Value; }
            var g4 = sGrades.FirstOrDefault(g => g.Unit == 4);
            if (g4 != null) { vm.TradU4 = g4.Value; vm.Grade4Id = g4.Id; } // Somente tradicional tem Unidade 4

            var firstModel = sGrades.FirstOrDefault()?.GradeModel;
            if (!string.IsNullOrEmpty(firstModel)) {
                SelectedGradeModel = firstModel == "traditional" ? "Tradicional (4 Bimestres)" : "Avaliação Contínua (3 Unidades)";
                vm.GradeModel = firstModel;
            } else {
                vm.GradeModel = SelectedGradeModel == "Tradicional (4 Bimestres)" ? "traditional" : "continuous";
            }
            
            GradeList.Add(vm);
        }
    }

    [RelayCommand]
    private async Task SaveGradesBatchAsync()
    {
        if (SelectedClass == null || string.IsNullOrEmpty(SelectedDiscipline)) return;
        
        GradesErrorMessage = "";
        if (GradeList.Any(g => g.TotalU1 > 10.0 || g.TotalU2 > 10.0 || g.TotalU3 > 10.0))
        {
            GradesErrorMessage = "Existem alunos com soma de notas acima de 10.0! Corrija as notas em vermelho antes de salvar.";
            return;
        }

        IsSavingGrades = true;
        foreach (var vm in GradeList)
        {
            var internalModel = SelectedGradeModel == "Tradicional (4 Bimestres)" ? "traditional" : "continuous";
            if (internalModel == "traditional")
            {
                await SaveSingleGradeAsync(vm.Grade1Id, vm.Student.Id, 1, vm.TradU1 ?? 0, null, null, null, null, internalModel);
                await SaveSingleGradeAsync(vm.Grade2Id, vm.Student.Id, 2, vm.TradU2 ?? 0, null, null, null, null, internalModel);
                await SaveSingleGradeAsync(vm.Grade3Id, vm.Student.Id, 3, vm.TradU3 ?? 0, null, null, null, null, internalModel);
                // We don't have Grade4Id stored, we can search by student/unit, but for MVP let's assume we create it if missing
                var g4Id = GradeList.Where(g => g.Student.Id == vm.Student.Id).FirstOrDefault()?.Grade1Id + "_4"; // Hacky, proper way is querying, but for now we skip storing ID for 4th if it wasn't loaded. Actually, let's just use raw create if ID not mapped.
                // Wait, if it wasn't loaded, it won't have an ID, we'll just create. It might duplicate. 
                // Better approach: I'll fix this in SaveSingleGradeAsync!
                await SaveSingleGradeAsync(vm.Grade4Id, vm.Student.Id, 4, vm.TradU4 ?? 0, null, null, null, null, internalModel);
            }
            else
            {
                await SaveSingleGradeAsync(vm.Grade1Id, vm.Student.Id, 1, vm.TotalU1, vm.U1A1, vm.U1A2, vm.U1A3, vm.U1A4, internalModel);
                await SaveSingleGradeAsync(vm.Grade2Id, vm.Student.Id, 2, vm.TotalU2, vm.U2A1, vm.U2A2, vm.U2A3, vm.U2A4, internalModel);
                await SaveSingleGradeAsync(vm.Grade3Id, vm.Student.Id, 3, vm.TotalU3, vm.U3A1, vm.U3A2, vm.U3A3, vm.U3A4, internalModel);
            }
        }
        await LoadGradesAsync();
        IsSavingGrades = false;
    }

    private async Task SaveSingleGradeAsync(string id, string studentId, int unit, double total, double? a1, double? a2, double? a3, double? a4, string gradeModel)
    {
        var data = new {
            student_id = studentId, class_id = SelectedClass!.Id, teacher_id = TeacherId,
            discipline = SelectedDiscipline, unit = unit, value = total, a1 = a1, a2 = a2, a3 = a3, a4 = a4,
            passing_grade = CurrentPassingGradeValue, grade_model = gradeModel
        };
        try {
            if (string.IsNullOrEmpty(id))
                await AppwriteService.Instance.RawCreateDocumentAsync("grades", data, new List<string> { "read(\"any\")", "update(\"any\")", "delete(\"any\")" });
            else
                await AppwriteService.Instance.RawUpdateDocumentAsync("grades", id, data);
        } catch { }
    }
    
    private async Task LoadLessonPlansAsync()
    {
        if (SelectedClass == null || string.IsNullOrEmpty(SelectedDiscipline)) { LessonPlans.Clear(); return; }
        try
        {
            var queries = new List<string> { 
                $"{{\"method\":\"equal\",\"attribute\":\"class_id\",\"values\":[\"{SelectedClass.Id}\"]}}",
                $"{{\"method\":\"equal\",\"attribute\":\"discipline\",\"values\":[\"{SelectedDiscipline}\"]}}"
            };
            var res = await AppwriteService.Instance.RawListDocumentsAsync("lesson_plans", queries);
            var docs = res.GetProperty("documents");
            LessonPlans.Clear();
            foreach (var doc in docs.EnumerateArray())
                LessonPlans.Add(JsonSerializer.Deserialize<LessonPlan>(doc.GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!);
        }
        catch { }
    }

    [RelayCommand] private void AddLessonPlan() { EditingLessonPlan = new LessonPlan { ClassId = SelectedClass?.Id ?? "", Discipline = SelectedDiscipline ?? "", TeacherId = TeacherId, PlanType = "Plano de Aula" }; LessonPlanDate = DateTimeOffset.Now; IsLessonPlanModalOpen = true; }
    [RelayCommand] private void EditLessonPlan(LessonPlan p) { EditingLessonPlan = new LessonPlan { Id = p.Id, ClassId = p.ClassId, Discipline = p.Discipline, TeacherId = p.TeacherId, Title = p.Title, Content = p.Content, Date = p.Date, PlanType = p.PlanType, AttachmentUrl = p.AttachmentUrl }; LessonPlanDate = DateTimeOffset.Parse(p.Date); IsLessonPlanModalOpen = true; }
    [RelayCommand] private void CloseLessonPlanModal() { IsLessonPlanModalOpen = false; }
    
    [RelayCommand] private async Task SimulateAttachmentAsync()
    {
        // Simulando o upload de um PDF/DOCX
        await Task.Delay(500);
        // Criando uma nova instância para forçar a atualização visual rápida
        var updated = new LessonPlan { 
            Id = EditingLessonPlan.Id, ClassId = EditingLessonPlan.ClassId, Discipline = EditingLessonPlan.Discipline, 
            TeacherId = EditingLessonPlan.TeacherId, Title = EditingLessonPlan.Title, Content = EditingLessonPlan.Content, 
            Date = EditingLessonPlan.Date, PlanType = EditingLessonPlan.PlanType,
            AttachmentUrl = "https://meu-storage-appwrite.com/plano_de_aula_anexo.pdf"
        };
        EditingLessonPlan = updated;
    }

    [RelayCommand] private async Task SaveLessonPlanAsync()
    {
        try {
            var data = new { 
                title = EditingLessonPlan.Title, content = EditingLessonPlan.Content, 
                class_id = SelectedClass!.Id, teacher_id = TeacherId, 
                discipline = SelectedDiscipline, date = LessonPlanDate.ToString("o"),
                plan_type = EditingLessonPlan.PlanType, attachment_url = EditingLessonPlan.AttachmentUrl
            };
            if (string.IsNullOrEmpty(EditingLessonPlan.Id))
                await AppwriteService.Instance.RawCreateDocumentAsync("lesson_plans", data, new List<string> { "read(\"any\")", "update(\"any\")", "delete(\"any\")" });
            else
                await AppwriteService.Instance.RawUpdateDocumentAsync("lesson_plans", EditingLessonPlan.Id, data);
            
            IsLessonPlanModalOpen = false;
            await LoadLessonPlansAsync();
        } catch { }
    }
    [RelayCommand] private async Task DeleteLessonPlanAsync(LessonPlan p)
    {
        try {
            await AppwriteService.Instance.RawDeleteDocumentAsync("lesson_plans", p.Id);
            await LoadLessonPlansAsync();
        } catch { }
    }
}
