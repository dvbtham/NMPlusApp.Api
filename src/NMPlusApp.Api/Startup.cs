using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace NMPlusApp.Api
{
    public class Startup
    {
        private const string defaultIssuer = "http://api.nmplusapp.io";
        private readonly static SecurityKey serverKey = new SymmetricSecurityKey(Utilities.Base64UrlDecode("superprotectsecretkey123"));

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IClientValidator, ClientValidator>()
                    .AddAuthentication();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateActor = false,
                ValidIssuer = defaultIssuer,
                IssuerSigningKey = serverKey
            };

            app.UseJwtBearerAuthentication(
                authenticationEndpoint: "/jwt/token",
                options: new JwtBearerOptions
                {
                    TokenValidationParameters = tokenValidationParameters,
                });

            app.Map("/ping", appContext =>
            {
                appContext.Run(context =>
                {
                    return context.Response.WriteAsync("Pong!");
                });
            });

            app.UseAuthorization();

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
