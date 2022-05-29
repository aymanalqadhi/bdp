using FluentValidation;

namespace BDP.Domain.Entities.Validators;

/// <summary>
/// A validator for the entity <see cref="FinancialRecord"/>
/// </summary>
public sealed class FinancialRecordValidator : Validator<FinancialRecord>
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public FinancialRecordValidator()
    {
        RuleFor(f => f.Amount)
            .NotEqual(0)
            .WithMessage("financial record amount cannot be 0");
    }
}
