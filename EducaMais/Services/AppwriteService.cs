using System;
using System.Threading.Tasks;
using Appwrite;
using Appwrite.Services;

namespace EducaMais.Services;

public class AppwriteService
{
    private static AppwriteService? _instance;
    public static AppwriteService Instance => _instance ??= new AppwriteService();

    private readonly Client _client;
    private readonly Account _account;
    private readonly Databases _databases;

    // Constantes do projeto (Configuradas para o ambiente local/Dell N4050)
    public const string ProjectId = "6aac6a93001f3977a4f7";
    public const string Endpoint = "https://educamais.vibelab.app.br/v1"; // Futuramente: https://educamais.vibelab.app.br/v1
    public const string DatabaseId = "educamais-db";

    private AppwriteService()
    {
        _client = new Client()
            .SetEndpoint(Endpoint)
            .SetProject(ProjectId);

        _account = new Account(_client);
        _databases = new Databases(_client);
    }

    public Account Account => _account;
    public Databases Databases => _databases;
    public Client Client => _client;

    public async Task<bool> LoginAsync(string email, string password)
    {
        try
        {
            await _account.CreateEmailPasswordSession(email, password);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro no login: {ex.Message}");
            return false;
        }
    }

    public async Task LogoutAsync()
    {
        try
        {
            if (_account != null)
            {
                await _account.DeleteSession("current");
            }
        }
        catch (AppwriteException ex)
        {
            // Se já for guest (sessão não existe), apenas ignoramos.
            if (!ex.Message.Contains("guests") && !ex.Message.Contains("missing scope"))
            {
                System.Console.WriteLine("Erro no logout: " + ex.Message);
            }
        }
    }

    
    public async Task<System.Text.Json.JsonElement> RawGetDocumentAsync(string collectionId, string documentId)
    {
        var client = new System.Net.Http.HttpClient();
        client.DefaultRequestHeaders.Add("X-Appwrite-Project", ProjectId);
        client.DefaultRequestHeaders.Add("X-Appwrite-Key", "standard_cdcb5d55e4a6175bcf85877721a75d924ef35d25d434d8ed480c749b8d9655a7c955bb081f8bd3abc67f5a10c87052f6f34699dbb54858d816710c97ffd6a17bf22442ec032e3eed1e031a3f84c87cb16b8a7d8d27c05245839c43e369cdde6c7fdcc346238bc9d7df21b8e0b7a6f9211020c1b98fe581757ee5d68228159643");
        
        string url = $"{Endpoint}/databases/{DatabaseId}/collections/{collectionId}/documents/{documentId}";
        
        var res = await client.GetAsync(url);
        if (!res.IsSuccessStatusCode)
        {
            var err = await res.Content.ReadAsStringAsync();
            throw new System.Exception($"Appwrite Error ({res.StatusCode}) on {collectionId}: {err}");
        }
        var json = await res.Content.ReadAsStringAsync();
        return System.Text.Json.JsonDocument.Parse(json).RootElement;
    }

    public async Task<System.Text.Json.JsonElement> RawListDocumentsAsync(string collectionId, System.Collections.Generic.List<string>? queries = null)
    {
        var client = new System.Net.Http.HttpClient();
        client.DefaultRequestHeaders.Add("X-Appwrite-Project", ProjectId);
        client.DefaultRequestHeaders.Add("X-Appwrite-Key", "standard_cdcb5d55e4a6175bcf85877721a75d924ef35d25d434d8ed480c749b8d9655a7c955bb081f8bd3abc67f5a10c87052f6f34699dbb54858d816710c97ffd6a17bf22442ec032e3eed1e031a3f84c87cb16b8a7d8d27c05245839c43e369cdde6c7fdcc346238bc9d7df21b8e0b7a6f9211020c1b98fe581757ee5d68228159643");
        
        string url = $"{Endpoint}/databases/{DatabaseId}/collections/{collectionId}/documents";
        
        if (queries != null && queries.Count > 0)
        {
            url += "?";
            foreach (var q in queries)
            {
                url += $"queries[]={System.Uri.EscapeDataString(q)}&";
            }
        }

        var res = await client.GetAsync(url);
        if (!res.IsSuccessStatusCode)
        {
            var err = await res.Content.ReadAsStringAsync();
            throw new System.Exception($"Appwrite Error ({res.StatusCode}) on {collectionId}: {err}");
        }
        var json = await res.Content.ReadAsStringAsync();
        return System.Text.Json.JsonDocument.Parse(json).RootElement;
    }

    public async Task RawCreateDocumentAsync(string collectionId, object data, System.Collections.Generic.List<string> permissions)
    {
        var client = new System.Net.Http.HttpClient();
        client.DefaultRequestHeaders.Add("X-Appwrite-Project", ProjectId);
        client.DefaultRequestHeaders.Add("X-Appwrite-Key", "standard_cdcb5d55e4a6175bcf85877721a75d924ef35d25d434d8ed480c749b8d9655a7c955bb081f8bd3abc67f5a10c87052f6f34699dbb54858d816710c97ffd6a17bf22442ec032e3eed1e031a3f84c87cb16b8a7d8d27c05245839c43e369cdde6c7fdcc346238bc9d7df21b8e0b7a6f9211020c1b98fe581757ee5d68228159643");

        var payload = new
        {
            documentId = "unique()",
            data = data,
            permissions = permissions
        };
        var content = new System.Net.Http.StringContent(System.Text.Json.JsonSerializer.Serialize(payload), System.Text.Encoding.UTF8, "application/json");
        var res = await client.PostAsync($"{Endpoint}/databases/{DatabaseId}/collections/{collectionId}/documents", content);
        res.EnsureSuccessStatusCode();
    }

    public async Task RawUpdateDocumentAsync(string collectionId, string documentId, object data, System.Collections.Generic.List<string>? permissions = null)
    {
        var client = new System.Net.Http.HttpClient();
        client.DefaultRequestHeaders.Add("X-Appwrite-Project", ProjectId);
        client.DefaultRequestHeaders.Add("X-Appwrite-Key", "standard_cdcb5d55e4a6175bcf85877721a75d924ef35d25d434d8ed480c749b8d9655a7c955bb081f8bd3abc67f5a10c87052f6f34699dbb54858d816710c97ffd6a17bf22442ec032e3eed1e031a3f84c87cb16b8a7d8d27c05245839c43e369cdde6c7fdcc346238bc9d7df21b8e0b7a6f9211020c1b98fe581757ee5d68228159643");

        var payload = new
        {
            data = data,
            permissions = permissions
        };
        var content = new System.Net.Http.StringContent(System.Text.Json.JsonSerializer.Serialize(payload), System.Text.Encoding.UTF8, "application/json");
        var req = new System.Net.Http.HttpRequestMessage(new System.Net.Http.HttpMethod("PATCH"), $"{Endpoint}/databases/{DatabaseId}/collections/{collectionId}/documents/{documentId}");
        req.Content = content;
        var res = await client.SendAsync(req);
        res.EnsureSuccessStatusCode();
    }

    public async Task RawDeleteDocumentAsync(string collectionId, string documentId)
    {
        using var client = new System.Net.Http.HttpClient();
        client.DefaultRequestHeaders.Add("X-Appwrite-Project", ProjectId);
        // client.DefaultRequestHeaders.Add("X-Appwrite-Key", ...); // Ignorado por segurança se rodando no cliente
        
        
        {
            client.DefaultRequestHeaders.Add("X-Appwrite-Key", "standard_cdcb5d55e4a6175bcf85877721a75d924ef35d25d434d8ed480c749b8d9655a7c955bb081f8bd3abc67f5a10c87052f6f34699dbb54858d816710c97ffd6a17bf22442ec032e3eed1e031a3f84c87cb16b8a7d8d27c05245839c43e369cdde6c7fdcc346238bc9d7df21b8e0b7a6f9211020c1b98fe581757ee5d68228159643");
        }

        var req = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Delete, $"{Endpoint}/databases/{DatabaseId}/collections/{collectionId}/documents/{documentId}");
        
        var res = await client.SendAsync(req);
        res.EnsureSuccessStatusCode();
    }

    public async Task<string> RawCreateUserAsync(string email, string password, string name)
    {
        var client = new System.Net.Http.HttpClient();
        client.DefaultRequestHeaders.Add("X-Appwrite-Project", ProjectId);
        client.DefaultRequestHeaders.Add("X-Appwrite-Key", "standard_cdcb5d55e4a6175bcf85877721a75d924ef35d25d434d8ed480c749b8d9655a7c955bb081f8bd3abc67f5a10c87052f6f34699dbb54858d816710c97ffd6a17bf22442ec032e3eed1e031a3f84c87cb16b8a7d8d27c05245839c43e369cdde6c7fdcc346238bc9d7df21b8e0b7a6f9211020c1b98fe581757ee5d68228159643");

        var payload = new
        {
            userId = "unique()",
            email = email,
            password = password,
            name = name
        };
        var content = new System.Net.Http.StringContent(System.Text.Json.JsonSerializer.Serialize(payload), System.Text.Encoding.UTF8, "application/json");
        var res = await client.PostAsync($"{Endpoint}/users", content);
        res.EnsureSuccessStatusCode();
        var json = await res.Content.ReadAsStringAsync();
        using var doc = System.Text.Json.JsonDocument.Parse(json);
        return doc.RootElement.GetProperty("$id").GetString() ?? "";
    }


    public async Task RawUpdateUserPasswordByEmailAsync(string email, string newPassword)
    {
        var client = new System.Net.Http.HttpClient();
        client.DefaultRequestHeaders.Add("X-Appwrite-Project", ProjectId);
        client.DefaultRequestHeaders.Add("X-Appwrite-Key", "standard_cdcb5d55e4a6175bcf85877721a75d924ef35d25d434d8ed480c749b8d9655a7c955bb081f8bd3abc67f5a10c87052f6f34699dbb54858d816710c97ffd6a17bf22442ec032e3eed1e031a3f84c87cb16b8a7d8d27c05245839c43e369cdde6c7fdcc346238bc9d7df21b8e0b7a6f9211020c1b98fe581757ee5d68228159643");

        // 1. Get users list and find by email
        var getRes = await client.GetAsync($"{Endpoint}/users?search={email}");
        getRes.EnsureSuccessStatusCode();
        var json = await getRes.Content.ReadAsStringAsync();
        using var doc = System.Text.Json.JsonDocument.Parse(json);
        var users = doc.RootElement.GetProperty("users");
        string userId = "";
        foreach (var u in users.EnumerateArray())
        {
            if (u.GetProperty("email").GetString() == email)
            {
                userId = u.GetProperty("$id").GetString() ?? "";
                break;
            }
        }

        if (string.IsNullOrEmpty(userId)) throw new Exception("User not found to update password.");

        // 2. Update password
        var payload = new { password = newPassword };
        var content = new System.Net.Http.StringContent(System.Text.Json.JsonSerializer.Serialize(payload), System.Text.Encoding.UTF8, "application/json");
        
        var req = new System.Net.Http.HttpRequestMessage(new System.Net.Http.HttpMethod("PATCH"), $"{Endpoint}/users/{userId}/password");
        req.Content = content;
        var patchRes = await client.SendAsync(req);
        if (!patchRes.IsSuccessStatusCode)
        {
            var err = await patchRes.Content.ReadAsStringAsync();
            throw new Exception(err);
        }
    }

}
