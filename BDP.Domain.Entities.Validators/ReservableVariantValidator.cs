using FluentValidation;

namespace BDP.Domain.Entities.Validators;

/// <summary>
/// A validator for the entity <see cref="ReservableVariant"/>
/// </summary>
public sealed class ReservableVariantValidator : ProductVariantValidator<ReservableVariant>
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public ReservableVariantValidator() : base()
    {
    }
}