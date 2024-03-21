namespace E_Ticaret.Application.Features.Auth.Commands.Login;

public class LoginCommandResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime Expration { get; set; }
}
