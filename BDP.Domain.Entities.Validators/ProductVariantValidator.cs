using FluentValidation;

namespace BDP.Domain.Entities.Validators;

/// <summary>
/// A validator for the entity <see cref="ProductVariant{TVariant}"/>
/// </summary>
public abstract class ProductVariantValidator<TVariant> : Validator<TVariant>
    where TVariant : ProductVariant<TVariant>
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
    }
}