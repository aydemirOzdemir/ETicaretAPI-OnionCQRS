using E_Ticaret.Application.Bases;
using E_Ticaret.Application.Features.Auth.Rules;
using E_Ticaret.Application.Interfaces.AutoMapper;
using E_Ticaret.Application.UnitOfWorks;
using E_Ticaret.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace E_Ticaret.Application.Features.Auth.Commands.Revoke;

public class RevokeCommandHandler : BaseHandler, IRequestHandler<RevokeCommandRequest, Unit>
{
    private readonly AuthRules authRules;
    private readonly UserManager<User> userManager;

    public RevokeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor,AuthRules authRules,UserManager<User> userManager) : base(mapper, unitOfWork, httpContextAccessor)
    {
        this.authRules = authRules;
        this.userManager = userManager;
    }

    public async Task<Unit> Handle(RevokeCommandRequest request, CancellationToken cancellationToken)
    { User? user= await userManager.FindByEmailAsync(request.Email);
        await authRules.EmailAddressShouldBeValid(user);
        user.RefreshToken = null;
        await userManager.UpdateAsync(user);
        await userManager.UpdateSecurityStampAsync(user);
        return Unit.Value;
    }
}
