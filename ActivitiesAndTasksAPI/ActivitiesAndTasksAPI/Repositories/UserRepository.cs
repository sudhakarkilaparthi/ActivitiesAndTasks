using ActivitiesAndTasksAPI.Data;
using ActivitiesAndTasksAPI.DTOs;
using ActivitiesAndTasksAPI.Helpers;
using ActivitiesAndTasksAPI.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace ActivitiesAndTasksAPI.Repositories
{
    public class UserRepository : IUserRepository
    {

		private readonly AppDbContext _context;

		public UserRepository(AppDbContext context)
		{
			_context = context;
		}



		public async Task<List<User>> GetUsersAsync2()
		{
			var users = new List<User>();

			// No parameters for this SP → pass null
			using DbDataReader reader = await _context.DbExecuteReaderAsync("sp_GetAllUsers");

			while (await reader.ReadAsync())
			{
				//var user = new User
				//{
				//	UserId = reader.DREGetInt32("UserId"),
				//	FirstName = reader.DREGetString("FirstName"),
				//	LastName = reader.DREGetString("LastName"),
				//	Email = reader.DREGetString("Email")
				//};
				//users.Add(user);

				//users.Add(MapUser(reader));
			}

			return users;
		}

		public async Task<List<User>> GetUsersAsync()
		{
			List<User> users = await _context.DbExecuteReaderMapAsync("sp_GetAllUsers", null, MapUser);
			return users;
		}

		private User MapUser(DbDataReader reader)
		{
			return new User
			{
				UserId = reader.DREGetInt32("UserId"),
				FirstName = reader.DREGetString("FirstName"),
				LastName = reader.DREGetString("LastName"),
				Email = reader.DREGetString("Email")
			};
		}

        public async Task<int> CreateUser(AddUserDto addUserDto)
        {

			var newUserIdParam = new SqlParameter("@NewUserId", SqlDbType.Int)
			{
				Direction = ParameterDirection.Output
			};

			var parameters = new List<SqlParameter>
				{
					new SqlParameter("@FirstName", addUserDto.FirstName),
					new SqlParameter("@LastName", (object?)addUserDto.LastName ?? DBNull.Value),
					new SqlParameter("@Email", addUserDto.Email),
					new SqlParameter("@PasswordHash", addUserDto.Password),
					new SqlParameter("@Phone", (object?)addUserDto.Phone ?? DBNull.Value),
					new SqlParameter("@IsActive", addUserDto.IsActive),
					newUserIdParam, // OUTPUT PARAMETER
				};

			// Call your helper method
			var effectedRows = await _context.DbExecuteNonQueryAsync("sp_CreateUser", parameters);

			// Retrieve the new user id from out param
			int newUserIdOut =  (int)newUserIdParam.Value;

			return newUserIdOut;
		}
    }
}
