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



public partial class ReportCardLine : ObservableObject
{
    public string Discipline { get; set; } = "";
    public double? TradU1 { get; set; }
    public double? TradU2 { get; set; }
    public double? TradU3 { get; set; }
    public double? TradU4 { get; set; }
    
    public double? TotalU1 { get; set; }
    public double? TotalU2 { get; set; }
    public double? TotalU3 { get; set; }
    
    public string DisplayU1 => TradU1.HasValue ? TradU1.Value.ToString("F1") : (TotalU1.HasValue ? TotalU1.Value.ToString("F1") : "-");
    public string DisplayU2 => TradU2.HasValue ? TradU2.Value.ToString("F1") : (TotalU2.HasValue ? TotalU2.Value.ToString("F1") : "-");
    public string DisplayU3 => TradU3.HasValue ? TradU3.Value.ToString("F1") : (TotalU3.HasValue ? TotalU3.Value.ToString("F1") : "-");
    public string DisplayU4 => TradU4.HasValue ? TradU4.Value.ToString("F1") : "-";

    public double FinalAverage { get; set; }
    public string Status { get; set; } = "";
    public string StatusColor { get; set; } = "";
    public string GradeModel { get; set; } = "continuous";
}



public partial class ClassSelectionViewModel : ObservableObject
{
    public string ClassId { get; set; } = "";
    public string ClassName { get; set; } = "";
    
    [ObservableProperty]
    private bool _isSelected;
}

public partial class SchoolDashboardViewModel : ViewModelBase
{
    
    // --- GERAR ACESSO (FASE 5) ---
    [ObservableProperty] private bool _isAccessModalOpen = false;
    [ObservableProperty] private string _generatedPassword = "";
    [ObservableProperty] private string _generatedLogin = "";
    [ObservableProperty] private Student _accessStudent = new();
    [ObservableProperty] private string _accessErrorMessage = "";

    [RelayCommand]
    private async Task GenerateAccessAsync(Student s)
    {
        AccessStudent = s;
        // Senha aleatoria de 6 caracteres alfanumericos
        var chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789"; // sem o/0, i/1
        var random = new Random();
        var password = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
        AccessErrorMessage = "";
        
        GeneratedPassword = password;
        GeneratedLogin = string.IsNullOrWhiteSpace(s.Registration) ? s.Id.Substring(0,8) : s.Registration;
        
        IsAccessModalOpen = true;
    }

        [RelayCommand]
    private async Task ConfirmGenerateAccessAsync()
    {
        try
        {
            AccessErrorMessage = "Salvando credenciais...";
            string email = $"{GeneratedLogin}@student.educamais.local".ToLower();
            
            try 
            {
                // Tenta criar o usuário novo
                await AppwriteService.Instance.RawCreateUserAsync(email, GeneratedPassword, AccessStudent.Name);
            }
            catch (Exception ex) when (ex.Message.Contains("already exists") || ex.Message.Contains("409"))
            {
                // Se já existe, atualiza a senha!
                await AppwriteService.Instance.RawUpdateUserPasswordByEmailAsync(email, GeneratedPassword);
            }
            
            IsAccessModalOpen = false;
        }
        catch (Exception ex)
        {
            AccessErrorMessage = "Erro: " + ex.Message;
        }
    }
    [RelayCommand]
    private void CloseAccessModal()
    {
        IsAccessModalOpen = false;
    }
public Action? OnLogout { get; set; }
    public Action<Teacher>? OnAccessTeacher { get; set; }
    // --- BOLETIM OFICIAL ---
    [ObservableProperty] private bool _isReportCardModalOpen = false;
    [ObservableProperty] private Student _reportCardStudent = new();
    public ObservableCollection<ReportCardLine> ReportCardLines { get; } = new();

    [RelayCommand]
    private async Task OpenReportCardAsync(Student s)
    {
        ReportCardStudent = s;
        IsReportCardModalOpen = true;
        ReportCardLines.Clear();

        try
        {
            var res = await AppwriteService.Instance.RawListDocumentsAsync("grades", new List<string> {
                $"{{\"method\":\"equal\",\"attribute\":\"student_id\",\"values\":[\"{s.Id}\"]}}"
            });
            var docs = res.GetProperty("documents");
            var grades = new List<Grade>();
            foreach (var doc in docs.EnumerateArray())
            {
                grades.Add(JsonSerializer.Deserialize<Grade>(doc.GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!);
            }

            var disciplines = grades.Select(g => g.Discipline).Distinct().ToList();

            foreach (var disc in disciplines)
            {
                var dGrades = grades.Where(g => g.Discipline == disc).ToList();
                var firstGrade = dGrades.FirstOrDefault();
                if (firstGrade == null) continue;

                var line = new ReportCardLine { Discipline = disc, GradeModel = firstGrade.GradeModel };
                var passingGrade = firstGrade.PassingGrade;

                if (line.GradeModel == "traditional")
                {
                    line.TradU1 = dGrades.FirstOrDefault(g => g.Unit == 1)?.Value;
                    line.TradU2 = dGrades.FirstOrDefault(g => g.Unit == 2)?.Value;
                    line.TradU3 = dGrades.FirstOrDefault(g => g.Unit == 3)?.Value;
                    line.TradU4 = dGrades.FirstOrDefault(g => g.Unit == 4)?.Value;
                    line.FinalAverage = ((line.TradU1 ?? 0) + (line.TradU2 ?? 0) + (line.TradU3 ?? 0) + (line.TradU4 ?? 0)) / 4.0;
                }
                else
                {
                    var g1 = dGrades.FirstOrDefault(g => g.Unit == 1);
                    var g2 = dGrades.FirstOrDefault(g => g.Unit == 2);
                    var g3 = dGrades.FirstOrDefault(g => g.Unit == 3);
                    
                    line.TotalU1 = (g1?.A1 ?? 0) + (g1?.A2 ?? 0) + (g1?.A3 ?? 0) + (g1?.A4 ?? 0);
                    line.TotalU2 = (g2?.A1 ?? 0) + (g2?.A2 ?? 0) + (g2?.A3 ?? 0) + (g2?.A4 ?? 0);
                    line.TotalU3 = (g3?.A1 ?? 0) + (g3?.A2 ?? 0) + (g3?.A3 ?? 0) + (g3?.A4 ?? 0);
                    
                    line.FinalAverage = ((line.TotalU1 ?? 0) + (line.TotalU2 ?? 0) + (line.TotalU3 ?? 0)) / 3.0;
                }

                line.Status = line.FinalAverage >= passingGrade ? "Aprovado" : "Reprovado";
                line.StatusColor = line.FinalAverage >= passingGrade ? "#28a745" : "#dc3545";
                
                ReportCardLines.Add(line);
            }
        }
        catch { }
    }

    [RelayCommand]
    private void CloseReportCard()
    {
        IsReportCardModalOpen = false;
    }


    [ObservableProperty]
    private string _schoolName = "Escola Municipal Exemplo";

    [ObservableProperty]
    private string _schoolId = "";

    // Abas de navegação
    [ObservableProperty] private bool _isOverviewVisible = true;
    [ObservableProperty] private bool _isTeachersVisible = false;
    [ObservableProperty] private bool _isClassesVisible = false;
    [ObservableProperty] private bool _isStudentsVisible = false;

    // Coleções
    public ObservableCollection<Teacher> Teachers { get; } = new();
    public ObservableCollection<SchoolClass> Classes { get; } = new();
    public ObservableCollection<Student> Students { get; } = new();

    // Indicadores Mockados
    [ObservableProperty] private int _totalTeachers = 0;
    [ObservableProperty] private int _totalClasses = 0;
    [ObservableProperty] private int _totalStudents = 0;
    
    // --- TURMAS (CLASSES) ---
    [ObservableProperty] private bool _isClassModalOpen = false;
    [ObservableProperty] private SchoolClass _editingClass = new();
    public ObservableCollection<string> Shifts { get; } = new() { "Matutino", "Vespertino", "Noturno", "Integral" };

    // --- PROFESSORES ---
    [ObservableProperty] private bool _isTeacherModalOpen = false;
    [ObservableProperty] private Teacher _editingTeacher = new();
    [ObservableProperty] private string _teacherSubjectsInput = "";
    public ObservableCollection<ClassSelectionViewModel> TeacherClassesSelection { get; } = new();

    // --- ALUNOS ---
    [ObservableProperty] private bool _isStudentModalOpen = false;
    [ObservableProperty] private Student _editingStudent = new();
    [ObservableProperty] private SchoolClass? _selectedClassFilter;

    // Navegação
    [RelayCommand]
    private void ShowOverview() { IsOverviewVisible = true; IsTeachersVisible = false; IsClassesVisible = false; IsStudentsVisible = false; }
    
    [RelayCommand]
    private void ShowTeachers() { IsOverviewVisible = false; IsTeachersVisible = true; IsClassesVisible = false; IsStudentsVisible = false; LoadTeachersAsync(); }

    [RelayCommand]
    private void ShowClasses() { IsOverviewVisible = false; IsTeachersVisible = false; IsClassesVisible = true; IsStudentsVisible = false; LoadClassesAsync(); }

    [RelayCommand]
    private void ShowStudents() { IsOverviewVisible = false; IsTeachersVisible = false; IsClassesVisible = false; IsStudentsVisible = true; LoadClassesAsync(); LoadStudentsAsync(); }

    [RelayCommand]
    private void Logout()
    {
        OnLogout?.Invoke();
    }

    partial void OnSchoolIdChanged(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            _ = LoadDashboardAsync();
        }
    }

    private async Task LoadDashboardAsync()
    {
        await LoadClassesAsync();
        await LoadTeachersAsync();
        await LoadStudentsAsync();
    }

    // --- LÓGICA TURMAS ---
    private async Task LoadClassesAsync()
    {
        if (string.IsNullOrEmpty(SchoolId)) return;
        try
        {
            var res = await AppwriteService.Instance.RawListDocumentsAsync("classes", new List<string> { $"{{\"method\":\"equal\",\"attribute\":\"school_id\",\"values\":[\"{SchoolId}\"]}}" });
            var docs = res.GetProperty("documents");
            Classes.Clear();
            foreach (var doc in docs.EnumerateArray())
            {
                Classes.Add(JsonSerializer.Deserialize<SchoolClass>(doc.GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!);
            }
            TotalClasses = Classes.Count;
        }
        catch { }
    }

    [RelayCommand] private void AddClass() { EditingClass = new SchoolClass { SchoolId = SchoolId, Shift = "Matutino" }; IsClassModalOpen = true; }
    [RelayCommand] private void EditClass(SchoolClass sc) { EditingClass = new SchoolClass { Id = sc.Id, Name = sc.Name, Shift = sc.Shift, SchoolId = sc.SchoolId }; IsClassModalOpen = true; }
    [RelayCommand] private void CloseClassModal() { IsClassModalOpen = false; }
    [RelayCommand] private async Task SaveClassAsync()
    {
        var data = new { name = EditingClass.Name, shift = EditingClass.Shift, school_id = SchoolId };
        var json = JsonSerializer.Serialize(data);
        if (string.IsNullOrEmpty(EditingClass.Id))
            await AppwriteService.Instance.RawCreateDocumentAsync("classes", data, new List<string> { "read(\"any\")", "update(\"any\")", "delete(\"any\")" });
        else
            await AppwriteService.Instance.RawUpdateDocumentAsync("classes", EditingClass.Id, data);
        
        IsClassModalOpen = false;
        await LoadClassesAsync();
    }
    [RelayCommand] private async Task DeleteClassAsync(SchoolClass sc)
    {
        try {
            await AppwriteService.Instance.RawDeleteDocumentAsync("classes", sc.Id);
            await LoadClassesAsync();
        } catch (System.Exception ex) {
            System.Console.WriteLine("Erro DeleteClass: " + ex.Message);
        }
    }

    // --- LÓGICA PROFESSORES ---
    private async Task LoadTeachersAsync()
    {
        if (string.IsNullOrEmpty(SchoolId)) return;
        try
        {
            var res = await AppwriteService.Instance.RawListDocumentsAsync("teachers", new List<string> { $"{{\"method\":\"equal\",\"attribute\":\"school_id\",\"values\":[\"{SchoolId}\"]}}" });
            var docs = res.GetProperty("documents");
            Teachers.Clear();
            foreach (var doc in docs.EnumerateArray())
            {
                Teachers.Add(JsonSerializer.Deserialize<Teacher>(doc.GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!);
            }
            TotalTeachers = Teachers.Count;
        }
        catch { }
    }


    [RelayCommand]
    private void AccessTeacher(Teacher t)
    {
        OnAccessTeacher?.Invoke(t);
    }
    [RelayCommand] private void AddTeacher() 
    { 
        EditingTeacher = new Teacher { SchoolId = SchoolId }; 
        TeacherSubjectsInput = ""; 
        TeacherClassesSelection.Clear();
        foreach (var c in Classes) TeacherClassesSelection.Add(new ClassSelectionViewModel { ClassId = c.Id, ClassName = c.Name, IsSelected = false });
        IsTeacherModalOpen = true; 
    }
    
    [RelayCommand] private void EditTeacher(Teacher t) 
    { 
        EditingTeacher = new Teacher { Id = t.Id, Name = t.Name, Email = t.Email, Phone = t.Phone, SchoolId = t.SchoolId, Subjects = t.Subjects, ClassIds = t.ClassIds }; 
        TeacherSubjectsInput = string.Join(", ", t.Subjects ?? new List<string>()); 
        
        TeacherClassesSelection.Clear();
        foreach (var c in Classes) 
        {
            TeacherClassesSelection.Add(new ClassSelectionViewModel { 
                ClassId = c.Id, 
                ClassName = c.Name, 
                IsSelected = t.ClassIds != null && t.ClassIds.Contains(c.Id) 
            });
        }
        
        IsTeacherModalOpen = true; 
    }
    [RelayCommand] private void CloseTeacherModal() { IsTeacherModalOpen = false; }
    
    [RelayCommand] private async Task SaveTeacherAsync()
    {
        try {
            var input = TeacherSubjectsInput ?? "";
            var subjectsList = input.Split(',').Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s)).ToList();
            var selectedClassIds = TeacherClassesSelection.Where(x => x.IsSelected).Select(x => x.ClassId).ToList();
            
            var data = new { name = EditingTeacher.Name, email = EditingTeacher.Email, phone = EditingTeacher.Phone, school_id = SchoolId, subjects = subjectsList, class_ids = selectedClassIds };
            
            if (string.IsNullOrEmpty(EditingTeacher.Id))
                await AppwriteService.Instance.RawCreateDocumentAsync("teachers", data, new List<string> { "read(\"any\")", "update(\"any\")", "delete(\"any\")" });
            else
                await AppwriteService.Instance.RawUpdateDocumentAsync("teachers", EditingTeacher.Id, data);
            
            IsTeacherModalOpen = false;
            await LoadTeachersAsync();
        } catch (System.Exception ex) {
            System.Console.WriteLine("Erro SaveTeacherAsync: " + ex.Message);
        }
    }
    
    [RelayCommand] private async Task DeleteTeacherAsync(Teacher t)
    {
        try {
            await AppwriteService.Instance.RawDeleteDocumentAsync("teachers", t.Id);
            await LoadTeachersAsync();
        } catch (System.Exception ex) {
            System.Console.WriteLine("Erro DeleteTeacher: " + ex.Message);
        }
    }

    // --- LÓGICA ALUNOS ---
    partial void OnSelectedClassFilterChanged(SchoolClass? value)
    {
        _ = LoadStudentsAsync();
    }

    private async Task LoadStudentsAsync()
    {
        if (string.IsNullOrEmpty(SchoolId)) return;
        try
        {
            var queries = new List<string> { $"{{\"method\":\"equal\",\"attribute\":\"school_id\",\"values\":[\"{SchoolId}\"]}}" };
            if (SelectedClassFilter != null)
            {
                queries.Add($"{{\"method\":\"equal\",\"attribute\":\"class_id\",\"values\":[\"{SelectedClassFilter.Id}\"]}}");
            }

            var res = await AppwriteService.Instance.RawListDocumentsAsync("students", queries);
            var docs = res.GetProperty("documents");
            Students.Clear();
            foreach (var doc in docs.EnumerateArray())
            {
                Students.Add(JsonSerializer.Deserialize<Student>(doc.GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!);
            }
            if (SelectedClassFilter == null) TotalStudents = Students.Count; // Atualiza indicador geral só quando não filtra
        }
        catch { }
    }

    [RelayCommand] private void AddStudent() { EditingStudent = new Student { SchoolId = SchoolId, ClassId = SelectedClassFilter?.Id ?? "" }; IsStudentModalOpen = true; }
    [RelayCommand] private void EditStudent(Student s) { EditingStudent = new Student { Id = s.Id, Name = s.Name, Registration = s.Registration, ClassId = s.ClassId, SchoolId = s.SchoolId }; IsStudentModalOpen = true; }
    [RelayCommand] private void CloseStudentModal() { IsStudentModalOpen = false; }
    [RelayCommand] private async Task SaveStudentAsync()
    {
        var data = new { name = EditingStudent.Name, registration = EditingStudent.Registration, class_id = EditingStudent.ClassId, school_id = SchoolId };
        var json = JsonSerializer.Serialize(data);
        if (string.IsNullOrEmpty(EditingStudent.Id))
            await AppwriteService.Instance.RawCreateDocumentAsync("students", data, new List<string> { "read(\"any\")", "update(\"any\")", "delete(\"any\")" });
        else
            await AppwriteService.Instance.RawUpdateDocumentAsync("students", EditingStudent.Id, data);
        
        IsStudentModalOpen = false;
        await LoadStudentsAsync();
    }
    [RelayCommand] private async Task DeleteStudentAsync(Student s)
    {
        try {
            await AppwriteService.Instance.RawDeleteDocumentAsync("students", s.Id);
            await LoadStudentsAsync();
        } catch (System.Exception ex) {
            System.Console.WriteLine("Erro DeleteStudent: " + ex.Message);
        }
    }

    [RelayCommand] private async Task ImportCsvAsync()
    {
        try {
            // Mock import (simulate delay)
            await Task.Delay(1500);
            
            // Simular a criação de alunos genéricos
            if (SelectedClassFilter != null)
            {
                var data1 = new { name = "Joãozinho Silva (Importado)", registration = "MAT-" + new System.Random().Next(1000, 9999), class_id = SelectedClassFilter.Id, school_id = SchoolId };
                await AppwriteService.Instance.RawCreateDocumentAsync("students", data1, new List<string> { "read(\"any\")", "update(\"any\")", "delete(\"any\")" });
                var data2 = new { name = "Mariazinha Souza (Importada)", registration = "MAT-" + new System.Random().Next(1000, 9999), class_id = SelectedClassFilter.Id, school_id = SchoolId };
                await AppwriteService.Instance.RawCreateDocumentAsync("students", data2, new List<string> { "read(\"any\")", "update(\"any\")", "delete(\"any\")" });
                
                await LoadStudentsAsync();
            }
        } catch (System.Exception ex) {
            System.Console.WriteLine("Erro ImportCsvAsync: " + ex.Message);
        }
    }

    [RelayCommand]
    private async Task PrintReportCardAsync()
    {
        try
        {
            string desktopDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
            string tempPath = System.IO.Path.Combine(desktopDir, $"Boletim_{ReportCardStudent.Name.Replace(" ", "_")}.pdf");
            
            QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(QuestPDF.Helpers.PageSizes.A4);
                    page.Margin(2, QuestPDF.Infrastructure.Unit.Centimetre);
                    page.PageColor(QuestPDF.Helpers.Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Página ");
                        x.CurrentPageNumber();
                        x.Span(" de ");
                        x.TotalPages();
                    });
                });
            })
            .GeneratePdf(tempPath);

            // Open folder or file
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo { FileName = "explorer.exe", Arguments = $"/select,\"{tempPath}\"", UseShellExecute = true });
            }
            else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
            {
                var psi = new System.Diagnostics.ProcessStartInfo { FileName = "xdg-open", UseShellExecute = false };
                psi.ArgumentList.Add("file://" + tempPath);
                System.Diagnostics.Process.Start(psi);
            }
            else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX))
            {
                var psi = new System.Diagnostics.ProcessStartInfo { FileName = "open", UseShellExecute = false };
                psi.ArgumentList.Add(tempPath);
                System.Diagnostics.Process.Start(psi);
            }
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Erro ao gerar PDF do Boletim: " + ex.Message);
        }
    }

    private void ComposeHeader(QuestPDF.Infrastructure.IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Text("BOLETIM ESCOLAR OFICIAL").FontSize(20).SemiBold().FontColor(QuestPDF.Helpers.Colors.Blue.Darken2);
                column.Item().Text($"Escola: {SchoolName}").FontSize(14);
                column.Item().Text($"Aluno(a): {ReportCardStudent?.Name} (Matrícula: {ReportCardStudent?.Registration})").FontSize(14);
                column.Item().Text($"Emitido em: {System.DateTime.Now:dd/MM/yyyy HH:mm}").FontSize(10).FontColor(QuestPDF.Helpers.Colors.Grey.Medium);
            });
        });
    }

    private void ComposeContent(QuestPDF.Infrastructure.IContainer container)
    {
        container.PaddingVertical(1, QuestPDF.Infrastructure.Unit.Centimetre).Column(column =>
        {
            // Table
            column.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(3);
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(2);
                });

                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).AlignLeft().Text("DISCIPLINA");
                    header.Cell().Element(CellStyle).AlignCenter().Text("UNIDADE 1");
                    header.Cell().Element(CellStyle).AlignCenter().Text("UNIDADE 2");
                    header.Cell().Element(CellStyle).AlignCenter().Text("UNIDADE 3");
                    header.Cell().Element(CellStyle).AlignCenter().Text("UNIDADE 4");
                    header.Cell().Element(CellStyle).AlignCenter().Text("SITUAÇÃO FINAL");

                    QuestPDF.Infrastructure.IContainer CellStyle(QuestPDF.Infrastructure.IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(QuestPDF.Helpers.Colors.Black);
                    }
                });

                foreach (var line in ReportCardLines)
                {
                    table.Cell().Element(CellStyle).AlignLeft().Text(line.Discipline);
                    
                    string u1 = line.TradU1.HasValue ? line.TradU1.Value.ToString("F1") : (line.TotalU1.HasValue ? line.TotalU1.Value.ToString("F1") : "-");
                    string u2 = line.TradU2.HasValue ? line.TradU2.Value.ToString("F1") : (line.TotalU2.HasValue ? line.TotalU2.Value.ToString("F1") : "-");
                    string u3 = line.TradU3.HasValue ? line.TradU3.Value.ToString("F1") : (line.TotalU3.HasValue ? line.TotalU3.Value.ToString("F1") : "-");
                    string u4 = line.TradU4.HasValue ? line.TradU4.Value.ToString("F1") : "-";

                    table.Cell().Element(CellStyle).AlignCenter().Text(u1);
                    table.Cell().Element(CellStyle).AlignCenter().Text(u2);
                    table.Cell().Element(CellStyle).AlignCenter().Text(u3);
                    table.Cell().Element(CellStyle).AlignCenter().Text(u4);

                    var statusCell = table.Cell().Element(CellStyle).AlignCenter();
                    if (line.Status == "Aprovado")
                        statusCell.Text(line.Status).FontColor(QuestPDF.Helpers.Colors.Green.Darken2).SemiBold();
                    else if (line.Status == "Reprovado")
                        statusCell.Text(line.Status).FontColor(QuestPDF.Helpers.Colors.Red.Darken2).SemiBold();
                    else
                        statusCell.Text(line.Status);

                    QuestPDF.Infrastructure.IContainer CellStyle(QuestPDF.Infrastructure.IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(QuestPDF.Helpers.Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });

            // Signatures
            column.Item().PaddingTop(2, QuestPDF.Infrastructure.Unit.Centimetre).Row(row =>
            {
                row.RelativeItem().Column(sigColumn =>
                {
                    sigColumn.Item().AlignCenter().Width(150).BorderBottom(1).BorderColor(QuestPDF.Helpers.Colors.Black);
                    sigColumn.Item().AlignCenter().PaddingTop(5).Text("Assinatura do(a) Coordenador(a)").FontSize(10);
                });

                row.RelativeItem().Column(sigColumn =>
                {
                    sigColumn.Item().AlignCenter().Width(150).BorderBottom(1).BorderColor(QuestPDF.Helpers.Colors.Black);
                    sigColumn.Item().AlignCenter().PaddingTop(5).Text("Assinatura do(a) Diretor(a)").FontSize(10);
                });
            });
        });
    }

}
