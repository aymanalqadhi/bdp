﻿using FluentValidation;

namespace BDP.Domain.Entities.Validators;

/// <summary>
/// A validator for the entity <see cref="FinancialRecord"/>
/// </summary>
public sealed class FinancialRecordValidator : Validator<FinancialRecord>
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public FinancialRecordValidator()
    {
        RuleFor(f => f.Amount)
            .NotEqual(0)
            .WithMessage("financial record amount cannot be 0");

        RuleFor(f => f.Note)
            .MaximumLength(255)
            .When(n => n is not null)
            .WithMessage("notes cannot go above 255 characters");
    }
}