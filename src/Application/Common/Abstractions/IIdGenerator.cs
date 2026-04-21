namespace Application.Common.Abstractions;

public interface IIdGenerator<TType>
{
    TType NewId();
}
