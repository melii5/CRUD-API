using FluentValidation;

namespace Application.Products.Update;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty();
        
        RuleFor(r => r.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(r => r.Description)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(r => r.Sku)
            .NotEmpty()
            .MaximumLength(6);

        RuleFor(r => r.Price)
            .NotEmpty();
    }
}