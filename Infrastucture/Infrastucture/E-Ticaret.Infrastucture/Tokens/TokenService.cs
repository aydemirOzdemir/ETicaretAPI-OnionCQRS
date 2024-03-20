using E_Ticaret.Application.Interfaces.Tokens;
using E_Ticaret.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Infrastucture.Tokens;

public class TokenService : ITokenService
{
    private readonly TokenSetting tokenSetting;
    private readonly UserManager<User> userManager;

    public TokenService(IOptions<TokenSetting> options,UserManager<User> userManager)
    {
        this.tokenSetting = options.Value;
        this.userManager = userManager;
    }
    public async Task<JwtSecurityToken> CreateToken(User user, IList<string> roles)
    {
        List<Claim> claims =new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email,user.Email),
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
        };
        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSetting.Secret));
        JwtSecurityToken token = new JwtSecurityToken(
            issuer:tokenSetting.Issuer,
            audience:tokenSetting.Audience,
            expires:DateTime.Now.AddMinutes(tokenSetting.TokenValidityInMinutes),
            claims:claims,
            signingCredentials:new SigningCredentials(key,SecurityAlgorithms.HmacSha256)
            );
        
        await userManager.AddClaimsAsync(user, claims);

        return token;
        
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var reg=RandomNumberGenerator.Create();
        reg.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);

    }

    public ClaimsPrincipal? GetPrincipalFromExpireToken(string? token)
    {
        TokenValidationParameters tokenValidationParameters = new()
        {
            ValidateIssuer=false,
            ValidateAudience=false,
        ValidateIssuerSigningKey=true,
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSetting.Secret)),
        ValidateLifetime=false
        };
        JwtSecurityTokenHandler tokenHandler = new();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Token Bulunamadı");
        return principal;
    }
}
