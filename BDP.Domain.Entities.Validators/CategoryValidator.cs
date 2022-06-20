using FluentValidation;

namespace BDP.Domain.Entities.Validators;

/// <summary>
/// A validator for the entity <see cref="Category"/>
/// </summary>
public class CategoryValidator : Validator<Category>
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public CategoryValidator()
    {
        RuleFor(a => a.Name)
            .NotEmpty()
            .WithMessage("category name cannot be empty");
    }
}