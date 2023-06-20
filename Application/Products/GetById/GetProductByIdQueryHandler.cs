using Domain.Products;
using MediatR;

namespace Application.Products.GetById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductById(new ProductId(request.Id));

        if (product is null)
            throw new Exception("Product Not Found");

        return product!;
    }
}