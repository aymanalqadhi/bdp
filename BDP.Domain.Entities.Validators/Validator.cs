using FluentValidation;

namespace BDP.Domain.Entities.Validators;

/// <summary>
/// Base validator type
/// </summary>
/// <typeparam name="T">The type which to validate</typeparam>
public class Validator<T> : AbstractValidator<T>
{
}
