using FluentValidation;

namespace BDP.Domain.Entities.Validators;

/// <summary>
/// A validator for the entity <see cref="Confirmation"/>
/// </summary>
public sealed class SellableReviewValidator : Validator<SellableReview>
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public SellableReviewValidator()
    {
        RuleFor(c => c.Rating)
            .Must(c => c >= 1 && c <= 5)
            .WithMessage("rating value should be in the range 1-5");
    }
}