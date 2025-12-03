
namespace Domain.EmployeeExample;

public static class EmployeeErrors
{
    public static Error InvalidEmployeeAge =>
    Error.Forbidden("EmployeeErrors.InvalidEmployeeAge",
    $"Employee should be atleast {Employee.MinimumAllowedEmployeeAge} years old");


}
