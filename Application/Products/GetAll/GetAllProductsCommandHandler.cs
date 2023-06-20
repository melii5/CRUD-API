using Domain.Products;
using MediatR;

namespace Application.Products.GetAll;

public class GetAllProductsCommandHandler : IRequestHandler<GetAllProductsCommand, IEnumerable<Product>>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> Handle(GetAllProductsCommand request, CancellationToken cancellationToken)
    {
        var result = await _productRepository.GetAll();

        return result;
    }
}