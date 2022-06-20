using FluentValidation;

namespace BDP.Domain.Entities.Validators;

/// <summary>
/// A validator for the entity <see cref="Event"/>
/// </summary>
public sealed class EventValidator : Validator<Event>
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public EventValidator()
    {
        RuleFor(e => e.Title)
            .NotEmpty()
            .WithMessage("title is required");

        RuleFor(e => e.Description)
            .NotEmpty()
            .WithMessage("description is required");

        RuleFor(e => e.Progress)
            .Must(p => p <= 1 && p >= 0)
            .WithMessage("invalid progress value");

        RuleFor(e => e.TakesPlaceAt)
            .GreaterThan(DateTime.Now)
            .WithMessage("event date cannot be in the past");
    }
}