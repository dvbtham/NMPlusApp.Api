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
using NMPlusApp.Api.Controllers;
using NMPlusApp.Api.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

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

            services
               .AddMvcCore(options =>
               {
                   var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                   options.Filters.Add(new AuthorizeFilter(policy));
                   options.OutputFormatters.Insert(0, new PingPongOutputFormatter());
               })
               .AddAuthorization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IOptions<JwtAuthenticationOptions> jwtOptions)
        {
            app.UseJwtBearerAuthenticationWithTokenIssuer();
            app.UseMvc();
        }
    }
}
