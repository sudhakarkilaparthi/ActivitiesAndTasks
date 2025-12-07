using ActivitiesAndTasksAPI.DTOs;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ActivitiesAndTasksAPI.Models
{
    public class JwtModel
    {

		private readonly IOptionsMonitor<JwtSettings> _jwtOptionsMonitor;
		public JwtModel(IOptionsMonitor<JwtSettings> jwtOptionsMonitor)
        {
			_jwtOptionsMonitor = jwtOptionsMonitor;
		}


        public JwtInfo generateToken(User user)
        {
			
			JwtInfo jwtInfo = new JwtInfo();

			// create claims
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
				new Claim(ClaimTypes.Name, user.FirstName ?? string.Empty),
				new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
			};

			var _jwtSettings = _jwtOptionsMonitor.CurrentValue;
			if (_jwtSettings.Enabled)
			{
				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
				var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

				var tokenHandler = new JwtSecurityTokenHandler();
				var tokenDescriptor = new SecurityTokenDescriptor
				{
					Subject = new ClaimsIdentity(claims),
					Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationMinutes),
					Issuer = _jwtSettings.Issuer,
					Audience = _jwtSettings.Audience,
					SigningCredentials = creds
				};

				var token = tokenHandler.CreateToken(tokenDescriptor);
				var tokenString = tokenHandler.WriteToken(token);

				jwtInfo.Token = tokenString;
				jwtInfo.ExpiresAt = tokenDescriptor.Expires?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") ?? string.Empty; //yyyy-MM-ddTHH:mm:ssZ
			}
			

			return jwtInfo;
		}

		// Create a new token for an authenticated principal and extend expiry
		public JwtInfo RefreshTokenFromPrincipal(ClaimsPrincipal principal)
		{
			if (principal == null || principal.Identity == null || !principal.Identity.IsAuthenticated)
			{
				return new JwtInfo();
				
			}

			var idClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			int userId = 0;
			int.TryParse(idClaim, out userId);

			var firstName = principal.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
			var email = principal.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;

			User user = new User
			{
				UserId = userId,
				FirstName = firstName,
				LastName = string.Empty,
				Email = email
			};

			return generateToken(user);
		}
	}
}
