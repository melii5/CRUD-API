using ErrorOr;
using FluentValidation;
using MediatR;

namespace Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TReponse> : IPipelineBehavior<TRequest, TReponse>
    where TRequest : IRequest<TReponse>
    where TReponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    public async Task<TReponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TReponse> next,
        CancellationToken cancellationToken
    )
    {
        if (_validator is null)
            return await next();

        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validatorResult.IsValid)
            return await next();

        var errors = validatorResult.Errors
            .ConvertAll(
                validationFailure => Error.Validation(validationFailure.PropertyName, validationFailure.ErrorMessage));

        return (dynamic)errors;
    }
}