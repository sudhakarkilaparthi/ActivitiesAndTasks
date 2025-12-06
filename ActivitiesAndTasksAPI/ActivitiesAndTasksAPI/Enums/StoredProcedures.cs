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

		#endregion

		#region Auth

		/// <summary>
		/// Create a User.
		/// </summary>
		[StringValue("sp_UserLogin")]
		spUserLogin,

		#endregion
	}
}
