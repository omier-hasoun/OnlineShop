

namespace Application.Common.Enums;

public static class AppRoleTypes
{
    public const string Admin = "Admin";
    public const string Staff = "Staff";
    public const string Manager = "Manager";

    public static readonly string[] AssignableRoles = ["staff", "manager"];
}
