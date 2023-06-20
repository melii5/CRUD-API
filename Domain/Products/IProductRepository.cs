namespace Domain.Products;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product?> GetProductById(ProductId id);
    Task Add(Product product);
    void Update(Product product);
    void Remove(Product product);
}