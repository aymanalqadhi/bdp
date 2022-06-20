using BDP.Domain.Entities;

namespace BDP.Domain.Services;

public interface IProductsService
{
    /// <summary>
    /// Asynchronously gets a product by its id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Product> GetByIdAsync(Guid id);

    /// <summary>
    /// Asynchronously lists a product
    /// </summary>
    /// <param name="userId">The id of the user which to list the product for</param>
    /// <param name="title">The title of the product</param>
    /// <param name="description">The description of the product</param>
    /// <param name="price">The price of the product</param>
    /// <param name="quantity">The available quantity</param>
    /// <param name="attachments">The file attachments of the product</param>
    /// <returns>The listed product</returns>
    Task<Product> ListAsync(
        Guid userId,
        string title,
        string description,
        decimal price,
        uint quantity,
        IEnumerable<IUploadFile>? attachments = null
    );

    /// <summary>
    /// Asynchronsoulsy updates a product
    /// </summary>
    /// <param name="productId">The id of the product to update</param>
    /// <param name="title">The new title of the product</param>
    /// <param name="description">The new description of the product</param>
    /// <param name="price">The new price of the product</param>
    /// <returns></returns>
    Task<Product> UpdateAsync(
        Guid productId,
        string title,
        string description,
        decimal price
    );

    /// <summary>
    /// Asynchrnously unlists a product
    /// </summary>
    /// <param name="productId">The id of the product to unlist</param>
    /// <returns></returns>
    Task UnlistAsync(Guid productId);

    /// <summary>
    /// Asynchrnously gets the available quantity of the product
    /// </summary>
    /// <param name="productId">The id of the product to check for</param>
    /// <returns>The available quantity</returns>
    Task<long> AvailableQuantityAsync(Guid productId);

    /// <summary>
    /// Asynchrnously checks whether the product is available
    /// </summary>
    /// <param name="productId">The id of the product to check for</param>
    /// <returns>True if the product has an available qauntity, false otherwise</returns>
    Task<bool> IsAvailableAsync(Guid productId);

    /// <summary>
    /// Asynchronously changes the availability flag of a product
    /// </summary>
    /// <param name="product">The product to change</param>
    /// <param name="isAvailable">The new availability value</param>
    /// <returns>The updated service</returns>
    Task<Product> SetAvailability(Guid productId, bool isAvailable);

    /// <summary>
    /// Asynchronously creates an order for a product
    /// </summary>
    /// <param name="userId">The id of the user which to create the order for</param>
    /// <param name="productId">The id of the product to order</param>
    /// <param name="quantity">The ordered quantity</param>
    /// <returns>The created order</returns>
    Task<ProductOrder> OrderAsync(Guid userId, Guid productId, uint quantity);
}