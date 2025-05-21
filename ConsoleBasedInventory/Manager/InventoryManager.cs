using ConsoleBasedInventory.Interface;
using ConsoleBasedInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleBasedInventory.Manager
{
    internal class InventoryManager : IInventoryManager
    {
        /// <summary>
        /// Internal list of products managed by the inventory.
        /// </summary>
        private List<Product> _products = new List<Product>();

        /// <summary>
        /// Adds a new product to the inventory.
        /// </summary>
        /// <param name="product">The product to add.</param>
        /// <exception cref="ArgumentException">Thrown when a product with the same ID already exists.</exception>
        public void AddProduct(Product product)
        {
            // Validate the product details before adding
            var existingProduct = _products.FirstOrDefault(p => p.ProductId == product.ProductId);


            // If the product already exists, throw an exception
            if (existingProduct != null)
                throw new ArgumentException("Product with the same ID already exists.");

            _products.Add(product);
        }

        /// <summary>
        /// Calculates the total value of all products in the inventory.
        /// </summary>
        /// <returns>The total inventory value as a decimal.</returns>
        public decimal GetTotalValue()
        {
            /// Calculate the total value of all products in the inventory
            return _products.Select(a => a.GetTotalValue()).Sum();
        }

        /// <summary>
        /// Returns a list of all products in the inventory.
        /// </summary>
        /// <returns>An enumerable collection of products.</returns>
        public IEnumerable<Product> ListProducts()
        {
            return _products;
        }

        /// <summary>
        /// Removes a product from the inventory based on its product ID.
        /// </summary>
        /// <param name="productId">The ID of the product to remove.</param>
        /// <exception cref="ArgumentException">Thrown when the product is not found.</exception>
        public void RemoveProduct(int productId)
        {
            // Check if the product exists in the inventory
            var product = _products.FirstOrDefault(p => p.ProductId == productId);

            // If the product is not found, throw an exception
            if (product == null)
                throw new ArgumentException("Product not found.");

            _products.Remove(product);
        }

        /// <summary>
        /// Updates the quantity of an existing product in the inventory.
        /// </summary>
        /// <param name="productId">The ID of the product to update.</param>
        /// <param name="newQuantity">The new quantity to assign.</param>
        /// <exception cref="ArgumentException">Thrown when the product is not found.</exception>
        public void UpdateProduct(int productId, int newQuantity)
        {
            // Check if the product exists in the inventory
            var product = _products.FirstOrDefault(p => p.ProductId == productId);

            // If the product is not found, throw an exception
            if (product == null)
                throw new ArgumentException("Product not found.");

            product.QuantityInStock = newQuantity;
        }
    }
}
