using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace EducaMais.ViewModels;

public partial class LandingViewModel : ViewModelBase
{
    public Action<string>? OnProfileSelected { get; set; }

    [ObservableProperty] private bool _isStudentLoginModalOpen = false;
    [ObservableProperty] private string _studentLogin = "";
    [ObservableProperty] private string _studentPassword = "";
    [ObservableProperty] private string _studentLoginError = "";

    [RelayCommand]
    private async Task SelectProfileAsync(string profile)
    {
        if (profile == "student")
        {
            IsStudentLoginModalOpen = true;
            StudentLoginError = "";
        }
        else
        {
            OnProfileSelected?.Invoke(profile);
        }
    }

    [RelayCommand]
    private void CloseStudentModal()
    {
        IsStudentLoginModalOpen = false;
    }

    [RelayCommand]
    private async Task LoginStudentAsync()
    {
        StudentLoginError = "";
        if (string.IsNullOrWhiteSpace(StudentLogin) || string.IsNullOrWhiteSpace(StudentPassword))
        {
            StudentLoginError = "Preencha matrícula e senha.";
            return;
        }
        
        try
        {
            string email = $"{StudentLogin.Trim()}@student.educamais.local".ToLower();
            // Tenta logar
            bool ok = await EducaMais.Services.AppwriteService.Instance.LoginAsync(email, StudentPassword.Trim());
            if (ok)
            {
                IsStudentLoginModalOpen = false;
                // Passa o id real pra carregar só as notas desse aluno depois
                OnProfileSelected?.Invoke($"student_auth_{StudentLogin}");
            }
            else
            {
                StudentLoginError = "Matrícula ou senha inválidos.";
            }
        }
        catch (Exception ex)
        {
            StudentLoginError = "Falha de conexão.";
        }
    }


    [RelayCommand]
    private void OldSelectProfile(string profile)
    {
        OnProfileSelected?.Invoke(profile);
    }
}
