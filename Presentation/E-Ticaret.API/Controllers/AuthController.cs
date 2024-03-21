using E_Ticaret.Application.Features.Auth.Commands.Login;
using E_Ticaret.Application.Features.Auth.Commands.RefreshToken;
using E_Ticaret.Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator mediator;

    public AuthController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterCommandRequest request)
    {
        await mediator.Send(request);
        return Created();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginCommandRequest request)
    {
        
        return Ok(await mediator.Send(request));
    }
    [HttpPost]
    public async Task<IActionResult> RefreshToken(RefreshTokenCommandRequest request)
    {

        return Ok(await mediator.Send(request));
    }
}
