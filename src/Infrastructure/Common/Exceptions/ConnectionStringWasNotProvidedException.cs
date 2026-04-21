
namespace Infrastructure.Common.Exceptions;

internal class ConnectionStringWasNotProvidedException : Exception
{
    public ConnectionStringWasNotProvidedException(string message = "no ConnectionString was provided") : base(message)
    {
        
    }
}
