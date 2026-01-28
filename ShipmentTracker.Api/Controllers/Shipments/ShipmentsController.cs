using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShipmentTracker.Api.Controllers.Shipments;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ShipmentsController : ControllerBase
{
    [HttpGet("", Name = "GetShipments")]
    public async Task<IResult> GetShipments()
    {
        await Task.Delay(0);

        var claimsPrincipal = HttpContext.User;
        
        var emailAddress = HttpContext.User.FindFirstValue(ClaimTypes.Email);
        
        return Results.Ok(new { emailAddress });
    }
}