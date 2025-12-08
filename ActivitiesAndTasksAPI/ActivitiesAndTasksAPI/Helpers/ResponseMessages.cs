namespace ActivitiesAndTasksAPI.Helpers
{
    public static class ResponseMessages
    {
        public const string InvalidInputData = "Invalid Input Data!.";
        public const string InvalidUsernameOrPassword = "Invalid username or password! please try again";
        public const string UnauthorizedAccess = "You are not authorized to perform this action.";
        public const string SomethingWentWrong = "Something went wront!";
        public const string UserRegisterSuccess = "User Registered Successfully";
        public const string NewAndConfirmPasswordNotMathched = "New Password and Confirm new password not matched!";
        public const string InvalidCurrentPassword = "Invalid Current password!";
        public const string StartTimeIsGreaterThanEndTime = "Start Time should not be same or greater than End Time";
        public const string UserBlockedConflictTimes = "Selected Time range is already blocked.";
        public static string CustomMessage(string paramMessage) => $"{paramMessage}";
        public static string DataFetched(string paramMessage) => $"{paramMessage} data fetched successfully.";
        public static string IsNotValid(string paramMessage) => $"{paramMessage} is not valid!";
        public static string IsRequired(string paramMessage) => $"{paramMessage} is Required!";
        public static string IsNotValidDateformat(string paramMessage) => $"{paramMessage} format is not valid!, required date format MM/dd/YYYY";
        public static string Added(string paramMessage) => $"Added {paramMessage} details!";
        public static string Updated(string paramMessage) => $"Updated {paramMessage} details!";
        public static string Saved(string paramMessage) => $"Saved {paramMessage} details!";
        public static string Deleted(string paramMessage) => $"Deleted {paramMessage} details!";
        public static string Changed(string paramMessage) => $"{paramMessage} Changed Sucessfully!";
        public static string NotFound(string paramMessage) => $"{paramMessage} not found!";
        public static string NotAvailable(string paramMessage) => $"{paramMessage} not available!";
        public static string Successfull(string paramMessage) => $"{paramMessage} successfull.";
        public static string UserAlreadyExisted(string paramMessage) => $"User already existed with email address {paramMessage}";
        public static string AlreadyExisted(string paramValue) => $"already existed the {paramValue}";
    }
}
