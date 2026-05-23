using InternshipPractice.Application.Interfaces.Services;
using InternshipPractice.Infrastructure.Options;
using KDS.Primitives.FluentResult;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InternshipPractice.Infrastructure.Services;

public class JwtService(IOptions<JwtOptions> options): IJwtService
{
    public async Task<Result<string>> GenerateToken(Guid userId, string roleCode)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(options.Value.SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("UserId", userId.ToString()),
                new Claim("RoleCode", roleCode)
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<Result<(Guid, string)>> ValidateToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(options.Value.SecretKey);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
            var userIdClaim = principal.FindFirst("UserId");
            var roleCodeClaim = principal.FindFirst("ConnectionId");
            var iin = principal.FindFirst("Iin");

            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return Result.Failure<(Guid, string)>(new Error("500", "Некорректный токен: отсутствует UserId."));
            }

            if (roleCodeClaim == null || string.IsNullOrWhiteSpace(roleCodeClaim.Value))
            {

                return Result.Failure<(Guid, string)>(new Error("BadRequest", "Некорректный токен: отсутствует RoleCode."));
            }
            return Result.Success((userId, roleCodeClaim.Value));
        }
        catch (SecurityTokenException)
        {
            return Result.Failure<(Guid, string)>(new Error("BadRequest", "Некорректный токен."));
        }
        catch (Exception)
        {
            return Result.Failure<(Guid, string)>(new Error("ServiceUnavailable", "Произошла ошибка при обработке токена."));
        }
    }
}
