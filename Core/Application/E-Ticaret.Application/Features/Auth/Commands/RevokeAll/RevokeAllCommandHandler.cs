﻿using E_Ticaret.Application.Bases;
using E_Ticaret.Application.Features.Auth.Rules;
using E_Ticaret.Application.Interfaces.AutoMapper;
using E_Ticaret.Application.UnitOfWorks;
using E_Ticaret.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Application.Features.Auth.Commands.RevokeAll;

public class RevokeAllCommandHandler : BaseHandler, IRequestHandler<RevokeAllCommandRequest, Unit>
{
    private readonly AuthRules authRules;
    private readonly UserManager<User> userManager;

    public RevokeAllCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor,AuthRules authRules,UserManager<User> userManager) : base(mapper, unitOfWork, httpContextAccessor)
    {
        this.authRules = authRules;
        this.userManager = userManager;
    }

    public async Task<Unit> Handle(RevokeAllCommandRequest request, CancellationToken cancellationToken)
    {
     var users=await userManager.Users.ToListAsync(cancellationToken);
        foreach(var user in users)
        {
            user.RefreshToken=null,
                await userManager.UpdateAsync(user);
            await userManager.UpdateSecurityStampAsync(user);

        }
        return Unit.Value;
    }
}
