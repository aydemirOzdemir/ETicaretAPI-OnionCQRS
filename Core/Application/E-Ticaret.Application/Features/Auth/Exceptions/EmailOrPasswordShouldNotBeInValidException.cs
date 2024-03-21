using E_Ticaret.Application.Bases;

namespace E_Ticaret.Application.Features.Auth.Exceptions;

public class EmailOrPasswordShouldNotBeInValidException : BaseExceptions
{
    public EmailOrPasswordShouldNotBeInValidException() : base("Kullanıcı Adı Veya Şifre Hatalıdır.")
    {

    }
}
