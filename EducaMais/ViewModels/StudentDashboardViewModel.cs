using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EducaMais.Models;
using EducaMais.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace EducaMais.ViewModels;

public partial class StudentDashboardViewModel : ViewModelBase
{
    public Action? OnLogout { get; set; }

    [ObservableProperty] private string _studentName = "Carregando...";
    [ObservableProperty] private string _schoolName = "Carregando...";
    [ObservableProperty] private string _className = "Carregando...";

    [ObservableProperty] private double _attendancePercentage = 100.0;
    [ObservableProperty] private int _totalClasses = 0;
    [ObservableProperty] private int _totalAbsences = 0;
    
    // Aproveitamos o mesmo modelo da escola/professor para montar o grid de notas
    public ObservableCollection<StudentGradeViewModel> ReportCards { get; } = new();

    private string _authMatricula;
    public StudentDashboardViewModel(string matricula = "")
    {
        _authMatricula = matricula;
        _ = LoadStudentDataAsync();
    }

    [RelayCommand]
    private void Logout()
    {
        OnLogout?.Invoke();
    }

    private async Task LoadStudentDataAsync()
    {
        try
        {
            // Busca estudante
            List<string> queries = new();
            if (!string.IsNullOrEmpty(_authMatricula))
            {
                queries.Add($"{{\"method\":\"equal\",\"attribute\":\"registration\",\"values\":[\"{_authMatricula}\"]}}");
            }

            var res = await AppwriteService.Instance.RawListDocumentsAsync("students", queries);
            var docs = res.GetProperty("documents");
            if (docs.GetArrayLength() > 0)
            {
                var student = System.Text.Json.JsonSerializer.Deserialize<Student>(docs[0].GetRawText(), new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
                StudentName = student.Name;

                // Busca a Turma
                var classRes = await AppwriteService.Instance.RawGetDocumentAsync("classes", student.ClassId);
                var classObj = System.Text.Json.JsonSerializer.Deserialize<SchoolClass>(classRes.GetRawText(), new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
                ClassName = classObj.Name;

                // Busca a Escola
                var schoolRes = await AppwriteService.Instance.RawGetDocumentAsync("schools", classObj.SchoolId);
                var schoolObj = System.Text.Json.JsonSerializer.Deserialize<School>(schoolRes.GetRawText(), new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
                SchoolName = schoolObj.Name;

                // Busca as Notas Desse Estudante Específico
                List<string> gradeQueries = new() {
                    $"{{\"method\":\"equal\",\"attribute\":\"student_id\",\"values\":[\"{student.Id}\"]}}"
                };
                
                var gradesRes = await AppwriteService.Instance.RawListDocumentsAsync("grades", gradeQueries);
                var gradesDocs = gradesRes.GetProperty("documents");
                
                var studentGrades = new List<Grade>();
                foreach (var gDoc in gradesDocs.EnumerateArray())
                {
                    studentGrades.Add(System.Text.Json.JsonSerializer.Deserialize<Grade>(gDoc.GetRawText(), new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true })!);
                }

                // Carrega
                ReportCards.Clear();
                var subjects = studentGrades.Select(g => g.Discipline).Distinct();
                foreach (var subj in subjects)
                {
                    var subjGrades = studentGrades.Where(g => g.Discipline == subj).ToList();
                    var vm = new StudentGradeViewModel { Student = student };
                    // Usando 5.0 por padrão, já que os alunos não precisam ver média variável
                    vm.PassingGrade = 5.0;
                    
                    var g1 = subjGrades.FirstOrDefault(g => g.Unit == 1);
                    if (g1 != null) { vm.Grade1Id = g1.Id; vm.U1A1 = g1.A1; vm.U1A2 = g1.A2; vm.U1A3 = g1.A3; vm.U1A4 = g1.A4; vm.TradU1 = g1.Value; }
                    var g2 = subjGrades.FirstOrDefault(g => g.Unit == 2);
                    if (g2 != null) { vm.Grade2Id = g2.Id; vm.U2A1 = g2.A1; vm.U2A2 = g2.A2; vm.U2A3 = g2.A3; vm.U2A4 = g2.A4; vm.TradU2 = g2.Value; }
                    var g3 = subjGrades.FirstOrDefault(g => g.Unit == 3);
                    if (g3 != null) { vm.Grade3Id = g3.Id; vm.U3A1 = g3.A1; vm.U3A2 = g3.A2; vm.U3A3 = g3.A3; vm.U3A4 = g3.A4; vm.TradU3 = g3.Value; }
                    var g4 = subjGrades.FirstOrDefault(g => g.Unit == 4);
                    if (g4 != null) { vm.Grade4Id = g4.Id; vm.TradU4 = g4.Value; }
                    
                    var firstModel = subjGrades.FirstOrDefault()?.GradeModel;
                    if (!string.IsNullOrEmpty(firstModel))
                    {
                        vm.GradeModel = firstModel;
                    }

                    ReportCards.Add(vm);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao carregar dados do aluno: " + ex.Message);
        }
    }

}
