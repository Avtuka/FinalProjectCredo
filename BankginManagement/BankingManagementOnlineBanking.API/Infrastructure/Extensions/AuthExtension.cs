using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BankingManagementOnlineBanking.API.Infrastructure.Extensions
{
    public static class AuthExtension
    {
        public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, IConfiguration option)
        {
            var key = Encoding.ASCII.GetBytes(option.GetSection("JWTConfiguration").GetSection("Secret").Value!);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "OnlineBanking",
                    ValidAudience = "localhost"
                });
            return services;
        }
    }
}