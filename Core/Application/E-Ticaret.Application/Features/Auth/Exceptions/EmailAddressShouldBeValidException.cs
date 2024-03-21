using E_Ticaret.Application.Bases;

namespace E_Ticaret.Application.Features.Auth.Exceptions;

public class EmailAddressShouldBeValidException : BaseExceptions
{
    public EmailAddressShouldBeValidException() : base("Böyle bir emaille sahip kullanıcı bulunamamaktadır.")
    {

    }
}
