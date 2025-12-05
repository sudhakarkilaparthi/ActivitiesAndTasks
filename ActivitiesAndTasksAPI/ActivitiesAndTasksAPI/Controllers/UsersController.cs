using ActivitiesAndTasksAPI.DTOs;
using ActivitiesAndTasksAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ActivitiesAndTasksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
		private readonly UserModel _userModel;
		public UsersController(UserModel userModel)
        {
            _userModel = userModel;
        }

		// GET: api/students
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{

			var users = await _userModel.GetAllUsersAsync();


			return Ok();
		}


		// GET: api/students/5
		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetById(int id)
		{
			
			return Ok();
		}

		// POST: api/students
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] AddUserDto input)
		{

			var newUserId = await _userModel.CreateUserAsync(input);
			return Ok(newUserId);
		}

		// PUT: api/students/5
		[HttpPut("{id:int}")]
		public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto input)
		{
			
			return NoContent();
		}


		// DELETE: api/students/5
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = true;
			if (!result)
				return NotFound();

			return NoContent();
		}
	}
}
