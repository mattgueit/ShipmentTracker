using Microsoft.AspNetCore.Mvc;
using ShipmentTracker.Application.Authentication.LoginUser;
using ShipmentTracker.Application.Authentication.RegisterUser;

namespace ShipmentTracker.Api.Controllers.Authentication;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("register", Name = "Register")]
    public async Task<IResult> Register([FromBody] RegisterUserCommand request, RegisterUserHandler registerUserHandler)
    {
        var accessToken = await registerUserHandler.Handle(request); 
        
        return Results.Ok(new { accessToken });
    }

    [HttpPost("login", Name = "Login")]
    public async Task<IResult> Login([FromBody] LoginUserCommand request, LoginUserHandler loginUserHandler)
    {
        var accessToken = await loginUserHandler.Handle(request);

        return Results.Ok(new { accessToken });
    }
}