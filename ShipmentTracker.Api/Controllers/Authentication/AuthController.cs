using Microsoft.AspNetCore.Mvc;
using ShipmentTracker.Domain.Authentication;

namespace ShipmentTracker.Api.Controllers.Authentication;

public record RegisterRequest(string Username, string Password);
public record LoginRequest(string Username, string Password);

[ApiController]
[Route("[controller]")]
public class AuthController(IAuthenticationService authenticationService) : ControllerBase
{
    [HttpPost("register", Name = "Register")]
    public async Task<IResult> Register([FromBody] RegisterRequest request)
    {
        var jwt = await authenticationService.Register(request.Username, request.Password); 
        
        return Results.Ok(new { jwt });
    }

    [HttpPost("login", Name = "Login")]
    public async Task<IResult> Login([FromBody] LoginRequest request)
    {
        var jwt = await authenticationService.Login(request.Username, request.Password);

        return Results.Ok(new { jwt });
    }
}