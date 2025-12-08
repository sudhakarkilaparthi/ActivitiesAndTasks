using ActivitiesAndTasksAPI.Interfaces;
using Google.Apis.Auth;

namespace ActivitiesAndTasksAPI.Repositories
{
    public class GoogleTokenValidator : IGoogleTokenValidator
    {
        private readonly string _googleClientId;

        public GoogleTokenValidator(IConfiguration configuration)
        {
            // appsettings.json: "Authentication": { "Google": { "ClientId": "..." } }
            _googleClientId = configuration["Authentication:Google:ClientId"]
                              ?? throw new Exception("Google ClientId not configured");
        }

        public async Task<GoogleJsonWebSignature.Payload?> ValidateAsync(string idToken)
        {
            if (string.IsNullOrWhiteSpace(idToken))
                throw new ArgumentException("ID token is required", nameof(idToken));

            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                // This must match your OAuth client ID from Google Cloud Console
                Audience = new[] { _googleClientId }
            };

            try
            {
                // This will:
                // 1. Download Google's public keys
                // 2. Verify signature
                // 3. Verify 'aud', 'iss', 'exp'
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
                return payload;
            }
            catch (InvalidJwtException ex)
            {
                // token invalid / expired / wrong audience etc.
                // Log ex if needed
                return null;
            }
        }
    }
}
