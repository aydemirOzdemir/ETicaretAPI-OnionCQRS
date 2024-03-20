using E_Ticaret.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Ticaret.Application.Features.Auth.Exceptions;

public class UserAlreadyExistsException:BaseExceptions
{
    public UserAlreadyExistsException():base("Böyle bir kullanıcı zaten var.")
    {
        
    }
}
