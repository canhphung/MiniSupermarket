using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace MiniSupermarket.WinForms.Services;

public static class ApiClientService
{
    public static HttpClient Client { get; } = new(new SessionTokenHandler())
    {
        BaseAddress = new Uri(Environment.GetEnvironmentVariable("MINISUPERMARKET_API_URL")
            ?? "https://localhost:7203/api/"),
        Timeout = TimeSpan.FromSeconds(30)
    };

    public static void AttachToken(string? token) => SessionManager.JwtToken = token ?? string.Empty;

    public static async Task<string> SendAsync(HttpMethod method, string endpoint, object? body = null)
    {
        using var request = new HttpRequestMessage(method, endpoint);
        if (body != null) request.Content = JsonContent.Create(body);
        using var response = await Client.SendAsync(request);
        string content = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode) return content;
        if ((int)response.StatusCode == 401)
            throw new InvalidOperationException("Phiên đăng nhập không hợp lệ. Hãy đăng nhập lại.");
        if ((int)response.StatusCode == 403)
            throw new InvalidOperationException("Bạn không có quyền thực hiện thao tác này.");
        string error = $"API trả về HTTP {(int)response.StatusCode}.";
        try
        {
            using var doc = JsonDocument.Parse(content);
            var root = doc.RootElement;
            if (root.TryGetProperty("message", out var message)) error = message.GetString() ?? error;
            else if (root.TryGetProperty("errors", out var errors))
                error = string.Join(Environment.NewLine, errors.EnumerateObject()
                    .SelectMany(p => p.Value.EnumerateArray().Select(v => v.GetString())));
            else if (root.TryGetProperty("title", out var title)) error = title.GetString() ?? error;
        }
        catch (JsonException) { }
        throw new InvalidOperationException(error);
    }

    public static async Task<bool> LoginAsync(string username, string password)
    {
        AttachToken(null);
        SessionManager.CurrentRole = string.Empty;
        SessionManager.CurrentUsername = string.Empty;
        using var response = await Client.PostAsJsonAsync("auth/login", new { Username = username, Password = password });
        if (!response.IsSuccessStatusCode) return false;
        using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        AttachToken(doc.RootElement.GetProperty("token").GetString());
        SessionManager.CurrentRole = doc.RootElement.GetProperty("role").GetString() ?? string.Empty;
        SessionManager.CurrentUsername = username;
        return true;
    }

    public static Task<string> GetDataWithTokenAsync(string endpoint) => SendAsync(HttpMethod.Get, endpoint);
}

internal sealed class SessionTokenHandler : DelegatingHandler
{
    public SessionTokenHandler() : base(new HttpClientHandler()) { }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
    {
        request.Headers.Authorization = string.IsNullOrWhiteSpace(SessionManager.JwtToken)
            ? null : new AuthenticationHeaderValue("Bearer", SessionManager.JwtToken);
        return base.SendAsync(request, ct);
    }
}
