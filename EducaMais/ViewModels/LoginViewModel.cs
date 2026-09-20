using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EducaMais.Services;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EducaMais.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    // Dicionário estático para simular o banco de dados de usuários gerados na sessão (MVP)
    // Chave: Email, Valor: (Senha, TenantName)
    public static Dictionary<string, (string Password, string TenantName)> MockGeneratedUsers { get; set; } = new();

    // Evento de sucesso passa a Role e opcionalmente o TenantName vinculado
    // Evento de sucesso passa a Role e opcionalmente o TenantName vinculado
    public Action<string, string>? OnLoginSuccess { get; set;     }
    
    public Action? OnCancel { get; set;     }

    [ObservableProperty]
    private string _username = "";

    [ObservableProperty]
    private string _password = "";

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _errorMessage = "";

    private AppwriteService _appwriteService;
    
    [ObservableProperty]
    private string _expectedRole = "secretary";

    public LoginViewModel()
    {
        _appwriteService = AppwriteService.Instance;
        }

    [RelayCommand]
    private async Task LoginAsync()
    {
        IsBusy = true;
        ErrorMessage = string.Empty;

        await Task.Delay(1000); // Simulate network

        if (ExpectedRole == "student")
        {
            // Bypass MVP para Aluno: Acessa direto
            OnLoginSuccess?.Invoke(ExpectedRole, "");
            }
        // Bypass genérico do Superadmin
        else if ((Username == "admin" || Username == "admin@educamais.com") && (Password == "admin" || Password == "Educa@2026") && ExpectedRole == "superadmin")
        {
            OnLoginSuccess?.Invoke(ExpectedRole, "");
            }
        // Validação contra os usuários gerados em memória (MVP)
        else if (MockGeneratedUsers.ContainsKey(Username) && MockGeneratedUsers[Username].Password == Password)
        {
            var tenantName = MockGeneratedUsers[Username].TenantName;
            OnLoginSuccess?.Invoke(ExpectedRole, tenantName);
            }
        else
        {
            ErrorMessage = "E-mail ou senha inválidos.";
            }

        IsBusy = false;
    }

    [RelayCommand]
    private void Cancel()
    {
        OnCancel?.Invoke();
    }
}
