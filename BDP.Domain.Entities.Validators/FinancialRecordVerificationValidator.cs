using FluentValidation;

namespace BDP.Domain.Entities.Validators;

/// <summary>
/// A validator for the entity <see cref="FinancialRecordVerification"/>
/// </summary>
public sealed class FinancialRecordVerificationValidator : Validator<FinancialRecordVerification>
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public FinancialRecordVerificationValidator()
    {
        RuleFor(f => f.Note)
            .MaximumLength(255)
            .When(n => n is not null)
            .WithMessage("notes cannot go above 255 characters");
    }
}