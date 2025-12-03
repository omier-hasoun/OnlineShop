namespace Infrastructure.Data.JoinEntities;
// the purpose of join entities is to make many to many relation ships possible
// so its a database matter, in the code we will never need to use them directly .
// Thats why i decided to define all join entites  in Infrastructure\Data\JoinEntities
// to not pollute the domain with join entities that actually will never be needed in the code
public sealed class UserRoles : IdentityUserRole<Guid>
{

    public UserRoles()
    { }
}
