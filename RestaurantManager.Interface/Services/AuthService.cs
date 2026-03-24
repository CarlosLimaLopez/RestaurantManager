using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RestaurantManager.Auth
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(string username, string password);
        Task LogoutAsync();
        Task<bool> IsAuthenticatedAsync();
        string? CurrentUser { get; }
        string? CurrentRole { get; }
    }

    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private const string TokenKey = "auth_token";

        public string? CurrentUser { get; private set; }
        public string? CurrentRole { get; private set; }

        public AuthService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                var loginRequest = new LoginRequest
                { 
                    Username = username, 
                    Password = password 
                };

                var response = await _httpClient.PostAsJsonAsync("auth/login", loginRequest);

                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
                    if (loginResponse != null)
                    {
                        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, loginResponse.Token);

                        CurrentUser = loginResponse.Username;
                        CurrentRole = loginResponse.UserRole;

                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Token);

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex.Message}");
            }

            return false;
        }

        public async Task LogoutAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
            _httpClient.DefaultRequestHeaders.Authorization = null;
            CurrentUser = null;
            CurrentRole = null;
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            try
            {
                var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TokenKey);
                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var response = await _httpClient.GetAsync("auth/validate");
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadFromJsonAsync<AuthValidateResponse>();
                        CurrentUser = result?.Username;
                        CurrentRole = result?.UserRole;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Authenticate token error: {ex.Message}");

                await LogoutAsync();
            }

            return false;
        }
    }
}