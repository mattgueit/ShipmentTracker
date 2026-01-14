using Microsoft.AspNetCore.Mvc;
using ShipmentTracker.Domain.Authentication;

namespace ShipmentTracker.Controllers;

public record RegisterRequest(string Username, string Password);

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

            return BadRequest(new { error = result.ErrorMessage });
        }
        catch (Exception ex)
        {
            // Bad, should at least scrub the message. For now, it's ok.
            return StatusCode(500, ex.Message);
        }
    }
}