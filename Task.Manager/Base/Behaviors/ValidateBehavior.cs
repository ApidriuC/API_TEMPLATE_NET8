using FluentValidation;
using FluentValidation.Results;
using MediatR;
using TaskProject.Manager.Base.Exceptions;
using TaskProject.Manager.Base.Interfaces;

namespace TaskProject.Manager.Base.Behaviors;

/// <summary/>
public sealed class ValidateBehavior<TRequest, TResponse>(IServiceProvider provider) :
    IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, IValidateBehavior
{
    /// <inheritdoc/>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Type genericType = typeof(IValidator<>).MakeGenericType(typeof(TRequest));

        IValidator<TRequest> validator = provider
            .GetService(genericType) as IValidator<TRequest> ??
            throw ErrorValidator.ThrowNotFoundedValidator(typeof(TRequest).Name);

        ValidationResult validation = validator.Validate(request);
        var notSuccess = !validation.IsValid;

        if (notSuccess)
        {
            var errores = validation.Errors.Select(v =>
                new ErrorDetail
                {
                    Message = v.ErrorMessage,
                    PropertyName = v.PropertyName
                }
            );

            throw ErrorValidator
                .ThrowInvalidValidator("Ha ocurrido un error al realizar la validación del modelo.", errores);
        }

        var response = await next();
        return response;
    }
}