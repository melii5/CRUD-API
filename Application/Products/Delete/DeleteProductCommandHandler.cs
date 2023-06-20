using Domain.Primitives;
using Domain.Products;
using ErrorOr;
using MediatR;

namespace Application.Products.Delete;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _productRepository.GetProductById(new ProductId(request.Id));

            if (product is null)
            {
                return Error.NotFound("ProductNotFound", $"Product with Id {product?.Id} not found");
            }
        
            _productRepository.Remove(product);
        
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
        catch (Exception e)
        {
            return Error.Failure("DeleteProductFailure", e.Message);
        }
    }
}