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
            .GreaterThan(0u)
            .WithMessage("order quantity must be greater than 0");
    }
}