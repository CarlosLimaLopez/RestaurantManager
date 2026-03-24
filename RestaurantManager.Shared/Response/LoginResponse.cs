namespace RestaurantManager.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string UserRole { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }
    }
}
