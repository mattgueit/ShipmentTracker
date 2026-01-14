using Microsoft.AspNetCore.Identity;

namespace ShipmentTracker.Domain.Authentication;

// I would have gone without this class, but it seems to be required for .AddIdentityCore in the DI setup.  
public class ApplicationUser : IdentityUser
{
    // extend later if needed.
}