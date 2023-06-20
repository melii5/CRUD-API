using ErrorOr;
using MediatR;

namespace Application.Products.Create;

public record CreateProductCommand(string Name, string Description, string Sku, decimal Price) : IRequest<ErrorOr<Unit>>;