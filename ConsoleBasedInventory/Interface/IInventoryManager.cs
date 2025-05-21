using ConsoleBasedInventory.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBasedInventory.Interface
{
    /// <summary>
    /// Defines the operations or blueprint for managing a product inventory.
    /// </summary>
    public interface IInventoryManager
    {
        /// <summary>
        /// Adds a product to the inventory.
        /// </summary>
        /// <param name="product">The product to add.</param>
        void AddProduct(Product product);

        /// <summary>
        /// Calculates the total value of all products in the inventory.
        /// </summary>
        /// <returns>The total inventory value.</returns>
        decimal GetTotalValue();

        /// <summary>
        /// Retrieves all products in the inventory.
        /// </summary>
        /// <returns>A collection of products.</returns>
        IEnumerable<Product> ListProducts();

        /// <summary>
        /// Removes a product from the inventory.
        /// </summary>
        /// <param name="productId">The ID of the product to remove.</param>
        void RemoveProduct(int productId);

        /// <summary>
        /// Updates the quantity of a product in the inventory.
        /// </summary>
        /// <param name="productId">The ID of the product to update.</param>
        /// <param name="newQuantity">The new quantity value.</param>
        void UpdateProduct(int productId, int newQuantity);
    }
}
