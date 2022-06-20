using FluentValidation;

namespace BDP.Domain.Entities.Validators;

/// <summary>
/// A validator for the entity <see cref="Order"/>
/// </summary>
public sealed class OrderValidator : Validator<Order>
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public OrderValidator()
    {
        RuleFor(o => o.Quantity)
            .LessThanOrEqualTo(o => o.Variant.AvailableQuantity)
            .WithMessage("order quantity cannot be greater than the available quantity");
    }
}