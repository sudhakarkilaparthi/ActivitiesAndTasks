using ActivitiesAndTasksAPI.DTOs;
using ActivitiesAndTasksAPI.Enums;
using ActivitiesAndTasksAPI.Helpers;
using ActivitiesAndTasksAPI.Interfaces;
using Google.Apis.Auth;

namespace ActivitiesAndTasksAPI.Models
{
    public class AuthModel
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtModel _jwtModel;
        private readonly IGoogleTokenValidator _googleTokenValidator;

        public AuthModel(IUserRepository userRepository, JwtModel jwtModel, IGoogleTokenValidator googleTokenValidator)
        {
            _userRepository = userRepository;
            _jwtModel = jwtModel;
            _googleTokenValidator = googleTokenValidator;

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

        public async Task<ApiReturnResponse> GoogleLogin(GoogleLoginRequest request)
        {
            ApiReturnResponse apiReturnData = new ApiReturnResponse();

            // 1) Validate Google ID token
            GoogleJsonWebSignature.Payload? payload = await _googleTokenValidator.ValidateAsync(request.IdToken);

            if (payload == null)
            {
                apiReturnData.HttpResponseCode = HttpResponseCode.Unauthorized;
                apiReturnData.Message = "Invalid Google token";
                return apiReturnData;
            }


            GoogleLoginResponse gr = GetGoogleLoginResponseData(payload);



            if (!gr.EmailVerified)
            {
                apiReturnData.HttpResponseCode = HttpResponseCode.Unauthorized;
                apiReturnData.Message = "Email not verified";
                return apiReturnData;
            }

            int NewUserId = 0;
            User? user = await _userRepository.GetUserByEmail(gr.Email);
            if (user == null)
            {
                AddUserDto addUserDto = new AddUserDto
                {
                    FirstName = gr.GivenName,
                    LastName = gr.FamilyName,
                    Email = gr.Email,
                    Password = "NoPassword",
                    IsActive = 1
                };

                NewUserId = await _userRepository.CreateUser(addUserDto);
                if (NewUserId <= 0)
                {
                    apiReturnData.HttpResponseCode = HttpResponseCode.InternalServerError;
                    apiReturnData.Message = ResponseMessages.SomethingWentWrong;
                    return apiReturnData;
                }

                user = await _userRepository.GetUserByEmail(gr.Email);
            }

            if (user?.IsActive == false)
            {
                apiReturnData.HttpResponseCode = HttpResponseCode.Unauthorized;
                apiReturnData.Message = "Your are not authorized";
                return apiReturnData;
            }

            LoginResponse loginResponse = new LoginResponse();
            loginResponse.UserInfo = user;
            loginResponse.TokenInfo = _jwtModel.generateToken(user);

            apiReturnData.Data = loginResponse;
            apiReturnData.Message = ResponseMessages.DataFetched("Google Login");
            apiReturnData.Error = false;
            apiReturnData.HttpResponseCode = HttpResponseCode.OK;
            return apiReturnData;

        }

        private GoogleLoginResponse GetGoogleLoginResponseData(GoogleJsonWebSignature.Payload? payload)
        {

            GoogleLoginResponse googleLoginResponse = new GoogleLoginResponse
            {
                GoogleUserId = payload.Subject,
                Email = payload.Email,
                Name = payload.Name,
                GivenName = payload.GivenName,
                FamilyName = payload.FamilyName,
                Picture = payload.Picture,
                EmailVerified = payload.EmailVerified,
                Audience = payload.Audience.ToString(),
                Issuer = payload.Issuer,
                ExpirationTimeSeconds = (int)(payload.ExpirationTimeSeconds ?? 0),
                IssuedAtTimeSeconds = (int)(payload.IssuedAtTimeSeconds ?? 0),
                JwtId = payload.JwtId,
                NotBeforeTimeSeconds = (int)(payload.NotBeforeTimeSeconds ?? 0)

            };


            return googleLoginResponse;
        }

    }
}
