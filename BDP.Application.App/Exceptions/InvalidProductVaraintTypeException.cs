using BDP.Domain.Entities;

namespace BDP.Application.App.Exceptions;

public class InvalidProductVaraintTypeException : Exception
{
    #region Fields

    private readonly ProductVariantType _actualType;
    private readonly ProductVariantType _expectedType;
    private readonly EntityKey<ProductVariant> _variantId;

    #endregion Fields

    #region Public Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    public InvalidProductVaraintTypeException(
        EntityKey<ProductVariant> variantId,
        ProductVariantType expected,
        ProductVariantType actual)
        : base($"produt variant #{variantId} has invalid type (expected: ${expected}, got: ${actual})")
    {
        _variantId = variantId;
        _expectedType = expected;
        _actualType = actual;
    }

    #endregion Public Constructors

    #region Properties

    /// <summary>
    /// Gets the product variant type passed by the user
    /// </summary>
    public ProductVariantType ActualType => _actualType;

    /// <summary>
    /// Gets the expected product variant type
    /// </summary>
    public ProductVariantType ExpectedType => _expectedType;

    /// <summary>
    /// Gets the id of the product variant
    /// </summary>
    public EntityKey<ProductVariant> VariantId => _variantId;

    #endregion Properties
}