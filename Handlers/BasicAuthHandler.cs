using System.Net;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace LibretaContactosAPI.Handlers
{
    public class BasicAuthHandler
    {
        private readonly RequestDelegate _next;
        private readonly string ?user;
        private readonly string ?pswd;

        public BasicAuthHandler(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            user = configuration["AuthCredentials:userValue"];
            pswd = configuration["AuthCredentials:pswdValue"];
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                var authHeader = context.Request.Headers["Authorization"].ToString();

                if (authHeader.StartsWith("Basic "))
                {
                    var encodedCredentials = authHeader.Substring("Basic ".Length).Trim();
                    var credentialBytes = Convert.FromBase64String(encodedCredentials);
                    var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);

                    var username = credentials[0];
                    var password = credentials[1];

                    if (IsValidUser(username, password))
                    {
                        await _next(context);
                        return;
                    }
                }
            }

            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.Headers["WWW-Authenticate"] = "Basic realm=\"myrealm\"";
            await context.Response.WriteAsync("Unauthorized");
        }

        private bool IsValidUser(string username, string password)
        {
            return username == user && password == pswd;
        }
    }
}
