using FluentValidation;

namespace BDP.Domain.Entities.Validators;

/// <summary>
/// A validator for the entity <see cref="User"/>
/// </summary>
public sealed class UserValidator : Validator<User>
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public UserValidator()
    {
        RuleFor(u => u.Username)
            .NotEmpty()
            .WithMessage("username is required")
            .Matches(@"^[a-zA-Z0-9](_(?!(\.|_))|\.(?!(_|\.))|[a-zA-Z0-9]){4,18}[a-zA-Z0-9]$")
            .WithMessage("invalid username");

        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage("email is required")
            .EmailAddress()
            .WithMessage("invalid email address");

        RuleFor(u => u.IsActive)
            .Equal(false)
            .When(u => !u.IsConfirmed)
            .WithMessage("a user cannot be active when not confirmed");
    }
}
