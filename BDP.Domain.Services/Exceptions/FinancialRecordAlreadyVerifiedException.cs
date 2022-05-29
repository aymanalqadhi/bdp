using BDP.Domain.Entities;

namespace BDP.Domain.Services.Exceptions;

public sealed class FinancialRecordAlreadyVerifiedException : Exception
{
    private readonly FinancialRecord _record;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="email">The email the user entered</param>
    public FinancialRecordAlreadyVerifiedException(FinancialRecord record)
        : base($"financial record #{record.Id} is has been already verified")
    {
        _record = record;
    }

    /// <summary>
    /// Gets the financial record associated with the error
    /// </summary>
    public FinancialRecord Record => _record;
}
