namespace Application.Common.Abstractions;

public interface IIdProvider<TType> where TType : IEquatable<TType> 
{
    public TType GetNewId();
}
