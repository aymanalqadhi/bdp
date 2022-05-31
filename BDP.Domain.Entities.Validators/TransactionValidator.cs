using FluentValidation;

namespace BDP.Domain.Entities.Validators;

/// <summary>
/// A validator for the entity <see cref="Transaction"/>
/// </summary>
public sealed class TransactionValidator : Validator<Transaction>
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public TransactionValidator()
    {
        RuleFor(u => u.Amount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("transaction amount must be higher than 0");
    }
}