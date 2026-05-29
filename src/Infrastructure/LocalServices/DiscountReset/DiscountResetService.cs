using Microsoft.Extensions.Logging;

namespace Infrastructure.LocalServices.DiscountReset;

internal sealed class DiscountResetService(IServiceProvider provider, TimeProvider time, ILogger<DiscountResetService> logger) : IZeroOclockService
{
    public async Task ExecuteAsync()
    {
        using var scope = provider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<IAppDbContext>();
        var today = DateOnly.FromDateTime(time.GetUtcNow().Date);

        int rowsAffected = await context.Products.Where(p => p.HasActiveDiscount && (p.DiscountExpiresOn == null || p.DiscountExpiresOn < today))
                                                 .ExecuteUpdateAsync(x => x.SetProperty(x => x.HasActiveDiscount, false));

        logger.LogInformation("{service} On {today}, updated {rowsAffected} products.", nameof(DiscountResetService), today, rowsAffected);

    }
}
