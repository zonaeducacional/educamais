using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EducaMais.Models;
using EducaMais.Services;

namespace EducaMais.ViewModels;

public partial class AdminDashboardViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _welcomeMessage = "Visão Geral do Sistema";

    [ObservableProperty]
    private int _totalMunicipalities;

    [ObservableProperty]
    private int _totalSchools;

    [ObservableProperty]
    private int _totalStudents;

    public System.Collections.Generic.List<string> Cities { get; } = BahiaCities.List;

    [ObservableProperty]
    private string? _selectedCity;

    [ObservableProperty]
    private bool _isModalOpen;

    [ObservableProperty]
    private string _secretaryName = "";

    [ObservableProperty]
    private string _secretaryPhone = "";

    [ObservableProperty]
    private string _secretaryEmail = "";

    private string _currentCityDocId = "";

    public System.Action? OnLogout { get; set; }
    public System.Action<EducaMais.Models.Municipality>? OnAccessSecretary { get; set; }

    public System.Collections.ObjectModel.ObservableCollection<EducaMais.Models.Municipality> Municipalities { get; set; } = new();

    [ObservableProperty]
    private bool _isDashboardVisible = true;

    [ObservableProperty]
    private bool _isSettingsVisible = false;

    [ObservableProperty]
    private bool _isCredentialsModalOpen = false;

    [ObservableProperty]
    private string _generatedCredentialsMessage = "";


    [RelayCommand]
    private void ShowDashboard()
    {
        IsDashboardVisible = true;
        IsSettingsVisible = false;
    }

    [RelayCommand]
    private void ShowSettings()
    {
        IsDashboardVisible = false;
        IsSettingsVisible = true;
    }

    public AdminDashboardViewModel()
    {
        _ = LoadMetricsAsync();
    }

    private async Task LoadMetricsAsync()
    {
        try
        {
            var res = await AppwriteService.Instance.RawListDocumentsAsync("municipalities");
            var docs = res.GetProperty("documents");
            
            TotalMunicipalities = res.GetProperty("total").GetInt32();
            TotalSchools = 45; // Simulado
            TotalStudents = 12500; // Simulado

            Municipalities.Clear();
            foreach (var doc in docs.EnumerateArray())
            {
                var name = doc.TryGetProperty("name", out var nProp) && nProp.ValueKind != System.Text.Json.JsonValueKind.Null ? nProp.GetString() ?? "" : "";
                var secName = doc.TryGetProperty("secretaryName", out var sProp) && sProp.ValueKind != System.Text.Json.JsonValueKind.Null ? sProp.GetString() ?? "" : "";
                var contact = doc.TryGetProperty("contact", out var cProp) && cProp.ValueKind != System.Text.Json.JsonValueKind.Null ? cProp.GetString() ?? "" : "";
                var email = doc.TryGetProperty("email", out var eProp) && eProp.ValueKind != System.Text.Json.JsonValueKind.Null ? eProp.GetString() ?? "" : "";
                var id = doc.GetProperty("$id").GetString() ?? "";
                
                Municipalities.Add(new EducaMais.Models.Municipality(id, name, secName, contact, email));
            }
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Erro metrics: " + ex.Message);
        }
    }

    partial void OnSelectedCityChanged(string? value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            _ = OpenModalForCityAsync(value);
        }
    }

    private async Task OpenModalForCityAsync(string cityName)
    {
        SecretaryName = "";
        SecretaryPhone = "";
        SecretaryEmail = "";
        _currentCityDocId = "";

        try
        {
            var queryJson = "{\"method\":\"equal\",\"attribute\":\"name\",\"values\":[\"" + cityName + "\"]}";
            var queries = new System.Collections.Generic.List<string> { queryJson };
            var res = await AppwriteService.Instance.RawListDocumentsAsync("municipalities", queries);
            
            var docs = res.GetProperty("documents");
            if (docs.GetArrayLength() > 0)
            {
                var doc = docs[0];
                _currentCityDocId = doc.GetProperty("$id").GetString() ?? "";
                
                if (doc.TryGetProperty("secretaryName", out var sName) && sName.ValueKind != System.Text.Json.JsonValueKind.Null)
                    SecretaryName = sName.GetString() ?? "";
                    
                if (doc.TryGetProperty("contact", out var cPhone) && cPhone.ValueKind != System.Text.Json.JsonValueKind.Null)
                    SecretaryPhone = cPhone.GetString() ?? "";
                    
                if (doc.TryGetProperty("email", out var cEmail) && cEmail.ValueKind != System.Text.Json.JsonValueKind.Null)
                    SecretaryEmail = cEmail.GetString() ?? "";
            }
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Erro ao buscar cidade: " + ex.Message);
        }

        IsModalOpen = true;
    }

    [RelayCommand]
    private void CloseModal()
    {
        IsModalOpen = false;
        SelectedCity = null;
    }

    [RelayCommand]
    private async Task SaveSecretaryAsync()
    {
        if (string.IsNullOrEmpty(SelectedCity)) return;

        try
        {
            var data = new System.Collections.Generic.Dictionary<string, object>
            {
                { "name", SelectedCity },
                { "secretaryName", SecretaryName },
                { "contact", SecretaryPhone },
                { "email", SecretaryEmail }
            };

            var permissions = new System.Collections.Generic.List<string> { "read(\"users\")", "update(\"users\")", "delete(\"users\")" };

            if (string.IsNullOrEmpty(_currentCityDocId))
            {
                await AppwriteService.Instance.RawCreateDocumentAsync("municipalities", data, permissions);
            }
            else
            {
                await AppwriteService.Instance.RawUpdateDocumentAsync("municipalities", _currentCityDocId, data, permissions);
            }

            CloseModal();
            await LoadMetricsAsync();
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Erro ao salvar: " + ex.Message);
        }
    }

    [RelayCommand]
    private void AccessSecretary(EducaMais.Models.Municipality mun)
    {
        OnAccessSecretary?.Invoke(mun);
    }

    [RelayCommand]
    private void EditSecretary(EducaMais.Models.Municipality mun)
    {
        SelectedCity = mun.Name; // This will trigger OnSelectedCityChanged and open the modal!
    }

    [RelayCommand]
    private async Task DeleteSecretaryAsync(EducaMais.Models.Municipality mun)
    {
        try
        {
            var client = new System.Net.Http.HttpClient();
            client.DefaultRequestHeaders.Add("X-Appwrite-Project", AppwriteService.ProjectId);
            client.DefaultRequestHeaders.Add("X-Appwrite-Key", "standard_cdcb5d55e4a6175bcf85877721a75d924ef35d25d434d8ed480c749b8d9655a7c955bb081f8bd3abc67f5a10c87052f6f34699dbb54858d816710c97ffd6a17bf22442ec032e3eed1e031a3f84c87cb16b8a7d8d27c05245839c43e369cdde6c7fdcc346238bc9d7df21b8e0b7a6f9211020c1b98fe581757ee5d68228159643");
            
            var res = await client.DeleteAsync($"{AppwriteService.Endpoint}/databases/{AppwriteService.DatabaseId}/collections/municipalities/documents/{mun.Id}");
            res.EnsureSuccessStatusCode();

            await LoadMetricsAsync();
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Erro ao deletar: " + ex.Message);
        }
    }
            [RelayCommand]
    private void GenerateCredentials(EducaMais.Models.Municipality mun)
    {
        var login = string.IsNullOrWhiteSpace(mun.Email) ? $"secretario@{mun.Name.ToLower().Replace(" ", "")}.ba.gov.br" : mun.Email;
        var password = System.Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        GeneratedCredentialsMessage = $"Credenciais geradas para {mun.Name}:\n\nLogin: {login}\nSenha temporária: {password}\n\nEnvie estes dados para o Secretário acessar o Módulo Municipal.";


        
        // Salva na memória do MVP para permitir o Login!
        LoginViewModel.MockGeneratedUsers[login] = (password, mun.Name);

        IsCredentialsModalOpen = true;
    }

    [RelayCommand]
    private void CloseCredentialsModal()
    {
        IsCredentialsModalOpen = false;
    }

    [RelayCommand]
    private async Task LogoutAsync()
    {
        await AppwriteService.Instance.LogoutAsync();
        OnLogout?.Invoke();
    }
}
