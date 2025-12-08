namespace ActivitiesAndTasksAPI.DTOs
{
    public class JwtSettings
    {
        public string Key { get; set; } = "replace-with-a-very-long-random-secret";
        public string Issuer { get; set; } = "ActivitiesAndTasksAPI";
        public string Audience { get; set; } = "ActivitiesAndTasksAPIClients";
        public int DurationMinutes { get; set; } = 30;
        public bool Enabled { get; set; } = true;
    }

    public class JwtInfo
    {
        public string Token { get; set; } = string.Empty;
        public string ExpiresAt { get; set; } = string.Empty;
    }
}