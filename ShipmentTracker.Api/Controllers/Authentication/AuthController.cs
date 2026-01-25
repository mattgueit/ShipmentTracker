using Microsoft.AspNetCore.Mvc;
using ShipmentTracker.Domain.Authentication;

namespace ShipmentTracker.Controllers.Authentication;

public record RegisterRequest(string Username, string Password);
public record LoginRequest(string Username, string Password);

[ApiController]
[Route("[controller]")]
public class AuthController(IAuthenticationService authenticationService) : ControllerBase
{
    [HttpPost("register", Name = "Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            var result = await authenticationService.Register(request.Username, request.Password);
            if (result.Success)
            {
                return Ok(new { jwt = result.Jwt });
            }

            return BadRequest(new { error = $"Error found while registering: { result.ErrorMessage }" });
        }
        catch (Exception ex)
        {
            // Bad, should at least scrub the message. For now, it's ok.
            return StatusCode(500, new { error =  ex.Message });
        }
    }

    [HttpPost("login", Name = "Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var result = await authenticationService.Login(request.Username, request.Password);
            if (result.Success)
            {
                return Ok(new { jwt = result.Jwt });
            }

            return BadRequest(new { error = $"Error found while logging in: { result.ErrorMessage }" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}