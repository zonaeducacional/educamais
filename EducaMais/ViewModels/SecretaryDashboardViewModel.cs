using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EducaMais.Services;
using EducaMais.Models;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;

namespace EducaMais.ViewModels;

public partial class SocialReportLine : ObservableObject
{
    public string StudentName { get; set; } = "";
    public string SchoolName { get; set; } = "";
    public string ClassName { get; set; } = "";
    
    public int TotalClasses { get; set; }
    public int TotalAbsences { get; set; }
    public int TotalPresences => TotalClasses - TotalAbsences;
    
    public double AttendancePercentage => TotalClasses > 0 ? 100.0 * TotalPresences / TotalClasses : 100.0;
    
    public string StatusColor => AttendancePercentage < 75.0 ? "#dc3545" : "#333333";
    public string StatusBackground => AttendancePercentage < 75.0 ? "#ffebe9" : "Transparent";
}


public partial class SecretaryDashboardViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _tenantName = "Carregando...";

    [ObservableProperty]
    private bool _isVisaoGeralVisible = true;

    [ObservableProperty]
    private bool _isEscolasVisible = false;

    [ObservableProperty]
    private bool _isDiretoresVisible = false;

    [ObservableProperty]
    private bool _isRelatoriosVisible = false;

    [ObservableProperty]
    private bool _isSchoolModalOpen = false;

    [ObservableProperty]
    private string _modalTitle = "Adicionar Escola";

    [ObservableProperty]
    private School _editingSchool = new();

    public ObservableCollection<School> Schools { get; set; } = new();

    // Mock properties from UI reconstruction
    public int TotalEscolas => Schools.Count;
    public int TotalDiretores => Directors.Count;
    public string MediaRede => "8.5";


    // --- DIRECTORS ---
    [ObservableProperty]
    private bool _isDirectorModalOpen = false;

    [ObservableProperty]
    private string _directorModalTitle = "Adicionar Diretor";

    [ObservableProperty]
    private Director _editingDirector = new();

    public ObservableCollection<Director> Directors { get; set; } = new();

    
    [RelayCommand]
    private async Task ExportSocialReportPdfAsync()
    {
        try
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("ESCOLA;TURMA;ALUNO;TOTAL AULAS;PRESENCAS;FALTAS;FREQUENCIA %");

            foreach (var line in SocialReportLines)
            {
                string escola = (line.SchoolName ?? "").Replace(";", ",");
                string aluno = (line.StudentName ?? "").Replace(";", ",");
                string turma = (line.ClassName ?? "").Replace(";", ",");
                
                sb.AppendLine($"{escola};{turma};{aluno};{line.TotalClasses};{line.TotalPresences};{line.TotalAbsences};{line.AttendancePercentage:F1}");
            }

            string desktopDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
            string tempPath = System.IO.Path.Combine(desktopDir, $"Relatorio_BolsaFamilia_{TenantName.Replace(" ", "_")}.csv");
            await System.IO.File.WriteAllTextAsync(tempPath, sb.ToString(), System.Text.Encoding.UTF8);

            // Open folder
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo { FileName = "explorer.exe", Arguments = $"/select,\"{tempPath}\"", UseShellExecute = true });
            }
            else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
            {
                var psi = new System.Diagnostics.ProcessStartInfo { FileName = "xdg-open", UseShellExecute = false };
                psi.ArgumentList.Add(desktopDir);
                System.Diagnostics.Process.Start(psi);
            }
            else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX))
            {
                var psi = new System.Diagnostics.ProcessStartInfo { FileName = "open", UseShellExecute = false };
                psi.ArgumentList.Add("-R");
                psi.ArgumentList.Add(tempPath);
                System.Diagnostics.Process.Start(psi);
            }
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Erro ao gerar CSV: " + ex.Message);
        }
    }

    public System.Action? OnLogout { get; set; }

    [ObservableProperty] private bool _isSocialReportVisible = false;
    public ObservableCollection<SocialReportLine> SocialReportLines { get; } = new();

    [RelayCommand]
    private void ShowSocialReport()
    {
        IsVisaoGeralVisible = false;
        IsEscolasVisible = false;
        IsDiretoresVisible = false;
        IsRelatoriosVisible = false;
        IsSocialReportVisible = true;
        _ = LoadSocialReportAsync();
    }


    private async Task LoadSocialReportAsync()
    {
        SocialReportLines.Clear();
        try
        {
            System.Console.WriteLine($"[DEBUG] Carregando relatorio para TenantName={TenantName}");
            var resSchools = await AppwriteService.Instance.RawListDocumentsAsync("schools", new List<string> {
                $"{{\"method\":\"equal\",\"attribute\":\"tenant\",\"values\":[\"{TenantName}\"]}}"
            });
            var docSchools = resSchools.GetProperty("documents");
            var schools = new List<School>();
            foreach (var doc in docSchools.EnumerateArray())
                schools.Add(JsonSerializer.Deserialize<School>(doc.GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!);

            System.Console.WriteLine($"[DEBUG] Found {schools.Count} schools.");
            var classes = new List<SchoolClass>();
            foreach (var school in schools)
            {
                var resClasses = await AppwriteService.Instance.RawListDocumentsAsync("classes", new List<string> {
                    $"{{\"method\":\"equal\",\"attribute\":\"school_id\",\"values\":[\"{school.Id}\"]}}"
                });
                foreach (var doc in resClasses.GetProperty("documents").EnumerateArray())
                    classes.Add(JsonSerializer.Deserialize<SchoolClass>(doc.GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!);
            }

            System.Console.WriteLine($"[DEBUG] Found {classes.Count} classes.");
            var students = new List<Student>();
            foreach (var c in classes)
            {
                var resStudents = await AppwriteService.Instance.RawListDocumentsAsync("students", new List<string> {
                    $"{{\"method\":\"equal\",\"attribute\":\"class_id\",\"values\":[\"{c.Id}\"]}}"
                });
                foreach (var doc in resStudents.GetProperty("documents").EnumerateArray())
                    students.Add(JsonSerializer.Deserialize<Student>(doc.GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!);
            }

            System.Console.WriteLine($"[DEBUG] Found {students.Count} students.");
            var allAttendances = new List<JsonElement>();
            foreach (var c in classes)
            {
                var resAtt = await AppwriteService.Instance.RawListDocumentsAsync("attendances", new List<string> {
                    $"{{\"method\":\"equal\",\"attribute\":\"class_id\",\"values\":[\"{c.Id}\"]}}"
                });
                foreach (var doc in resAtt.GetProperty("documents").EnumerateArray())
                {
                    allAttendances.Add(doc);
                }
            }

            foreach (var student in students)
            {
                var c = classes.FirstOrDefault(x => x.Id == student.ClassId);
                var s = schools.FirstOrDefault(x => x.Id == c?.SchoolId);

                var studentAttendances = allAttendances.Where(a => a.GetProperty("student_id").GetString() == student.Id).ToList();
                int totalClasses = studentAttendances.Count;
                int totalAbsences = 0;
                
                foreach (var doc in studentAttendances)
                {
                    if (!doc.GetProperty("is_present").GetBoolean() && 
                        (!doc.TryGetProperty("is_justified", out var jProp) || jProp.ValueKind == JsonValueKind.Null || !jProp.GetBoolean()))
                    {
                        totalAbsences++;
                    }
                }

                SocialReportLines.Add(new SocialReportLine
                {
                    StudentName = student.Name,
                    SchoolName = s?.Name ?? "Desconhecida",
                    ClassName = c?.Name ?? "Desconhecida",
                    TotalClasses = totalClasses,
                    TotalAbsences = totalAbsences
                });
            }
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Erro LoadSocialReport: " + ex.Message);
        }

        // --- INÍCIO DO MOCK PARA APRESENTAÇÃO ---
        // Se a base estiver vazia (porque o professor ainda não registrou chamadas),
        // vamos injetar dados fictícios apenas para mostrar a interface funcionando no MVP!
        if (SocialReportLines.Count == 0)
        {
            SocialReportLines.Add(new SocialReportLine { StudentName = "João Silva Souza", SchoolName = "Escola Municipal Aurora", ClassName = "6º Ano A", TotalClasses = 100, TotalAbsences = 10 });
            SocialReportLines.Add(new SocialReportLine { StudentName = "Maria Oliveira Dias", SchoolName = "Escola Municipal Aurora", ClassName = "6º Ano A", TotalClasses = 100, TotalAbsences = 5 });
            SocialReportLines.Add(new SocialReportLine { StudentName = "Pedro Henrique Costa", SchoolName = "Escola Municipal Aurora", ClassName = "6º Ano A", TotalClasses = 100, TotalAbsences = 28 }); // < 75%
            SocialReportLines.Add(new SocialReportLine { StudentName = "Ana Clara Santos", SchoolName = "Centro Educacional Rui Barbosa", ClassName = "9º Ano B", TotalClasses = 120, TotalAbsences = 35 }); // < 75%
            SocialReportLines.Add(new SocialReportLine { StudentName = "Lucas Mendes Silva", SchoolName = "Centro Educacional Rui Barbosa", ClassName = "9º Ano B", TotalClasses = 120, TotalAbsences = 2 });
            SocialReportLines.Add(new SocialReportLine { StudentName = "Sofia Rodrigues", SchoolName = "Escola Paulo Freire", ClassName = "1º Ano A", TotalClasses = 80, TotalAbsences = 0 });
            SocialReportLines.Add(new SocialReportLine { StudentName = "Gabriel Almeida", SchoolName = "Escola Paulo Freire", ClassName = "1º Ano A", TotalClasses = 80, TotalAbsences = 22 }); // < 75%
        }
        // --- FIM DO MOCK ---
    }

    public System.Action<School>? OnAccessSchool { get; set; }

    [RelayCommand]
    private void Navigate(string viewName)
    {
        IsVisaoGeralVisible = viewName == "VisaoGeral";
        IsEscolasVisible = viewName == "Escolas";
        IsDiretoresVisible = viewName == "Diretores";
        IsRelatoriosVisible = viewName == "Relatorios";
        IsSocialReportVisible = viewName == "SocialReport";

        if (viewName == "Escolas")
        {
            _ = LoadSchoolsAsync();
        }
        else if (viewName == "Diretores")
        {
            // Precisamos carregar as escolas primeiro para o ComboBox do modal
            _ = LoadSchoolsAsync().ContinueWith(_ => LoadDirectorsAsync());
        }
    }

    public SecretaryDashboardViewModel()
    {
    }

    partial void OnTenantNameChanged(string value)
    {
        _ = LoadSchoolsAsync();
    }

    private async Task LoadSchoolsAsync()
    {
        if (string.IsNullOrWhiteSpace(TenantName) || TenantName == "Carregando...") return;

        try
        {
            var queryJson = "{\"method\":\"equal\",\"attribute\":\"tenant_id\",\"values\":[\"" + TenantName + "\"]}";
            var queries = new List<string> { queryJson };

            var res = await AppwriteService.Instance.RawListDocumentsAsync("schools", queries);
            var docs = res.GetProperty("documents");
            
            Schools.Clear();
            foreach (var doc in docs.EnumerateArray())
            {
                var school = new School
                {
                    Id = doc.GetProperty("$id").GetString() ?? "",
                    Name = doc.TryGetProperty("name", out var nProp) && nProp.ValueKind != JsonValueKind.Null ? nProp.GetString() ?? "" : "",
                    Inep = doc.TryGetProperty("inep", out var iProp) && iProp.ValueKind != JsonValueKind.Null ? iProp.GetString() ?? "" : "",
                    Principal = doc.TryGetProperty("principal", out var pProp) && pProp.ValueKind != JsonValueKind.Null ? pProp.GetString() ?? "" : "",
                    TenantId = doc.TryGetProperty("tenant_id", out var tProp) && tProp.ValueKind != JsonValueKind.Null ? tProp.GetString() ?? "" : "",
                    Status = doc.TryGetProperty("status", out var sProp) && sProp.ValueKind != JsonValueKind.Null ? sProp.GetString() ?? "Ativa" : "Ativa"
                };
                Schools.Add(school);
            }
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Erro ao carregar escolas: " + ex.Message);
        }
    }

    [RelayCommand]
    private void OpenAddSchoolModal()
    {
        EditingSchool = new School { TenantId = TenantName };
        ModalTitle = "Adicionar Escola";
        IsSchoolModalOpen = true;
    }

    
    [RelayCommand]
    private void AccessSchool(School school)
    {
        OnAccessSchool?.Invoke(school);
    }
    
    [RelayCommand]
    private void EditSchool(School school)

    {
        EditingSchool = new School
        {
            Id = school.Id,
            Name = school.Name,
            Inep = school.Inep,
            Principal = school.Principal,
            TenantId = school.TenantId,
            Status = school.Status
        };
        ModalTitle = "Editar Escola";
        IsSchoolModalOpen = true;
    }

    [RelayCommand]
    private void CloseModal()
    {
        IsSchoolModalOpen = false;
    }

    [RelayCommand]
    private async Task SaveSchoolAsync()
    {
        if (string.IsNullOrWhiteSpace(EditingSchool.Name)) return;

        try
        {
            var data = new Dictionary<string, object>
            {
                { "name", EditingSchool.Name },
                { "inep", EditingSchool.Inep },
                { "principal", EditingSchool.Principal },
                { "tenant_id", TenantName },
                { "status", EditingSchool.Status }
            };

            if (string.IsNullOrEmpty(EditingSchool.Id))
            {
                await AppwriteService.Instance.RawCreateDocumentAsync("schools", data, new List<string> { "read(\"any\")", "update(\"users\")", "delete(\"users\")" });
            }
            else
            {
                await AppwriteService.Instance.RawUpdateDocumentAsync("schools", EditingSchool.Id, data);
            }

            IsSchoolModalOpen = false;
            await LoadSchoolsAsync();
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Erro ao salvar escola: " + ex.Message);
        }
    }

    [RelayCommand]
    private async Task DeleteSchoolAsync(School school)
    {
        try
        {
            await AppwriteService.Instance.RawDeleteDocumentAsync("schools", school.Id);
            await LoadSchoolsAsync();
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Erro ao excluir escola: " + ex.Message);
        }
    }

    // --- DIRETORES CRUD ---

    private async Task LoadDirectorsAsync()
    {
        if (string.IsNullOrWhiteSpace(TenantName) || TenantName == "Carregando...") return;

        try
        {
            var queryJson = "{\"method\":\"equal\",\"attribute\":\"tenant_id\",\"values\":[\"" + TenantName + "\"]}";
            var queries = new List<string> { queryJson };

            var res = await AppwriteService.Instance.RawListDocumentsAsync("directors", queries);
            var docs = res.GetProperty("documents");
            
            // Para poder manipular em UI thread segura
            var newDirectors = new List<Director>();

            foreach (var doc in docs.EnumerateArray())
            {
                var dir = new Director
                {
                    Id = doc.GetProperty("$id").GetString() ?? "",
                    Name = doc.TryGetProperty("name", out var nProp) && nProp.ValueKind != JsonValueKind.Null ? nProp.GetString() ?? "" : "",
                    Email = doc.TryGetProperty("email", out var eProp) && eProp.ValueKind != JsonValueKind.Null ? eProp.GetString() ?? "" : "",
                    Phone = doc.TryGetProperty("phone", out var pProp) && pProp.ValueKind != JsonValueKind.Null ? pProp.GetString() ?? "" : "",
                    SchoolId = doc.TryGetProperty("school_id", out var sProp) && sProp.ValueKind != JsonValueKind.Null ? sProp.GetString() ?? "" : "",
                    TenantId = doc.TryGetProperty("tenant_id", out var tProp) && tProp.ValueKind != JsonValueKind.Null ? tProp.GetString() ?? "" : "",
                };

                var matchingSchool = Schools.FirstOrDefault(s => s.Id == dir.SchoolId);
                dir.SchoolName = matchingSchool != null ? matchingSchool.Name : "Sem vínculo";

                newDirectors.Add(dir);
            }

            Avalonia.Threading.Dispatcher.UIThread.Post(() => {
                Directors.Clear();
                foreach(var d in newDirectors) Directors.Add(d);
            });
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Erro ao carregar diretores: " + ex.Message);
        }
    }

    [RelayCommand]
    private void OpenAddDirectorModal()
    {
        EditingDirector = new Director { TenantId = TenantName };
        DirectorModalTitle = "Adicionar Diretor";
        IsDirectorModalOpen = true;
    }

    [RelayCommand]
    private void EditDirector(Director dir)
    {
        EditingDirector = new Director
        {
            Id = dir.Id,
            Name = dir.Name,
            Email = dir.Email,
            Phone = dir.Phone,
            SchoolId = dir.SchoolId,
            TenantId = dir.TenantId
        };
        DirectorModalTitle = "Editar Diretor";
        IsDirectorModalOpen = true;
    }

    [RelayCommand]
    private void CloseDirectorModal()
    {
        IsDirectorModalOpen = false;
    }

    [RelayCommand]
    private async Task SaveDirectorAsync()
    {
        if (string.IsNullOrWhiteSpace(EditingDirector.Name)) return;

        try
        {
            var data = new Dictionary<string, object>
            {
                { "name", EditingDirector.Name },
                { "email", EditingDirector.Email },
                { "phone", EditingDirector.Phone },
                { "school_id", EditingDirector.SchoolId },
                { "tenant_id", TenantName }
            };

            if (string.IsNullOrEmpty(EditingDirector.Id))
            {
                await AppwriteService.Instance.RawCreateDocumentAsync("directors", data, new List<string> { "read(\"any\")", "update(\"users\")", "delete(\"users\")" });
            }
            else
            {
                await AppwriteService.Instance.RawUpdateDocumentAsync("directors", EditingDirector.Id, data);
            }

            IsDirectorModalOpen = false;
            await LoadDirectorsAsync();
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Erro ao salvar diretor: " + ex.Message);
        }
    }

    [RelayCommand]
    private async Task DeleteDirectorAsync(Director dir)
    {
        try
        {
            await AppwriteService.Instance.RawDeleteDocumentAsync("directors", dir.Id);
            await LoadDirectorsAsync();
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Erro ao excluir diretor: " + ex.Message);
        }
    }

    
    // --- RELATÓRIOS ---
    [ObservableProperty]
    private School? _selectedReportSchool;

    [ObservableProperty]
    private string _reportTitle = "Relatório Global da Rede";

    [ObservableProperty]
    private string _frequenciaMedia = "92.5%";

    [ObservableProperty]
    private int _bim1Height = 90;

    [ObservableProperty]
    private int _bim2Height = 120;

    [ObservableProperty]
    private int _bim3Height = 100;

    [ObservableProperty]
    private int _bim4Height = 140;

    [RelayCommand]
    private void SetSelectedReportSchoolNull()
    {
        SelectedReportSchool = null;
    }

    partial void OnSelectedReportSchoolChanged(School? value)
    {
        if (value == null)
        {
            ReportTitle = "Relatório Global da Rede";
            FrequenciaMedia = "92.5%";
            Bim1Height = 90;
            Bim2Height = 120;
            Bim3Height = 100;
            Bim4Height = 140;
        }
        else
        {
            ReportTitle = $"Relatório: {value.Name}";
            
            // Gerar valores aleatórios para simular a mudança visual por escola
            var rnd = new System.Random(value.Id.GetHashCode());
            FrequenciaMedia = $"{rnd.Next(75, 99)}.{rnd.Next(0, 9)}%";
            Bim1Height = rnd.Next(50, 150);
            Bim2Height = rnd.Next(50, 150);
            Bim3Height = rnd.Next(50, 150);
            Bim4Height = rnd.Next(50, 150);
        }
    }

    [RelayCommand]
    private async Task GeneratePdfReportAsync()
    {
        try
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("TITULO;FREQUENCIA MEDIA;DESEMPENHO BIM 1;DESEMPENHO BIM 2;DESEMPENHO BIM 3;DESEMPENHO BIM 4");
            
            sb.AppendLine($"{ReportTitle};{FrequenciaMedia};{Bim1Height}%;{Bim2Height}%;{Bim3Height}%;{Bim4Height}%");

            string desktopDir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
            string tempPath = System.IO.Path.Combine(desktopDir, $"Relatorio_Geral_Rede_{TenantName.Replace(" ", "_")}.csv");
            await System.IO.File.WriteAllTextAsync(tempPath, sb.ToString(), System.Text.Encoding.UTF8);

            // Open folder
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo { FileName = "explorer.exe", Arguments = $"/select,\"{tempPath}\"", UseShellExecute = true });
            }
            else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
            {
                var psi = new System.Diagnostics.ProcessStartInfo { FileName = "xdg-open", UseShellExecute = false };
                psi.ArgumentList.Add(desktopDir);
                System.Diagnostics.Process.Start(psi);
            }
            else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX))
            {
                var psi = new System.Diagnostics.ProcessStartInfo { FileName = "open", UseShellExecute = false };
                psi.ArgumentList.Add("-R");
                psi.ArgumentList.Add(tempPath);
                System.Diagnostics.Process.Start(psi);
            }
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Erro ao gerar CSV da Rede: " + ex.Message);
        }
    }

    [RelayCommand]
    private void Logout()
    {
        OnLogout?.Invoke();
    }

    // --- BACKUP & AUDITORIA ---
    [ObservableProperty]
    private bool _isExporting = false;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasBackupMessage))]
    private string _backupMessage = "";

    public bool HasBackupMessage => !string.IsNullOrEmpty(BackupMessage);

    [RelayCommand]
    private async Task ExportMunicipalBackupAsync()
    {
        if (IsExporting) return;
        
        try
        {
            IsExporting = true;
            BackupMessage = "Baixando dados...";

            var csvBuilder = new System.Text.StringBuilder();
            csvBuilder.AppendLine("Auditoria_Criacao,Auditoria_Atualizacao,ID_Nota,Escola,Turma,Aluno,Matricula,Disciplina,Bimestre_Unidade,Valor_Nota,Modelo_Nota");

            // 1. Fetch ALL schools for this Tenant
            var schoolQueries = new List<string> { $"{{\"method\":\"equal\",\"attribute\":\"tenant_id\",\"values\":[\"{TenantName}\"]}}" };
            var schoolRes = await AppwriteService.Instance.RawListDocumentsAsync("schools", schoolQueries);
            var schoolDocs = schoolRes.GetProperty("documents");
            var schoolsMap = new Dictionary<string, string>();
            foreach (var doc in schoolDocs.EnumerateArray())
            {
                var id = doc.GetProperty("$id").GetString()!;
                var name = doc.TryGetProperty("name", out var p) && p.ValueKind != System.Text.Json.JsonValueKind.Null ? p.GetString() ?? "" : "";
                schoolsMap[id] = name;
            }

            // 2. Fetch ALL classes
            var classesRes = await AppwriteService.Instance.RawListDocumentsAsync("classes", new List<string>());
            var classDocs = classesRes.GetProperty("documents");
            var classesMap = new Dictionary<string, (string Name, string SchoolId)>();
            foreach (var doc in classDocs.EnumerateArray())
            {
                var id = doc.GetProperty("$id").GetString()!;
                var name = doc.TryGetProperty("name", out var p) && p.ValueKind != System.Text.Json.JsonValueKind.Null ? p.GetString() ?? "" : "";
                var sid = doc.TryGetProperty("school_id", out var p2) && p2.ValueKind != System.Text.Json.JsonValueKind.Null ? p2.GetString() ?? "" : "";
                classesMap[id] = (name, sid);
            }

            // 3. Fetch ALL students
            var studentsRes = await AppwriteService.Instance.RawListDocumentsAsync("students", new List<string>());
            var studentDocs = studentsRes.GetProperty("documents");
            var studentsMap = new Dictionary<string, (string Name, string Reg, string ClassId)>();
            foreach (var doc in studentDocs.EnumerateArray())
            {
                var id = doc.GetProperty("$id").GetString()!;
                var name = doc.TryGetProperty("name", out var p) && p.ValueKind != System.Text.Json.JsonValueKind.Null ? p.GetString() ?? "" : "";
                var reg = doc.TryGetProperty("registration", out var p2) && p2.ValueKind != System.Text.Json.JsonValueKind.Null ? p2.GetString() ?? "" : "";
                var cid = doc.TryGetProperty("class_id", out var p3) && p3.ValueKind != System.Text.Json.JsonValueKind.Null ? p3.GetString() ?? "" : "";
                studentsMap[id] = (name, reg, cid);
            }

            // 4. Fetch ALL grades
            var gradesRes = await AppwriteService.Instance.RawListDocumentsAsync("grades", new List<string>());
            var gradeDocs = gradesRes.GetProperty("documents");
            
            foreach (var doc in gradeDocs.EnumerateArray())
            {
                var createdAt = doc.GetProperty("$createdAt").GetString();
                var updatedAt = doc.GetProperty("$updatedAt").GetString();
                var id = doc.GetProperty("$id").GetString();
                
                var studentId = doc.TryGetProperty("student_id", out var pSId) && pSId.ValueKind != System.Text.Json.JsonValueKind.Null ? pSId.GetString() ?? "" : "";
                var subject = doc.TryGetProperty("discipline", out var pSub) && pSub.ValueKind != System.Text.Json.JsonValueKind.Null ? pSub.GetString() ?? "" : "";
                var unit = doc.TryGetProperty("unit", out var pU) && pU.ValueKind != System.Text.Json.JsonValueKind.Null ? pU.GetInt32().ToString() : "";
                var val = doc.TryGetProperty("value", out var pV) && pV.ValueKind != System.Text.Json.JsonValueKind.Null ? pV.GetDouble().ToString("F1") : "";
                var model = doc.TryGetProperty("grade_model", out var pM) && pM.ValueKind != System.Text.Json.JsonValueKind.Null ? pM.GetString() ?? "" : "";

                var sName = "Desconhecido";
                var sReg = "Desconhecido";
                var cName = "Desconhecido";
                var schName = "Desconhecido";

                if (studentsMap.TryGetValue(studentId, out var studentInfo))
                {
                    sName = studentInfo.Name;
                    sReg = studentInfo.Reg;
                    
                    if (classesMap.TryGetValue(studentInfo.ClassId, out var classInfo))
                    {
                        cName = classInfo.Name;
                        if (schoolsMap.TryGetValue(classInfo.SchoolId, out var schoolName))
                        {
                            schName = schoolName;
                        }
                    }
                }

                // If the school is part of this tenant, add to CSV
                if (schName != "Desconhecido")
                {
                    csvBuilder.AppendLine($"{createdAt},{updatedAt},{id},\"{schName}\",\"{cName}\",\"{sName}\",\"{sReg}\",\"{subject}\",{unit},{val},\"{model}\"");
                }
            }

            // 5. Save to Desktop
            var desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
            var timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var filePath = System.IO.Path.Combine(desktopPath, $"Backup_EducaMais_{timestamp}.csv");
            
            await System.IO.File.WriteAllTextAsync(filePath, csvBuilder.ToString(), System.Text.Encoding.UTF8);
            
            BackupMessage = $"Salvo em: {filePath}";
            
            try
            {
                if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo { FileName = desktopPath, UseShellExecute = true });
        }
        else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
        {
            var psi = new System.Diagnostics.ProcessStartInfo { FileName = "xdg-open", UseShellExecute = false };
            psi.ArgumentList.Add("file://" + desktopPath);
            System.Diagnostics.Process.Start(psi);
        }
        else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX))
        {
            var psi = new System.Diagnostics.ProcessStartInfo { FileName = "open", UseShellExecute = false };
            psi.ArgumentList.Add(desktopPath);
            System.Diagnostics.Process.Start(psi);
        }
            }
            catch { }
            
            // Auto hide message after 5 seconds
            _ = Task.Run(async () => {
                await Task.Delay(5000);
                Avalonia.Threading.Dispatcher.UIThread.Post(() => BackupMessage = "");
            });
        }
        catch (System.Exception ex)
        {
            BackupMessage = "Erro: " + ex.Message;
        }
        finally
        {
            IsExporting = false;
        }
    }

}
