using Domain.Common.ValueObjects;
using Domain.ProductsGroups.Products;

namespace Application.Features.Management.ProductGroups.Commands.AddProduct;

public sealed class AddProductCommandHandler(IAppDbContext context, IIdGenerator<ProductId> idGen, IImageValidator validator, IImageStorageService imageStore) : IRequestHandler<AddProductCommand, Result<long>>
{
    public async Task<Result<long>> Handle(AddProductCommand request, CancellationToken ct)
    {

        ProductGroupId productGroupId = new(request.ProductId);

        var price = Money.From((decimal)request.Price).Value;

        var productGroup = await context.ProductGroups.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == productGroupId, ct);

        if (productGroup is null)
        {
            return ApplicationErrors.NotFound.Product;
        }

        if(request.Images != null)
        {
            validator.MinWidth = ApplicationRules.Uploads.MinWidth;
            validator.MinHeight = ApplicationRules.Uploads.MinHeight;
            validator.MaxSize = ApplicationRules.Uploads.MinHeight;

            var imagesValdationResult = validator.ValidateAll(request.Images);

            if (imagesValdationResult.Failed)
                return imagesValdationResult.Errors;
        }

        var productId = idGen.NewId();

        var addProductResult = productGroup.AddProduct(productId, price,
            request.Width, request.Height, request.Length, request.Weight,
            request.Sku, request.Slug, request.BarCode, request.Specifications);

        if (addProductResult.Failed)
        {
            return addProductResult.Errors;
        }

        if (request.Images != null)
        {
            List<string> imageNames = new(request.Images.Count);

            request.Images.ForEach(image =>
            {
                imageNames.Add(image.InternalFileName);
            });

            var addImagesResult = productGroup.AddProductImages(productId, imageNames);

            if (addImagesResult.Failed)
                return addImagesResult.Errors;


            var result = await imageStore.SaveAllAsync(request.Images, ct);

            if (result.Failed)
                return result.Errors;
        }

        await context.SaveAsync(ct);

        return productId.Value;
    }


}
