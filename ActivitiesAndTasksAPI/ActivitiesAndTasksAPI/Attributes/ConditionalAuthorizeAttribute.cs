using ActivitiesAndTasksAPI.DTOs;
using ActivitiesAndTasksAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace ActivitiesAndTasksAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ConditionalAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // Resolve JwtSettings from DI
            var jwtOptionsMonitor = context.HttpContext.RequestServices.GetService(typeof(IOptionsMonitor<JwtSettings>)) as IOptionsMonitor<JwtSettings>;
            var jwt = jwtOptionsMonitor?.CurrentValue ?? new JwtSettings { Enabled = false };

            // If JWT is disabled, skip authorization
            if (!jwt.Enabled)
            {
                return;
            }

            // If enabled, enforce that the user is authenticated
            var user = context.HttpContext.User;
            if (user?.Identity == null || !user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
            }

            // If authenticated, optionally refresh/extend the JWT expiry by issuing
            // a new token with expiry = now + DurationMinutes and return it in headers.
            // This keeps the existing authentication but extends validity for the client.
            if (user?.Identity != null && user.Identity.IsAuthenticated)
            {
                var jwtModel = context.HttpContext.RequestServices.GetService(typeof(JwtModel)) as JwtModel;
                try
                {
                    if (jwtModel != null)
                    {
                        var refreshed = jwtModel.RefreshTokenFromPrincipal(user);
                        if (!string.IsNullOrEmpty(refreshed.Token))
                        {
                            context.HttpContext.Response.Headers["X-Refreshed-Token"] = refreshed.Token;
                            context.HttpContext.Response.Headers["X-Token-ExpiresAt"] = refreshed.ExpiresAt;
                        }
                    }
                }
                catch
                {
                    // Do not break the request pipeline if refresh fails — authorization already succeeded.
                }
            }

            await Task.CompletedTask;
        }
    }
}
