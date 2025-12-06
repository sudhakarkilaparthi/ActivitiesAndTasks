using ActivitiesAndTasksAPI.DTOs;
using ActivitiesAndTasksAPI.Enums;
using ActivitiesAndTasksAPI.Helpers;
using ActivitiesAndTasksAPI.Interfaces;

namespace ActivitiesAndTasksAPI.Models
{
    public class AuthModel
    {
		private readonly IUserRepository _userRepository;
		public AuthModel(IUserRepository userRepository)
		{
			this._userRepository = userRepository;
		}

		public async Task<ApiReturnResponse> Login(LoginDto input)
		{
			ApiReturnResponse apiReturnData = new ApiReturnResponse();

			User? user = await _userRepository.GetLogin(input);
			if (user == null)
			{
				apiReturnData.Message = ResponseMessages.InvalidUsernameOrPassword;
				return apiReturnData;
			}

			apiReturnData.Data = user;
			apiReturnData.Message = ResponseMessages.DataFetched("Login");
			apiReturnData.Error = false;
			apiReturnData.HttpResponseCode = HttpResponseCode.OK;
			return apiReturnData;
		}
	}
}
