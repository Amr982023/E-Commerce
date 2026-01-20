using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace E_commerce_Infrastructure.Repositories.JWT
{
    public static class JwtServiceRegisteration
    {
        public static IServiceCollection AddJwtServices(this IServiceCollection Services,IConfiguration Configuration ,string Section)
        {
            var jwtOptions = Configuration.GetSection(Section).Get<JwtOptions>() ?? throw new InvalidOperationException("JwtOptions is not configured");

            Services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,

                    ClockSkew = TimeSpan.Zero
                };
            });




            return Services;
        }
    }
}
