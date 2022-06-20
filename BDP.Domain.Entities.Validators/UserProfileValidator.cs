using FluentValidation;

namespace BDP.Domain.Entities.Validators;

/// <summary>
/// A validator for the entity <see cref="UserProfile"/>
/// </summary>
public class UserProfileValidator : Validator<UserProfile>
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public UserProfileValidator()
    {
        RuleFor(p => p.FullName)
            .MaximumLength(64)
            .When(f => f is not null);

        RuleFor(p => p.Address)
            .MaximumLength(128)
            .When(l => l is not null);

        RuleFor(p => p.Bio)
            .MaximumLength(255)
            .When(b => b is not null);
    }
}