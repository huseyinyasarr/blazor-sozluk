using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BlazorSozluk.Api.WebApi.Infrastructure.Extensions;

public static class AuthRegistration
{
    public static IServiceCollection ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var singingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:SigningKey"]));
    }
}
