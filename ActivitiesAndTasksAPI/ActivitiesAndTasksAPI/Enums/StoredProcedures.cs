using ActivitiesAndTasksAPI.Helpers;

namespace ActivitiesAndTasksAPI.Enums
{
    public enum SPs
	{
		#region User

		/// <summary>
		/// Fetch All Users.
		/// </summary>
		[StringValue("sp_GetAllUsers")]
		spGetAllUsers,

		/// <summary>
		/// Create a User.
		/// </summary>
		[StringValue("sp_CreateUser")]
		spCreateUser,

		/// <summary>
		/// Get User by email
		/// </summary>
		[StringValue("sp_GetUserByEmail")]
		spGetUserByEmail,

		#endregion

		#region Auth

		/// <summary>
		/// Login user by email and password.
		/// </summary>
		[StringValue("sp_UserLogin")]
		spUserLogin,

		

		#endregion
	}
}
