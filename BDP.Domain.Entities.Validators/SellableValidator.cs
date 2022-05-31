using FluentValidation;

namespace BDP.Domain.Entities.Validators;

/// <summary>
/// A validator for the entity <see cref="Sellable"/>
/// </summary>
public class SellableValidator<T> : Validator<T> where T : Sellable
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public SellableValidator()
    {
        RuleFor(s => s.Title)
            .NotEmpty()
            .WithMessage("title cannot be empty");

        RuleFor(s => s.Description)
            .NotEmpty()
            .WithMessage("description cannot be empty");

        RuleFor(s => s.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("must be higher than or equal to 0");
    }
}

public sealed class SellableValidator : SellableValidator<Sellable>
{ }