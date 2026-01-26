using Microsoft.AspNetCore.Mvc;
using ShipmentTracker.Application.Authentication;
using ShipmentTracker.Domain.Authentication;

namespace ShipmentTracker.Api.Controllers.Authentication;

public record RegisterRequest(string Username, string Password);
public record LoginRequest(string Username, string Password);

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("register", Name = "Register")]
    public async Task<IResult> Register([FromBody] RegisterRequest request, RegistrationHandler registrationHandler)
    {
        var jwt = await registrationHandler.Handle(request.Username, request.Password); 
        
        return Results.Ok(new { jwt });
    }

    [HttpPost("login", Name = "Login")]
    public async Task<IResult> Login([FromBody] LoginRequest request, LoginHandler loginHandler)
    {
        var jwt = await loginHandler.Handle(request.Username, request.Password);

        return Results.Ok(new { jwt });
    }
}