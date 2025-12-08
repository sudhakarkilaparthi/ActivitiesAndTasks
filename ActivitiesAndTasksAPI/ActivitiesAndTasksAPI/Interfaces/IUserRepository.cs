using ActivitiesAndTasksAPI.DTOs;

namespace ActivitiesAndTasksAPI.Interfaces
{
    public interface IUserRepository
    {
		Task<List<User>> GetUsersAsync2();
		Task<List<User>> GetUsersAsync();
		Task<User?> GetLogin(LoginDto loginDto);
		Task<User?> GetUserByEmail(string Email);
		Task<int> CreateUser(AddUserDto addUserDto);


	}
}
