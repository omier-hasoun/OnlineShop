

namespace Domain.EmployeeExample;

public class Employee : AuditableEntity, ISofDeletable
{
    public const byte MinimumAllowedEmployeeAge = 16;
    private Employee()
    {

    }

    public static Result<Employee> Create(string firstName, string lastName, DateTime dateOfBirth, Guid id = default)
    {

        if (DataValidator.IsValidAge(dateOfBirth, Employee.MinimumAllowedEmployeeAge))
        {
            return EmployeeErrors.InvalidEmployeeAge;
        }


        return new Employee
        {
            Id = id,
            DateOfBirth = dateOfBirth,
            FirstName = firstName,
            LastName = lastName,
        };
    }

    // all your properties here
    #region Properties

    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string FullName { get { return $"{FirstName} {LastName}"; } }
    public DateTime DateOfBirth { get; private set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }

    #endregion



}
