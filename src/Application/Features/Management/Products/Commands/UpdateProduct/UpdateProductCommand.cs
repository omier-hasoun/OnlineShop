

namespace Application.Features.Management.Products.Commands.UpdateProduct;
public sealed record UpdateProductCommand(long Product_Id, Guid? New_Brand_Id, long? New_Category_Id, string? New_Title, string? New_Description,
    bool? New_Is_Serialized, Dictionary<string, string>? New_Attributes)
: IRequest<Result<Updated>>;
