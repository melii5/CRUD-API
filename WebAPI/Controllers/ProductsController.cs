using Application.Products.Create;
using Application.Products.Delete;
using Application.Products.GetAll;
using Application.Products.GetById;
using Application.Products.Update;
using Domain.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]")]
public class ProductsController : ApiController
{
    private readonly ISender _mediator;

    public ProductsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<Product>> GetProducts()
    {
        var result = await _mediator.Send(new GetAllProductsCommand());
        
        return result;
    }

    [HttpGet("{id:guid}")]
    public async Task<Product> GetProduct(Guid id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id));

        return result;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var createProductResult = await _mediator.Send(command);

        return createProductResult.Match(
            product => Ok(),
            errors => Problem(errors)
        );
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand command)
    {
        var createProductResult = await _mediator.Send(command);

        return createProductResult.Match(
            product => Ok(),
            errors => Problem(errors)
        );
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteProductCommand command)
    {
        var deleteProductResult = await _mediator.Send(command);

        return deleteProductResult.Match(
            product => Ok(),
            errors => Problem(errors)
        );
    }
}