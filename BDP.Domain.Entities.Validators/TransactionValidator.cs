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
        RuleFor(t => t.Amount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("transaction amount cannot be less than 0");

        RuleFor(t => t.ConfirmationToken)
            .NotEmpty()
            .WithMessage("confirmation token is required");
    }
}