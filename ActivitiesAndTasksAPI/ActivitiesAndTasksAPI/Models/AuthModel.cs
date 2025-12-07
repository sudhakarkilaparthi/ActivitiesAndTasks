using ActivitiesAndTasksAPI.DTOs;
using ActivitiesAndTasksAPI.Enums;
using ActivitiesAndTasksAPI.Helpers;
using ActivitiesAndTasksAPI.Interfaces;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ActivitiesAndTasksAPI.Models
{
    public class AuthModel
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtModel _jwtModel;


		public AuthModel(IUserRepository userRepository, JwtModel jwtModel )
        {
            _userRepository = userRepository;
            _jwtModel = jwtModel;
            
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

			LoginResponse loginResponse = new LoginResponse();
			loginResponse.UserInfo = user;
			loginResponse.TokenInfo = _jwtModel.generateToken(user);

			apiReturnData.Data = loginResponse;
            apiReturnData.Message = ResponseMessages.DataFetched("Login");
            apiReturnData.Error = false;
            apiReturnData.HttpResponseCode = HttpResponseCode.OK;
            return apiReturnData;
        }
    }
}
