using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace NMPlusApp.Api.Controllers
{
    public class HomeController
    {
        public static void Get(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                var logo = @"
                            __________________________
                           |                          |
                           |  NINETY MINUTES PLUS APP |
                           |  developed by David Thâm |
                           |__________________________|
                                ";
                var result = $"App name: {logo}\n App Id: {context.User.Claims.SingleOrDefault(x => x.Type == "appid").Value}";
                await context.Response.WriteAsync(result);
            });
        }
    }
}
