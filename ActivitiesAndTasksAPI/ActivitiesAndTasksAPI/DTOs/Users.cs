using System.ComponentModel.DataAnnotations;

namespace ActivitiesAndTasksAPI.DTOs
{
    public class AddUserDto
	{
        
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
	}


	public class UpdateUserDto
	{

		[Required(ErrorMessage = "First Name is required")]
		public string FirstName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Last Name is required")]
		public string LastName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Email format is not valid")]
		public required string Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		public string Password { get; set; } = string.Empty;
	}


}
