using Domain.Primitives;
using Domain.Products;
using MediatR;
using ErrorOr;

namespace Application.Products.Update;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _productRepository.GetProductById(new ProductId(request.Id));

            if (result is null)
            {
                return Error.NotFound("ProductNotFound", "Product Not Found");
            }
        
            result.Update(request.Name, request.Description, Sku.Create(request.Sku)!, request.Price);

            _productRepository.Update(result);
        
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
        catch (Exception e)
        {
            return Error.Failure("UpdateProductFailure", "Product not updated");
        }
    }
}