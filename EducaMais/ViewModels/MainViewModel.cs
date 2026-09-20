using CommunityToolkit.Mvvm.ComponentModel;

namespace EducaMais.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase? _currentViewModel;

    public MainViewModel()
    {
        ShowLanding();
    }

    private void ShowLanding()
    {
        var landing = new LandingViewModel();
        landing.OnProfileSelected = (role) => 
        {
            if (role.StartsWith("student_auth_"))
            {
                var mat = role.Replace("student_auth_", "");
                var authStudent = new StudentDashboardViewModel(mat);
                authStudent.OnLogout = ShowLanding;
                CurrentViewModel = authStudent;
            }
            else
            {
                ShowLogin(role);
            }
        };
        CurrentViewModel = landing;
    }

    private void ShowLogin(string role)
    {
        var login = new LoginViewModel();
        login.ExpectedRole = role;
        login.OnCancel = () => ShowLanding();
        login.OnLoginSuccess = (authenticatedRole, tenantName) => 
        {
            switch(authenticatedRole)
            {
                case "superadmin":
                    ShowAdmin();
                    break;
                case "school":
                    var school = new SchoolDashboardViewModel();
                    school.OnLogout = ShowLanding;
                    CurrentViewModel = school;
                    break;
                case "teacher":
                    var teacher = new TeacherDashboardViewModel();
                    teacher.OnLogout = ShowLanding;
                    CurrentViewModel = teacher;
                    break;
                case "student":
                    var student = new StudentDashboardViewModel("");
                    student.OnLogout = ShowLanding;
                    CurrentViewModel = student;
                    break;
                                default:
                    if (role.StartsWith("student_auth_"))
                    {
                        var mat = role.Replace("student_auth_", "");
                        var authStudent = new StudentDashboardViewModel(mat);
                        authStudent.OnLogout = ShowLanding;
                        CurrentViewModel = authStudent;
                        break;
                    }
                    var secretary = new SecretaryDashboardViewModel();
                    if (!string.IsNullOrEmpty(tenantName)) 
                        secretary.TenantName = tenantName;
                    secretary.OnLogout = ShowLanding;
                    secretary.OnAccessSchool = (school) =>
                    {
                        var schoolVm = new SchoolDashboardViewModel();
                        schoolVm.SchoolId = school.Id;
                        schoolVm.SchoolName = school.Name;
                        schoolVm.OnLogout = () => { CurrentViewModel = secretary; }; // Voltar para a secretaria
                        
                        schoolVm.OnAccessTeacher = (teacher) =>
                        {
                            var teacherVm = new TeacherDashboardViewModel();
                            teacherVm.InitializeAsync(teacher);
                            teacherVm.OnLogout = () => { CurrentViewModel = schoolVm; }; // Voltar para a escola
                            CurrentViewModel = teacherVm;
                        };
                        
                        CurrentViewModel = schoolVm;
                    };
                    CurrentViewModel = secretary;
                    break;
            }
        };
        CurrentViewModel = login;
    }

    private void ShowAdmin()
    {
        var adminVm = new AdminDashboardViewModel();
        adminVm.OnLogout = ShowLanding;
        adminVm.OnAccessSecretary = (mun) => {
            var secVm = new SecretaryDashboardViewModel();
            secVm.TenantName = mun.Name; // Passa o nome pro painel de Secretaria!
            
            // O botão de "Sair" do painel de Secretaria vai voltar pro Superadmin (como um "Back")
            secVm.OnLogout = ShowAdmin; 
            secVm.OnAccessSchool = (school) =>
            {
                var schoolVm = new SchoolDashboardViewModel();
                schoolVm.SchoolId = school.Id;
                schoolVm.SchoolName = school.Name;
                schoolVm.OnLogout = () => { CurrentViewModel = secVm; }; // Voltar para a secretaria
                
                schoolVm.OnAccessTeacher = (teacher) =>
                {
                    var teacherVm = new TeacherDashboardViewModel();
                    teacherVm.InitializeAsync(teacher);
                    teacherVm.OnLogout = () => { CurrentViewModel = schoolVm; }; // Voltar para a escola
                    CurrentViewModel = teacherVm;
                };
                
                CurrentViewModel = schoolVm;
            };
            
            CurrentViewModel = secVm;
        };
        CurrentViewModel = adminVm;
    }
}
