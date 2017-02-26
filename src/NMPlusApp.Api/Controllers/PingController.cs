using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace NMPlusApp.Api.Controllers
{
    public class PingController
    {
        public static void Get(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Pong!");
            });
        }
    }
}
