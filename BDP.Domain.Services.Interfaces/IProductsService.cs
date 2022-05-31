using BDP.Domain.Entities;

namespace BDP.Domain.Services;

public interface IProductsService
{
    /// <summary>
    /// Asynchronously gets a product by its id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Product> GetByIdAsync(long id);

    /// <summary>
    /// Asynchronously lists a product
    /// </summary>
    /// <param name="user">The user which to list the product for</param>
    /// <param name="title">The title of the product</param>
    /// <param name="description">The description of the product</param>
    /// <param name="price">The price of the product</param>
    /// <param name="quantity">The available quantity</param>
    /// <param name="attachments">The file attachments of the product</param>
    /// <returns>The listed product</returns>
    Task<Product> ListAsync(
        User user,
        string title,
        string description,
        decimal price,
        uint quantity,
        IEnumerable<IUploadFile>? attachments = null
    );

    /// <summary>
    /// Asynchronsoulsy updates a product
    /// </summary>
    /// <param name="product">The product to update</param>
    /// <param name="title">The new title of the product</param>
    /// <param name="description">The new description of the product</param>
    /// <param name="price">The new price of the product</param>
    /// <returns></returns>
    Task<Product> UpdateAsync(
        Product product,
        string title,
        string description,
        decimal price
    );

    /// <summary>
    /// Asynchrnously unlists a product
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    Task UnlistAsync(Product product);

    /// <summary>
    /// Asynchronously adds to the stock of a product
    /// </summary>
    /// <param name="product">The procut to add to its stock</param>
    /// <param name="quantity">The quantity to add</param>
    /// <returns>The updated product</returns>
    Task<Product> AddStock(Product product, uint quantity);

    /// <summary>
    /// Asynchronously removes from the stock of a product
    /// </summary>
    /// <param name="product">The procut to remove fromto its stock</param>
    /// <param name="quantity">The quantity to remove</param>
    /// <returns>The updated product</returns>
    Task<Product> RemoveStock(Product product, uint quantity);

    /// <summary>
    /// Asynchrnously gets the available quantity of the product
    /// </summary>
    /// <param name="product">The product to check for</param>
    /// <returns>The available quantity</returns>
    Task<long> AvailableQuantityAsync(Product product);

    /// <summary>
    /// Asynchrnously checks whether the product is available
    /// </summary>
    /// <param name="product">The product to check for</param>
    /// <returns>True if the product has an available qauntity, false otherwise</returns>
    Task<bool> IsAvailableAsync(Product product);

    /// <summary>
    /// Asynchronously changes the availability flag of a product
    /// </summary>
    /// <param name="product">The product to change</param>
    /// <param name="isAvailable">The new availability value</param>
    /// <returns>The updated service</returns>
    Task<Product> SetAvailability(Product product, bool isAvailable);

    /// <summary>
    /// Asynchronously creates an order for a product
    /// </summary>
    /// <param name="by">The user which to create the order for</param>
    /// <param name="product">The product to order</param>
    /// <param name="quantity">The ordered quantity</param>
    /// <returns>The created order</returns>
    Task<ProductOrder> OrderAsync(User by, Product product, uint quantity);
}