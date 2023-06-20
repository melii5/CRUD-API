using Domain.Primitives;

namespace Domain.Products;

public sealed class Product : AggregateRoot
{
    public ProductId Id { get; private set; } = default!;
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Sku Sku { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;

    private Product()
    {
        
    }

    public Product(ProductId id, string name, string description, Sku sku, decimal price)
    {
        Id = id;
        Name = name;
        Description = description;
        Sku = sku;
        Price = price;
    }

    public void Update(string name, string description, Sku sku, decimal price)
    {
        Name = name;
        Description = description;
        Sku = sku;
        Price = price;
    }
}