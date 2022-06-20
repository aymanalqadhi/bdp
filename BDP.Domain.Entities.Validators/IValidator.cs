namespace BDP.Domain.Entities.Validators;

/// <summary>
/// An interface to be implemented by entity validators
/// </summary>
/// <typeparam name="T">The type to be validated</typeparam>
public interface IValidator<T>
{
    /// <summary>
    /// Validates the passed item
    /// </summary>
    /// <param name="item">The item to validate</param>
    /// <returns>The validation result</returns>
    bool IsValid(T item);

    /// <summary>
    /// Validates
    /// </summary>
    /// <param name="item"></param>
    /// <exception cref="ValidationAggregateException">
    /// Thrown when a validation error is faced
    /// </exception>
    void Validate(T item);
}