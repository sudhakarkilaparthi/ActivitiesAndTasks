using System.ComponentModel.DataAnnotations;

namespace ActivitiesAndTasksAPI.DTOs
{
    
	public class User
	{
		public required int UserId { get; set; }
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required string Email { get; set; }
		//public required string Password { get; set; }

	}

	public class AddUserDto
	{
		public required string FirstName { get; set; }
		public string LastName { get; set; } = string.Empty;

		public string Phone { get; set; } = string.Empty;

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Email format is not valid")]
		public required string Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		public required string Password { get; set; }
		public int IsActive { get; set; }
	}


	public class UpdateUserDto
	{

		[Required(ErrorMessage = "First Name is required")]
		public required string FirstName { get; set; }

		[Required(ErrorMessage = "Last Name is required")]
		public required string LastName { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Email format is not valid")]
		public required string Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		public required string Password { get; set; }
	}


}
