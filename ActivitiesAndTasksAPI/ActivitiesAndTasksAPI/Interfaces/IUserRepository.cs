using ActivitiesAndTasksAPI.DTOs;

namespace ActivitiesAndTasksAPI.Interfaces
{
    public interface IUserRepository
    {
		Task<List<User>> GetUsersAsync();
		Task<List<User>> GetUsersAsync2();
		Task<int> CreateUser(AddUserDto addUserDto);


	}
}
