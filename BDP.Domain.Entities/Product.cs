﻿namespace BDP.Domain.Entities;

/// <summary>
/// A class to represent a product
/// </summary>
public sealed class Product : AuditableEntity<Product>, IOwnable
{
    /// <summary>
    /// Gets or sets the title of the sellable
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Gets or sets the description of the sellable
    /// </summary
    public string Description { get; set; } = null!;

    /// <inheritdoc/>
    public User OwnedBy { get; set; } = null!;

    /// <summary>
    /// Gets or sets whether the sellable is available
    /// </summary>
    public bool IsAvailable { get; set; } = true;

    /// <summary>
    /// Gets or sets the collection of categories the product belongs to
    /// </summary>
    public ICollection<Category> Categories { get; set; } = new List<Category>();

    /// <summary>
    /// Gets or sets the collection of reviews
    /// </summary>
    public ICollection<ProductReview> Reviews { get; set; } = new List<ProductReview>();

    /// <summary>
    /// Gets or sets the collection of variants of the product
    /// </summary>
    public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
}