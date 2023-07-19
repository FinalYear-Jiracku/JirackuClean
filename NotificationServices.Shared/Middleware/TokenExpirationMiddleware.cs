using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NotificationServices.Shared.Middleware
{
    public class TokenExpirationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenExpirationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var claimsIdentity = (ClaimsIdentity)context.User.Identity;

                var expirationClaim = claimsIdentity.FindFirst("exp");

                if (expirationClaim != null)
                {
                    var expirationTimeString = expirationClaim.Value;
                    if (long.TryParse(expirationTimeString, out long expirationTimeEpoch))
                    {
                        var expirationTimeUtc = DateTimeOffset.FromUnixTimeSeconds(expirationTimeEpoch).UtcDateTime;
                        if (expirationTimeUtc <= DateTime.UtcNow)
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            await context.Response.WriteAsync("Token has expired.");
                            return;
                        }
                    }
                }
            }

            await _next(context);
        }
    }
}
