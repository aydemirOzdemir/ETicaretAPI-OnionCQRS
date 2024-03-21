using FluentValidation;

namespace E_Ticaret.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandValidator:AbstractValidator<RefreshTokenCommandRequest>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.AccessToken).NotEmpty();  
        RuleFor(x => x.RefreshToken).NotEmpty();  
    }
}