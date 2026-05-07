public sealed record UpdateProductCommand(Guid Brand_Id, long Category_Id, string Title, string Description, bool Is_Serialized, Dictionary<string, string> Attributes);
