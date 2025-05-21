using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBasedInventory.Models
{
    /// <summary>
    /// Represents a product in the inventory, with details like ID, name, quantity, and price.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of the product available in stock.
        /// </summary>
        public int QuantityInStock { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Calculates the total value of the product (quantity in stock multiplied by price).
        /// </summary>
        private decimal TotalValue => QuantityInStock * Price;

        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        /// <param name="productId">The unique identifier for the product.</param>
        /// <param name="name">The name of the product.</param>
        /// <param name="quantityInStock">The quantity of the product available in stock.</param>
        /// <param name="price">The price of the product.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the product ID, quantity, or price is invalid.</exception>
        /// <exception cref="ArgumentException">Thrown if the product name is empty.</exception>
        public Product(int productId, string name, int quantityInStock, decimal price)
        {
            ValidateProductDetails(productId, name, quantityInStock, price);

            ProductId = productId;
            Name = name;
            QuantityInStock = quantityInStock;
            Price = price;
        }

        /// <summary>
        /// Validates the product details to ensure they are within valid ranges.
        /// </summary>
        /// <param name="productId">The product ID to validate.</param>
        /// <param name="name">The product name to validate.</param>
        /// <param name="quantity">The quantity in stock to validate.</param>
        /// <param name="price">The price of the product to validate.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the product ID, quantity, or price is invalid.</exception>
        /// <exception cref="ArgumentException">Thrown if the product name is empty or invalid.</exception>
        private void ValidateProductDetails(int productId, string name, int quantity, decimal price)
        {
            if (productId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ProductId), "Product ID must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Product name cannot be empty.");
            }
            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(QuantityInStock), "Quantity in stock cannot be negative.");
            }
            if (price < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(Price), "Price cannot be negative.");
            }
        }

        /// <summary>
        /// Gets the total value of the product (quantity in stock multiplied by the price).
        /// </summary>
        /// <returns>The total value of the product in decimal.</returns>
        public decimal GetTotalValue()
        {
            return TotalValue;
        }

        /// <summary>
        /// Returns a string representation of the product's details.
        /// </summary>
        /// <returns>A string containing the product's ID, name, quantity in stock, and price.</returns>
        public override string ToString()
        {
            return $"Product ID: {ProductId}, Name: {Name}, Quantity in Stock: {QuantityInStock}, Price: {Price:C}";
        }
    }
}
