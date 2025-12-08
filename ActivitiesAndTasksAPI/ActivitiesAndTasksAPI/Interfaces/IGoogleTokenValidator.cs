using Google.Apis.Auth;

namespace ActivitiesAndTasksAPI.Interfaces
{
    public interface IGoogleTokenValidator
    {
        Task<GoogleJsonWebSignature.Payload?> ValidateAsync(string idToken);
    }
}
