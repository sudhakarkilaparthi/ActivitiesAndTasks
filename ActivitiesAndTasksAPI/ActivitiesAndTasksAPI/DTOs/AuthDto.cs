namespace ActivitiesAndTasksAPI.DTOs
{
    public class LoginResponse
    {
        public JwtInfo TokenInfo { get; set; } = new JwtInfo();
        public User UserInfo { get; set; } = new User();
    }
}
