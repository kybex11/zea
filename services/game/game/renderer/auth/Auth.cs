using Newtonsoft.Json;

namespace game.renderer.auth;

public class Auth
{  
    private static readonly HttpClient client = new();

    public async Task<string> Login(bool http, string ip, string username, string password)
    {
        var user = new { username, password };
        var json = JsonConvert.SerializeObject(user);
        var content = new StringContent(json);
        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        if (http) {
            var response = await client.PostAsync($"http://{ip}/login", content);
            return await response.Content.ReadAsStringAsync();
        } else {
            var response = await client.PostAsync($"https://{ip}/login", content);
            return await response.Content.ReadAsStringAsync();
        }

        
    }

    public async Task<string> Register(bool http, string ip, string username, string email, string password, string retry_password)
    {
        if (password != retry_password) return "Passwords do not match";

        var user = new { username, email, password };
        var json = JsonConvert.SerializeObject(user);
        var content = new StringContent(json);
        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        if (http) {
            var response = await client.PostAsync($"http://{ip}/register", content);
            return await response.Content.ReadAsStringAsync();
        } else {
            var response = await client.PostAsync($"https://{ip}/register", content);
            return await response.Content.ReadAsStringAsync();
        }
    }
}