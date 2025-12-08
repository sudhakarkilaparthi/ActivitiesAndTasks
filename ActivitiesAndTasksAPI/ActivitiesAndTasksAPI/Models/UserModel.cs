using ActivitiesAndTasksAPI.DTOs;
using ActivitiesAndTasksAPI.Enums;
using ActivitiesAndTasksAPI.Helpers;
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

        public async Task<ApiReturnResponse> GetAllUsersAsync()
        {

            ApiReturnResponse apiReturnData = new ApiReturnResponse();
            //return await _userRepository.GetUsersAsync();
            List<User> users = await _userRepository.GetUsersAsync2();

            apiReturnData.Data = users;
            apiReturnData.Message = ResponseMessages.DataFetched("Users");
            apiReturnData.Error = false;
            apiReturnData.HttpResponseCode = HttpResponseCode.OK;
            return apiReturnData;
        }

        public async Task<int> CreateUserAsync(AddUserDto addUserDto)
        {
            return await _userRepository.CreateUser(addUserDto);
        }
    }
}
