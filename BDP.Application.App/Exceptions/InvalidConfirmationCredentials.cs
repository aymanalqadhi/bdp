using BDP.Domain.Entities;

namespace BDP.Application.App.Exceptions;

public class InvalidConfirmationCredentials : Exception
{
    private readonly Confirmation _confirmation;

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="confirmation">The confirmation object</param>
    public InvalidConfirmationCredentials(Confirmation confirmation)
    {
        _confirmation = confirmation;
    }

    /// <summary>
    /// Gets the confirmation object
    /// </summary>
    public Confirmation Confirmation => _confirmation;

    public override string Message => $"invalid credentails were used to activate account";
}