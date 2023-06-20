using Domain.Products;
using MediatR;

namespace Application.Products.GetAll;

public record GetAllProductsCommand() : IRequest<IEnumerable<Product>>;