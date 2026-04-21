
namespace Infrastructure.Common.Exceptions;

internal class MachineIdWasNotProvidedException : Exception
{
    public MachineIdWasNotProvidedException(string message = "no Machine Id was provided") : base(message)
    {
        
    }
}
