using FluentValidation;

namespace BDP.Domain.Entities.Validators;

/// <summary>
/// A validator for the entity <see cref="ProductVariant"/>
/// </summary>
public sealed class ProductVariantValidator : Validator<ProductVariant>
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public ProductVariantValidator()
    {
        RuleFor(s => s.Name)
            .NotEmpty()
            .WithMessage("variant name cannot be empty");

        RuleFor(s => s.Description)
            .NotEmpty()
            .WithMessage("variant description cannot be empty");

        RuleFor(s => s.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("price cannot be less than 0");
    }
}