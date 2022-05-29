using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MobUni.ApplicationCore.Errors;
using MobUni.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobUni.ApplicationCore.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next, IUnitOfWork unitOfWork)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateJwtToken(token);
            if (userId != null)
            {
                context.Items["UserId"] = userId;
            }
            else
                context.Request.Headers["Authorization"] = String.Empty;
            await _next(context);
        }
    }
}
