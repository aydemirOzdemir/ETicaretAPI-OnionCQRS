using E_Ticaret.Application.Bases;

namespace E_Ticaret.Application.Features.Auth.Exceptions;

public class RefreshTokenShouldNotBeExpiredException : BaseExceptions
{
    public RefreshTokenShouldNotBeExpiredException() : base("Oturum Süresi Sona ermiştir lütfen tekrar giriş yapınız.")
    {

    }
}
