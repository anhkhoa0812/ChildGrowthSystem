using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ChildGrowth.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace ChildGrowth.API.Utils;

public class JwtUtil
{
    private JwtUtil()
    {
    }

    public static string GenerateJwtToken(User user, Tuple<string, Guid> guidClaim)
    {
        JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
        SymmetricSecurityKey secrectKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("PRN231SE1731095AESIEUNHAN12345678PRN231SE1731095AESIEUNHAN12345678PRN231SE1731095AESIEUNHAN12345678"));
        var credentials = new SigningCredentials(secrectKey, SecurityAlgorithms.HmacSha256Signature);
        List<Claim> claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(ClaimTypes.Role, user.UserType!),
        };
        if (guidClaim != null) claims.Add(new Claim(guidClaim.Item1, guidClaim.Item2.ToString()));
        var expires = DateTime.Now.AddDays(30);
        var token = new JwtSecurityToken("ChildGrowthSystem", null, claims, notBefore: DateTime.Now, expires, credentials);
        return jwtHandler.WriteToken(token);
    }
}