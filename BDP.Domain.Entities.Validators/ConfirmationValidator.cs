﻿using FluentValidation;

namespace BDP.Domain.Entities.Validators;

/// <summary>
/// A validator for the entity <see cref="Confirmation"/>
/// </summary>
public sealed class ConfirmationValidator : Validator<Confirmation>
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public ConfirmationValidator()
    {
        RuleFor(c => c.ValidFor)
            .GreaterThan(TimeSpan.Zero)
            .WithMessage("validity period cannot be zero");

        RuleFor(c => c.OneTimePassword)
            .NotEmpty()
            .WithMessage("OTP cannot be empty");

        RuleFor(c => c.Token)
            .NotEmpty()
            .WithMessage("confirmation token is required");
    }
}