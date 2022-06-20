using BDP.Domain.Entities.Validators.Exceptions;
using FluentValidation;

namespace BDP.Domain.Entities.Validators;

/// <summary>
/// Base validator type
/// </summary>
/// <typeparam name="T">The type which to validate</typeparam>
public class Validator<T> : AbstractValidator<T>, IValidator<T>
{
    /// <inheritdoc/>
    public bool IsValid(T item)
        => Validate(item).IsValid;

    /// <inheritdoc/>
    void IValidator<T>.Validate(T item)
    {
        var res = Validate(item);

        if (!res.IsValid)
        {
            throw new ValidationAggregateException(
                res.Errors.Select(e =>
                    new ValidationError(e.PropertyName, e.AttemptedValue, e.ErrorMessage)
                )
            );
        }
    }
}