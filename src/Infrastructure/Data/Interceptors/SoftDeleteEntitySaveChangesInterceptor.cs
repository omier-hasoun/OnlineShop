

namespace Infrastructure.Data.Interceptors;

internal sealed class SoftDeleteEntitySaveChangesInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken ct = default)
    {
        if (eventData.Context is null)
            return await ValueTask.FromResult(result);

        var entries = eventData.Context.ChangeTracker.Entries<ISoftDelete>();

        foreach (var entry in entries)
        {
            if (entry.State != EntityState.Deleted)
                continue;
            
            entry.Property(e => e.IsDeleted).CurrentValue = true;
            entry.State = EntityState.Modified;

        }

        return await base.SavingChangesAsync(eventData, result, ct);
    }
}
