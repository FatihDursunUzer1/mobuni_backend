using Microsoft.AspNetCore.Http;
using MobUni.ApplicationCore.Entities.UserAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Web.Helpers;

namespace MobUni.ApplicationCore.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            // authorization
            var user = (User)context.HttpContext.Items["User"];
            if (user == null)
            {
                context.Result = new UnauthorizedObjectResult(value: "Unauthorized");
                context.HttpContext.Response.WriteAsync("Unauthorized");
            }

          // await context.HttpContext.Response.Body.WriteAsync();

        }
    }
}
