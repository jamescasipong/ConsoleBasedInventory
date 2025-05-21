using ConsoleBasedInventory.Interface;
using ConsoleBasedInventory.Models;
using ConsoleBasedInventory.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBasedInventory.Manager
{
    /// <summary>
    /// ConsoleManager is responsible for managing and interacting with the inventory via the console UI.
    /// It handles user inputs for adding, removing, updating, and listing products.
    /// </summary>
    public class ConsoleManager : IConsoleManager
    {
        private readonly IInventoryManager inventoryManager;  // Inventory manager that manages product operations
        private bool _exit = false;  // Flag to control the main application loop 
        private readonly IEnumerable<Product> products = new List<Product> // Predefined list of products
        {
            // Electronics
            new Product(1, "Smartphone", 150, 9999.99m), // Popular smartphones in the Philippines
            new Product(2, "Laptop", 80, 45000.00m),    // Laptops are essential for students and professionals
            new Product(3, "Bluetooth Headphones", 200, 2500.00m), // Affordable Bluetooth headphones

            // Kitchen Appliances
            new Product(4, "Rice Cooker", 120, 2500.00m), // Essential appliance for Filipino households
            new Product(5, "Blender", 100, 1500.00m),    // Blender for making shakes and smoothies
            new Product(6, "Electric Fan", 150, 2000.00m), // Widely used during the hot seasons in the Philippines

            // Home Decor
            new Product(7, "Wall Clock", 50, 500.00m),    // Wall clock is a basic home decoration
            new Product(8, "Throw Blanket", 75, 1000.00m), // Cozy blankets, perfect for cooler nights

            // Outdoor & Sports
            new Product(9, "Camping Tent", 60, 4000.00m), // For camping trips in the mountains or beaches
            new Product(10, "Yoga Mat", 100, 800.00m)    // Yoga mats for fitness enthusiasts
        };

        /// <summary>
        /// Initializes a new instance of the ConsoleManager class.
        /// </summary>
        /// <param name="inventoryManager">An instance of IInventoryManager used for inventory operations.</param>
        public ConsoleManager(IInventoryManager inventoryManager)
        {
            this.inventoryManager = inventoryManager;
        }

        // Predefined list of products to initialize the inventory
        

        /// <summary>
        /// Main entry point of the console application. Runs the inventory management system.
        /// Allows the user to interact with the inventory through a menu of options.
        /// </summary>
        public void Run()
        {
            // Set the console output encoding to UTF-8 to support special characters
            Console.OutputEncoding = System.Text.Encoding.UTF8;   

            // Initialize the inventory with the predefined products
            Initilize(products);
            Console.WriteLine("Welcome to the Inventory Management System!");

            // Main loop for the console application
            while (!_exit)
            {
                Console.WriteLine("\nInventory Management System");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Remove Product");
                Console.WriteLine("3. Update Product");
                Console.WriteLine("4. List Products");
                Console.WriteLine("5. Exit");

                Console.Write("Please select an option (1-5): ");


                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                    continue;
                }

                try
                {
                    // Switch based on the user's choice
                    switch (choice)
                    {
                        case 1:
                            AddProductUI();
                            break;
                        case 2:
                            RemoveProductUI();
                            break;
                        case 3:
                            UpdateProductUI();
                            break;
                        case 4:
                            ListProductsUI();
                            break;
                        case 5:
                            _exit = true;  // Exit the application
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occur during the operations
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        private void Initilize(IEnumerable<Product> products)
        {
            foreach (var product in products)
            {
                inventoryManager.AddProduct(product);
            }
        }

        /// <summary>
        /// Displays the user interface for adding a new product to the inventory.
        /// Validates input and adds the product to the inventory.
        /// </summary>
        public void AddProductUI()
        {
            string inputName = TypeHelper.GetValueFromInput<string>("Enter a product name: ", x => !string.IsNullOrWhiteSpace(x), "Invalid input. Please enter a valid product name.");

            int inputQuantity = TypeHelper.GetValueFromInput<int>("Enter the quantity: ", x => NumberValidator(x), "Invalid input. Please enter a valid product quantity.");

            decimal inputPrice = TypeHelper.GetValueFromInput<decimal>("Enter the price: ", x => decimal.TryParse(x, out _), "Invalid input. Please enter a valid product price.");

            // Generate a unique product ID by finding the maximum current ID and incrementing it
            int newProductId = inventoryManager.ListProducts().Select(a => a.ProductId).DefaultIfEmpty(0).Max() + 1;

            // Create a new product and add it to the inventory
            inventoryManager.AddProduct(new Product(newProductId, inputName, inputQuantity, inputPrice));

            Console.WriteLine("Product added successfully.");
        }

        /// <summary>
        /// Displays the list of all products in the inventory, along with their details.
        /// </summary>
        public void ListProductsUI()
        {
            int width = 20;

            // Set up the header row with column names
            string header = "Id".PadRight(width) + " " + "Name".PadRight(width) + " " + "Stock".PadRight(width) + " " + "Price".PadRight(width);
            Console.WriteLine(header);
            Console.WriteLine(new string('-', header.Length));

            // Iterate through each product in the inventory and display its details
            foreach (var product in inventoryManager.ListProducts())
            {
                int productId = product.ProductId;
                string productName = product.Name;
                int productQuantity = product.QuantityInStock;
                decimal productPrice = product.Price;

                // Limit th product name to 15 characters and add ellipsis if necessary
                string truncatedName = productName.Length > 15 ? productName.Substring(0, 14) + "..." : productName.PadRight(width);

                // Display product details in a formatted way
                Console.WriteLine(
                    productId.ToString().PadRight(width) + " " +
                    truncatedName.PadRight(width) + " " +
                    productQuantity.ToString().PadRight(width) + " " +
                    Formatter.FormatCurrency(productPrice).PadRight(width)
                );
            }

            // Print a separator and the total value of all products
            Console.WriteLine(new string('-', header.Length));
            Console.WriteLine("Total Value: ".PadRight(width) + Formatter.FormatCurrency(inventoryManager.GetTotalValue()).PadRight(width));
        }


        /// <summary>
        /// Displays the user interface for removing a product from the inventory.
        /// Validates the input and removes the product based on the given ID.
        /// </summary>
        public void RemoveProductUI()
        {
            int inputProductId = TypeHelper.GetValueFromInput<int>("Enter the product ID to remove: ", x => NumberValidator(x), "Invalid input. Please enter a valid product ID.");

            try
            {
                inventoryManager.RemoveProduct(inputProductId);
                Console.WriteLine("Product removed successfully.");
            }
            catch (Exception ex)
            {
                // Handle exceptions such as attempting to remove a non-existent product
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Displays the user interface for updating the quantity of a product.
        /// Validates input and updates the quantity for the specified product ID.
        /// </summary>
        public void UpdateProductUI()
        {
            int inputProductId = TypeHelper.GetValueFromInput<int>("Enter the product ID to update: ", x => NumberValidator(x), "Invalid input. Please enter a valid product ID.");

            int inputQuantity = TypeHelper.GetValueFromInput<int>("Enter the new quantity: ", x => NumberValidator(x), "Invalid input. Please enter a valid quantity.");

            try
            {
                inventoryManager.UpdateProduct(inputProductId, inputQuantity);
                Console.WriteLine("Product updated successfully.");
            }
            catch (Exception ex)
            {
                // Handle exceptions such as invalid product IDs
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Validator function for checking if the input is a valid positive integer.
        /// </summary>
        /// <param name="input">The input string to validate.</param>
        /// <returns>True if the input is a valid positive integer, otherwise false.</returns>
        private bool NumberValidator(string input)
        {
            return int.TryParse(input, out int id) && id > 0;
        }
    }
}
