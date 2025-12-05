using ActivitiesAndTasksAPI.DTOs;
using ActivitiesAndTasksAPI.Interfaces;

namespace ActivitiesAndTasksAPI.Models
{
    public class UserModel
    {
		private readonly IUserRepository _userRepository;
		public UserModel(IUserRepository userRepository)
		{
			this._userRepository = userRepository;
		}

		public async Task<List<User>> GetAllUsersAsync()
		{
			//return await _userRepository.GetUsersAsync();
			return await _userRepository.GetUsersAsync2();
		}

		public async Task<int> CreateUserAsync(AddUserDto addUserDto)
		{
			return await _userRepository.CreateUser(addUserDto);
		}
	}
}
