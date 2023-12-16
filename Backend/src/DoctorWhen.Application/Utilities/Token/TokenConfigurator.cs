using DoctorWhen.Application.Interfaces;
using DoctorWhen.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DoctorWhen.Application.Utilities.Token;
public class TokenConfigurator : ITokenConfigurator
{
    private readonly string _tokenSecret;
    private readonly UserManager<User> _userManager;

    public TokenConfigurator(IConfiguration config, UserManager<User> userManager)
    {
        _userManager = userManager;
        _tokenSecret = config.GetRequiredSection("Configurations:Jwt:Token").Value;
    }

    public async Task<string> GetToken(User user)
    {
        try
        {
            // as claims são propriedades do usuário relacionadas à segurança do System
            // não necessariamente tem relação com Identity
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
            };

            // responsabilidades do usuário (admin, moderador, etc).
            if (_userManager != null)
            {
                var roles = await _userManager.GetRolesAsync(user);

                // adiciona as roles dentro da lista de claims
                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            }

            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSecret));

            // conjunto de informações necessárias para assinar o Token (Sign)
            var creds = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature);

            // o description está utilizando as claims, a data de expiração, e a key do token encriptada)
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            // definindo que o formato do token será Json Web Token (jwt)
            var tokenHandler = new JwtSecurityTokenHandler();

            // o manipulador cria o token baseado na descrição
            var securityToken = tokenHandler.CreateToken(tokenDescription);

            var result = tokenHandler.WriteToken(securityToken);

            return result;
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
            return "Error";
        }
    }
}
