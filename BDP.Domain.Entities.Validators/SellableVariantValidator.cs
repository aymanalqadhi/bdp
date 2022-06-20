using FluentValidation;

namespace BDP.Domain.Entities.Validators;

/// <summary>
/// A validator for the entity <see cref="ProductVariant{TEntity}"/>
/// </summary>
public sealed class SellableVariantValidator : ProductVariantValidator<SellableVariant>
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public SellableVariantValidator() : base()
    {
    }
}