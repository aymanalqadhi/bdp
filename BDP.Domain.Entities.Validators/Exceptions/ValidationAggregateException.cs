namespace BDP.Domain.Entities.Validators.Exceptions;

/// <summary>
/// An exception class to represent validation errors
/// </summary>
public class ValidationAggregateException : Exception
{
    #region Fields

    private readonly IEnumerable<ValidationError> _errors;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="errors">The error list</param>
    public ValidationAggregateException(IEnumerable<ValidationError> errors)
        : base("validation error")
    {
        _errors = errors;
    }

    #endregion Public Constructors

    #region Properties

    /// <summary>
    /// Gets the validation errors
    /// </summary>
    public IEnumerable<ValidationError> Errors => _errors;

    #endregion Properties
}

/// <summary>
/// A record to hold validation errors
/// </summary>
/// <param name="PropertyName">The name of the failing property</param>
/// <param name="AttemptedValue">The failing value</param>
/// <param name="ErrorMessage">The error message</param>
public record ValidationError(string PropertyName, object AttemptedValue, string ErrorMessage);