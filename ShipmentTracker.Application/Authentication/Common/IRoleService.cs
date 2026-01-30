namespace ShipmentTracker.Application.Authentication.Common;

public interface IRoleService
{
    Task<bool> RoleExistsAsync(string role);
}