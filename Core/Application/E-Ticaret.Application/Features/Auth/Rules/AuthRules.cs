using E_Ticaret.Application.Bases;
using E_Ticaret.Application.Features.Auth.Exceptions;
using E_Ticaret.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Application.Features.Auth.Rules;

public class AuthRules:BaseRules
{
    public Task UserShouldNotBeExist(User? user)
    {
        if (user is not null) throw new UserAlreadyExistsException();
        return Task.CompletedTask;
    }
}
