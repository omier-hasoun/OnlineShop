namespace Domain.Common.Abstractions;

public interface IFullAudited : ICreationAudited, IModificationAudited, IHasCreationTime, IHasModificationTime;

