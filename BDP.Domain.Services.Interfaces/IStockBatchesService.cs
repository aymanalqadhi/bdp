﻿using BDP.Domain.Entities;

namespace BDP.Domain.Services;

/// <summary>
/// An interface to be implemented by sellable product variants  stock batches
/// services
/// </summary>
public interface IStockBatchesService
{
    #region Public Methods

    /// <summary>
    /// Asynchronously adds a stock batch to a sellable product variant
    /// </summary>
    /// <param name="variantId">The id of the variant to add the stock batch for</param>
    /// <param name="quantity">The quantity of the stock batch</param>
    /// <returns>The created stock batch</returns>
    /// <exception cref="InvalidProductVaraintTypeException"></exception>
    Task<StockBatch> AddStockBatchAsync(EntityKey<ProductVariant> variantId, uint quantity);

    /// <summary>
    /// Asynchronously removes a sellable product variant stock batch
    /// </summary>
    /// <param name="batchId">The id of the batch to remove</param>
    /// <returns></returns>
    /// <exception cref="NotEnoughStockException"></exception>
    Task RemoveStockBatchAsync(EntityKey<StockBatch> batchId);

    /// <summary>
    /// Asynchronously calculates the total available quantity of a sellable product
    /// variant. This method will calculate the quantity with regard to available stock batches
    /// </summary>
    /// <param name="variantId">
    /// The id of the product variant to calculate the total quantity for
    /// </param>
    /// <returns>The total available quantity</returns>
    Task<long> TotalAvailableQuantityAsync(EntityKey<ProductVariant> variantId);

    #endregion Public Methods
}