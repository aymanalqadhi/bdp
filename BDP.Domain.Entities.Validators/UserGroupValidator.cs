using FluentValidation;

namespace BDP.Domain.Entities.Validators;

/// <summary>
/// A validator for the entity <see cref="UserGroup"/>
/// </summary>
public sealed class UserGroupValidator : Validator<UserGroup>
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public UserGroupValidator()
    {
        RuleFor(u => u.Name)
            .NotEmpty()
            .WithMessage("Group name is required")
            .Matches(@"^\w{2,}$")
            .WithMessage("invalid group name");
    }
}