namespace Domain.Common.Abstractions;

public interface IHasModificationTime
{
    public DateTime LastModifiedAt { get; set; }
}

