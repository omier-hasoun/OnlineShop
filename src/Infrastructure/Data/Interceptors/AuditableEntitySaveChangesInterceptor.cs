

namespace Infrastructure.Data.Interceptors;

public sealed class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly IUserContext _user;

    private readonly TimeProvider _timeProvider;
    public AuditableEntitySaveChangesInterceptor(IUserContext user, TimeProvider dateTime)
    {
        _timeProvider = dateTime;
        _user = user;
    }
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken ct = default)
    {
        UpdateAuditableEntities(eventData);
        return await base.SavingChangesAsync(eventData, result, ct);
    }

    private void UpdateAuditableEntities(DbContextEventData eventData)
    {

        if (eventData.Context is null)
            return;

        var entries = eventData.Context.ChangeTracker.Entries<AuditableEntity>();
        Guid userId = _user.Id;

        foreach (var entry in entries)
        {

            DateTimeOffset utcNow = _timeProvider.GetUtcNow();
            var entity = entry.Entity;
            if (entry.State is EntityState.Added)
            {
                entity._setCreated(userId, utcNow);
            }

            // Skip unchanged entities and hard deletes (non soft-deletable)
            if (entry.State == EntityState.Unchanged || entry.State == EntityState.Deleted && entity is not ISofDeletable)
            {
                continue;
            }

            entity._setModified(userId, utcNow);

        }
    }


}
