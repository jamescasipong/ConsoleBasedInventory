using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBasedInventory.Interface
{
    /// <summary>
    /// Provides UI methods for interacting with the console-based inventory system.
    /// </summary>
    public interface IConsoleManager
    {
        /// <summary>
        /// Starts the main application loop for console interaction.
        /// </summary>
        void Run();

        /// <summary>
        /// Displays the UI for adding a new product to the inventory.
        /// </summary>
        void AddProductUI();

        /// <summary>
        /// Displays the UI for removing a product from the inventory.
        /// </summary>
        void RemoveProductUI();

        /// <summary>
        /// Displays the UI for updating an existing product in the inventory.
        /// </summary>
        void UpdateProductUI();

        /// <summary>
        /// Displays a list of all products in the inventory.
        /// </summary>
        void ListProductsUI();
    }
}
