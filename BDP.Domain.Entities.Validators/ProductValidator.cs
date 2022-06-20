using FluentValidation;

namespace BDP.Domain.Entities.Validators;

/// <summary>
/// A validator for the entity <see cref="Product"/>
/// </summary>
public class ProductValidator : Validator<Product>
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public ProductValidator()
    {
        RuleFor(s => s.Title)
            .NotEmpty()
            .WithMessage("title cannot be empty");

        RuleFor(s => s.Description)
            .NotEmpty()
            .WithMessage("description cannot be empty");
    }
}