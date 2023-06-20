namespace Domain.Products;

public partial record Sku
{
    private const int DefaultLength = 6;
    public string Value { get; init; }

    private Sku(string value)
    {
        Value = value;
    }

    public static Sku? Create(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length != DefaultLength)
            return null;

        return new Sku(value);
    }
}