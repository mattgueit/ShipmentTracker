namespace ShipmentTracker.Domain.Authentication;

public static class Roles
{
    public const string Admin = "Admin";
    public const string Member = "Member";
    
    public static readonly string[] AllRoles = [Admin, Member];
}