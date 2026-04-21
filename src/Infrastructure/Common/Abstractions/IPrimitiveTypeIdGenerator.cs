namespace Infrastructure.Common.Abstractions;

public interface IPrimitiveTypeIdGenerator<TType> where TType : IEquatable<TType> 
{
    public TType Generate();
}
