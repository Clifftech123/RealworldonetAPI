using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RealworldonetAPI.Infrastructure.Context;
using System.Text;

namespace RealworldonetAPI.Presentation.Extensions
{
    public static partial class ConfigureApplicationServices
    {
        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options => options.AddPolicy("CorsPolicy", policy => policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()));
        }


        // Configure identity options for better security and user management

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            // Configuring identity options for better security and user management
            var builder = services.AddIdentityCore<IdentityUser>(o =>
            {
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = true;
                o.Password.RequireUppercase = false;
                o.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<ApplicationContext>()
              .AddDefaultTokenProviders();
        }


        // Configure JWT authentication

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["key"];

            // Ensure the secret key is securely stored and of sufficient length
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
                o.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = System.Text.Json.JsonSerializer.Serialize(new
                        {
                            message = "You are not authorized to access this resource. Please authenticate."
                        });
                        return context.Response.WriteAsync(result);
                    },
                };
            });
        }


    }
}
