


namespace Infrastructure.Data.Interceptors;

public sealed class AuditedEntitySaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly IUserContext _user;

    public AuditedEntitySaveChangesInterceptor(IUserContext user)
    {
        _user = user;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken ct = default)
    {
        UpdateAuditableEntities(eventData);
        return await base.SavingChangesAsync(eventData, result, ct);
    }

    private void UpdateAuditableEntities(DbContextEventData eventData)
    {
        if (eventData.Context is null) return;

        var utcNow = TimeService.UtcNow;
        var userId = _user.Id;

        var entries = eventData.Context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            var isAdded = entry.State == EntityState.Added;
            var isModified = entry.State == EntityState.Modified;

            
            if (entry.Entity is IHasCreationTime cTime && isAdded)
                cTime.CreatedAt = utcNow;

            if (entry.Entity is IHasModificationTime mTime && (isAdded || isModified))
                mTime.LastModifiedAt = utcNow;

            if (entry.Entity is ICreationAudited cUser && isAdded)
                cUser.CreatedBy = userId;

            if (entry.Entity is IModificationAudited mUser && (isAdded || isModified))
                mUser.LastModifiedBy = userId;
        }
    }
}
