

namespace Infrastructure.Data.Interceptors;

internal sealed class AuditedEntitySaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserService _user;
    private readonly TimeProvider _time;

    public AuditedEntitySaveChangesInterceptor(ICurrentUserService user, TimeProvider time)
    {
        _user = user;
        _time = time;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken ct = default)
    {
        UpdateAuditableEntities(eventData);
        return await base.SavingChangesAsync(eventData, result, ct);
    }

    private void UpdateAuditableEntities(DbContextEventData eventData)
    {
        if (eventData.Context is null) return;

        var entries = eventData.Context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);


        foreach (var entry in entries)
        {
            var isAdded = entry.State == EntityState.Added;
            var isModified = entry.State == EntityState.Modified;

            var utcNow = _time.GetUtcNow().DateTime;
            
            if (entry.Entity is IHasCreationTime cTime && isAdded)
                cTime.CreatedAt = utcNow;

            if (entry.Entity is IHasModificationTime mTime && (isAdded || isModified))
                mTime.LastModifiedAt = utcNow;

            if (entry.Entity is ICreationAudited cUser && isAdded)
                cUser.CreatedBy = _user.GetUserId() ?? Guid.Parse("10000000-0000-0000-0000-000000000001");

            if (entry.Entity is IModificationAudited mUser && (isAdded || isModified))
                mUser.LastModifiedBy = _user.GetUserId() ?? Guid.Parse("10000000-0000-0000-0000-000000000001"); ;
        }
    }
}
