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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace NMPlusApp.Api
{
    public class Startup
    {
        //private const string defaultIssuer = "http://api.nmplusapp.io";
        //private readonly static SecurityKey serverKey = new SymmetricSecurityKey(Utilities.Base64UrlDecode("superprotectsecretkey123"));

        private readonly IConfigurationRoot configuration;
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddIniFile("consumers.ini");

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();

            configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddOptions()
                .Configure<ConsumerOptions>(configuration.GetSection("Consumers"))
                .AddAuthentication()
                .AddSingleton<IConfigureOptions<JwtAuthenticationOptions>, JwtAuthenticationOptionsConfiguration>()
                .AddSingleton<IConsumerValidator, ConsumerValidator>()
                .AddSingleton(configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IOptions<JwtAuthenticationOptions> jwtOptions)
        {
            app.UseJwtBearerAuthentication(
                authenticationEndpoint: jwtOptions.Value.TokenEndpoint,
                options: new JwtBearerOptions
                {
                    TokenValidationParameters = jwtOptions.Value.Parameters,
                });

            app.Map("/ping", appContext =>
            {
                appContext.Run(context =>
                {
                    return context.Response.WriteAsync("Pong!");
                });
            });

            app.UseAuthorization();

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
