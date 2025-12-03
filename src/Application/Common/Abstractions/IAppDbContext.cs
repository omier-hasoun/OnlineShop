
namespace Application.Common.Abstractions;

public interface IAppDbContext
{
    // add your Entities Set
    Task<int> SaveChangesAsync(CancellationToken token);
}
