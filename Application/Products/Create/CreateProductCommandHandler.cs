using Domain.Primitives;
using Domain.Products;
using ErrorOr;
using MediatR;

namespace Application.Products.Create;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Unit>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (Sku.Create(request.Sku) is not Sku sku)
                return Error.Validation("SkuIsNotValid", "Sku is not valid");

            var product = new Product(
                new ProductId(Guid.NewGuid()),
                request.Name,
                request.Description,
                sku,
                request.Price
            );

            await _productRepository.Add(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
        catch (Exception e)
        {
            return Error.Failure("CreateProductFailure", e.Message);
        }
    }
}