using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ActivitiesAndTasksAPI.DTOs
{
    
	public class User
	{
		//[JsonPropertyName("UserId")]
		public required int UserId { get; set; }

		//[JsonPropertyName("FirstName")]
		public required string FirstName { get; set; }

		//[JsonPropertyName("LastName")]
		public required string LastName { get; set; }

		//[JsonPropertyName("Email")]
		public required string Email { get; set; }
		//public required string Password { get; set; }

		public string Token { get; set; } = string.Empty;
		public string ExpiresAt { get; set; } = string.Empty;
		public string Role { get; set; } = string.Empty;


	}

	public class AddUserDto
	{
		[JsonPropertyName("FirstName")]
		public required string FirstName { get; set; }
		[JsonPropertyName("LastName")]
		public string LastName { get; set; } = string.Empty;

		[JsonPropertyName("Phone")]
		public string Phone { get; set; } = string.Empty;

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Email format is not valid")]
		[JsonPropertyName("Email")]
		public required string Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[JsonPropertyName("Password")]
		public required string Password { get; set; }

		[JsonPropertyName("IsActive")]
		public int IsActive { get; set; }
	}


	public class UpdateUserDto
	{

		[Required(ErrorMessage = "First Name is required")]
		[JsonPropertyName("FirstName")]
		public required string FirstName { get; set; }

		[Required(ErrorMessage = "Last Name is required")]
		[JsonPropertyName("LastName")]
		public required string LastName { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Email format is not valid")]
		[JsonPropertyName("Email")]
		public required string Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[JsonPropertyName("Password")]
		public required string Password { get; set; }
	}

	public class LoginDto
	{
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Email format is not valid")]
		[JsonPropertyName("email")]
		public required string Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[JsonPropertyName("password")]
		public required string Password { get; set; }
	}

	public class RegisterDto
	{
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Email format is not valid")]
		[JsonPropertyName("Email")]
		public required string Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[JsonPropertyName("Password")]
		public required string Password { get; set; }
	}
}
