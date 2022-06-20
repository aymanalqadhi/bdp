using FluentValidation;

namespace BDP.Domain.Entities.Validators;

/// <summary>
/// A validator for the entity <see cref="EventType"/>
/// </summary>
public sealed class EventTypeValidator : Validator<EventType>
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public EventTypeValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .WithMessage("event type name is required");
    }
}