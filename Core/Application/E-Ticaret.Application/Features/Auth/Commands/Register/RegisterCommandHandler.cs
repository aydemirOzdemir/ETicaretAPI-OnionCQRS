using E_Ticaret.Application.Bases;
using E_Ticaret.Application.Features.Auth.Rules;
using E_Ticaret.Application.Interfaces.AutoMapper;
using E_Ticaret.Application.UnitOfWorks;
using E_Ticaret.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler : BaseHandler, IRequestHandler<RegisterCommandRequest, Unit>
{
    private readonly AuthRules rules;
    private readonly UserManager<User> userManager;
    private readonly RoleManager<Role> roleManager;

    public RegisterCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor,AuthRules rules,UserManager<User> userManager, RoleManager<Role> roleManager) : base(mapper, unitOfWork, httpContextAccessor)
    {
        this.rules = rules;
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    public async Task<Unit> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
    {
        await rules.UserShouldNotBeExist(await userManager.FindByEmailAsync(request.Email));
        User user = mapper.Map<User, RegisterCommandRequest>(request);
        user.UserName = request.Email;
        user.SecurityStamp=Guid.NewGuid().ToString();
        IdentityResult result= await userManager.CreateAsync(user,request.Password);
        if (result.Succeeded)
        {
            if (await roleManager.RoleExistsAsync("user"))
                await roleManager.CreateAsync(new Role
                {
                    Id=Guid.NewGuid(),
                    Name="user",
                    NormalizedName="USER",
                    ConcurrencyStamp=Guid.NewGuid().ToString()
                });
            await userManager.AddToRoleAsync(user, "user");
        }
        return Unit.Value;
    }
}
