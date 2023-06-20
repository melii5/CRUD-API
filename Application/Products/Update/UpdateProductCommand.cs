using MediatR;
using ErrorOr;

namespace Application.Products.Update;

public record UpdateProductCommand(Guid Id, string Name, string Description, string Sku, decimal Price) : IRequest<ErrorOr<Unit>>;