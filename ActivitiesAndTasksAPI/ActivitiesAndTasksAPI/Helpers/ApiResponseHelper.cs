using ActivitiesAndTasksAPI.DTOs;
using ActivitiesAndTasksAPI.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ActivitiesAndTasksAPI.Helpers
{
    public static class ApiResponseHelper
    {
        public static IActionResult BuildResponse(ApiReturnResponse response, ControllerBase controller)
        {
            switch (response.HttpResponseCode)
            {
                case HttpResponseCode.OK:
                    return controller.Ok(response);
                case HttpResponseCode.Created:
                    return controller.StatusCode((int)HttpResponseCode.Created, response);
                case HttpResponseCode.Accepted:
                    return controller.Accepted(response);
                case HttpResponseCode.BadRequest:
                    return controller.BadRequest(response);
                case HttpResponseCode.NotFound:
                    return controller.NotFound(response);
                case HttpResponseCode.InternalServerError:
                    return controller.StatusCode((int)HttpResponseCode.InternalServerError, response);
                default:
                    return controller.StatusCode((int)response.HttpResponseCode, response); // fallback
            }
        }
    }
}
