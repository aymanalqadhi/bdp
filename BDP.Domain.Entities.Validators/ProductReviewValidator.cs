using FluentValidation;

namespace BDP.Domain.Entities.Validators;

/// <summary>
/// A validator for the entity <see cref="ProductReview"/>
/// </summary>
public sealed class ProductReviewValidator : Validator<ProductReview>
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public ProductReviewValidator()
    {
        RuleFor(c => c.Rating)
            .Must(c => c >= 1 && c <= 5)
            .WithMessage("rating value should be in the range 1-5");
    }
}