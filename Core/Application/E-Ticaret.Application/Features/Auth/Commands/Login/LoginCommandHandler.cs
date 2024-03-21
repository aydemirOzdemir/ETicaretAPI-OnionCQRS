using E_Ticaret.Application.Bases;
using E_Ticaret.Application.Features.Auth.Rules;
using E_Ticaret.Application.Interfaces.AutoMapper;
using E_Ticaret.Application.Interfaces.Tokens;
using E_Ticaret.Application.UnitOfWorks;
using E_Ticaret.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace E_Ticaret.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler : BaseHandler,IRequestHandler<LoginCommandRequest, LoginCommandResponse>
{
    private readonly UserManager<User> userManager;
    private readonly AuthRules authRules;

    private readonly ITokenService tokenService;
    private readonly IConfiguration configuration;

    public LoginCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor,UserManager<User> userManager,AuthRules authRules,ITokenService tokenService,
        IConfiguration configuration) : base(mapper, unitOfWork, httpContextAccessor)
    {
        this.userManager = userManager;
        this.authRules = authRules;
        
        this.tokenService = tokenService;
        this.configuration = configuration;
    }

    public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
    {
      User? user=await userManager.FindByEmailAsync(request.Email);
        bool checkPassword = await userManager.CheckPasswordAsync(user,request.Password);

        await authRules.EmailOrPasswordShouldNotBeInValid(user,checkPassword);
        IList<string> roles = await userManager.GetRolesAsync(user);
        JwtSecurityToken token = await tokenService.CreateToken(user,roles);
        string refreshToken = tokenService.GenerateRefreshToken();
      _ =  int.TryParse(configuration["JWT:RefreshTokenValidityInDays"],out int refreshTokenValidityInDays);
        user.RefreshToken = refreshToken;
        await userManager.UpdateAsync(user);
        await userManager.UpdateSecurityStampAsync(user);
        var _token = new JwtSecurityTokenHandler().WriteToken(token);        
user.RefreshTokenExpireTime=DateTime.Now.AddDays(refreshTokenValidityInDays);
        await userManager.SetAuthenticationTokenAsync(user,"Default","Accses",_token);
        return new()
        {
            Token=_token,
            RefreshToken=refreshToken,
            Expration=token.ValidTo
        };
    }
}
