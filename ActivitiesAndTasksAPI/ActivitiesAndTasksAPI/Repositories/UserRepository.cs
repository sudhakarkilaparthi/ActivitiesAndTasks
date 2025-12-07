using ActivitiesAndTasksAPI.Data;
using ActivitiesAndTasksAPI.DTOs;
using ActivitiesAndTasksAPI.Enums;
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

		private readonly AppDbContext _dbContext;

		public UserRepository(AppDbContext context)
		{
			_dbContext = context;
		}


		public async Task<List<User>> GetUsersAsync2()
		{
			var users = new List<User>();

			// No parameters for this SP → pass null
			using DbDataReader reader = await _dbContext.DbExecuteReaderAsync(SPs.spGetAllUsers);

			while (await reader.ReadAsync())
			{
				//var user = new User
				//{
				//	UserId = reader.DDRGetInt32("UserId"),
				//	FirstName = reader.DDRGetString("FirstName"),
				//	LastName = reader.DDRGetString("LastName"),
				//	Email = reader.DDRGetString("Email")
				//};
				//users.Add(user);

				users.Add(MapUser(reader));
			}

			return users;
		}

		public async Task<List<User>> GetUsersAsync()
		{
			List<User> users = await _dbContext.DbExecuteReaderMapAsync(SPs.spGetAllUsers, null, MapUser);
			return users;
		}

        public async Task<int> CreateUser(AddUserDto addUserDto)
        {

			var newUserIdParam = new SqlParameter("@NewUserId", SqlDbType.Int)
			{
				Direction = ParameterDirection.Output
			};

			List<SqlParameter> parameters = new List<SqlParameter>
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
			var effectedRows = await _dbContext.DbExecuteNonQueryAsync(SPs.spCreateUser, parameters);

			// Retrieve the new user id from out param
			int newUserIdOut =  (int)newUserIdParam.Value;

			return newUserIdOut;
		}

		public async Task<User?> GetLogin(LoginDto input)
		{

			List<SqlParameter> parameters = new List<SqlParameter>
			{
				new SqlParameter("@Email", input.Email),
				new SqlParameter("@PasswordHash", input.Password),
			};

			using DbDataReader reader = await _dbContext.DbExecuteReaderAsync(SPs.spUserLogin, parameters);

			// Read only the FIRST ROW
			if (await reader.ReadAsync())
			{
				return MapUser(reader);
			}

			return null;
		}

		private User MapUser(DbDataReader reader)
		{
			return new User
			{
				UserId = reader.DDRGetInt32("UserId"),
				FirstName = reader.DDRGetString("FirstName"),
				LastName = reader.DDRGetString("LastName"),
				Email = reader.DDRGetString("Email"),
				RoleId = reader.DDRGetInt32("RoleId"),
				RoleName = reader.DDRGetString("RoleName"),
				IsActive = reader.DDRGetBoolean("IsActive"),
				CreatedOn = reader.DDRGetDateTime("CreatedOn")
			};
		}

       
    }
}
