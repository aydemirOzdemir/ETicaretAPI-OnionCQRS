using E_Ticaret.Application.Bases;
using E_Ticaret.Application.Features.Auth.Rules;
using E_Ticaret.Application.Interfaces.AutoMapper;
using E_Ticaret.Application.Interfaces.Tokens;
using E_Ticaret.Application.UnitOfWorks;
using E_Ticaret.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace E_Ticaret.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler :BaseHandler, IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
{
    private readonly AuthRules authRules;
    private readonly ITokenService tokenService;
    private readonly UserManager<User> userManager;

    public RefreshTokenCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor,AuthRules authRules,ITokenService tokenService,UserManager<User> userManager) : base(mapper, unitOfWork, httpContextAccessor)
    {
        this.authRules = authRules;
        this.tokenService = tokenService;
        this.userManager = userManager;
    }

    public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
    {
        var principal = tokenService.GetPrincipalFromExpireToken(request.AccessToken);
        string email = principal.FindFirstValue(claimType:ClaimTypes.Email);
        var user = await userManager.FindByEmailAsync(email);
        var roles = await userManager.GetRolesAsync(user);
        await authRules.RefreshTokenShouldNotBeExpired(user.RefreshTokenExpireTime);
        JwtSecurityToken jwtSecurityToken = await tokenService.CreateToken(user,roles);
        string refreshToken = tokenService.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        await userManager.UpdateAsync(user);
        await userManager.UpdateSecurityStampAsync(user);
        return new()
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            RefreshToken = refreshToken
        };
    }
}
