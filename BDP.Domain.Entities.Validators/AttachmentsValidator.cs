using FluentValidation;

namespace BDP.Domain.Entities.Validators;

/// <summary>
/// A validator for the entity <see cref="Attachment"/>
/// </summary>
public class AttachmentsValidator : Validator<Attachment>
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public AttachmentsValidator()
    {
        RuleFor(a => a.Mime)
            .Matches(@"(.+?)/(.+?)");
    }
}