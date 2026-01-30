using Microsoft.AspNetCore.Identity;
using ShipmentTracker.Application.Authentication.Common;

namespace ShipmentTracker.Infrastructure.Authentication;

public class RoleService(RoleManager<IdentityRole> roleManager) : IRoleService
{
    public async Task<bool> RoleExistsAsync(string role)
    {
        return await roleManager.RoleExistsAsync(role);
    }
}