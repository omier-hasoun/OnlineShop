

namespace Infrastructure.Data.Interceptors;

public sealed class SoftDeletableEntitySaveChangesInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken ct = default)
    {
        if (eventData.Context is null)
            return await ValueTask.FromResult(result);

        var entries = eventData.Context.ChangeTracker.Entries<ISoftDeletable>();

        foreach (var entry in entries)
        {
            if (entry.State != EntityState.Deleted)
                continue;
            

            entry.State = EntityState.Modified;
            entry.Entity.IsDeleted = true;

        }

        return await base.SavingChangesAsync(eventData, result, ct);
    }
}
