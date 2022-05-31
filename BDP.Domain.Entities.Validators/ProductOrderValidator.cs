using FluentValidation;

namespace BDP.Domain.Entities.Validators;

/// <summary>
/// A validator for the entity <see cref="ProductOrder"/>
/// </summary>
public sealed class ProductOrderValidator : Validator<ProductOrder>
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public ProductOrderValidator()
    {
        RuleFor(o => o.Quantity)
            .LessThanOrEqualTo(o => o.Product.Quantity)
            .WithMessage("order quantity cannot be greater than the available quantity");
    }
}