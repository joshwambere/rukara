using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using superhero.models;

namespace superhero.utils;

public class TokenUtil
{
    public  string GetToken(User userData)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userData.email),
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("THIS IS THE KEY CHANGE IT IF YOU CAN"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var tokenSecurity = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(tokenSecurity);
    }
}
