using System;

namespace ConsoleBasedInventory.Utils
{
    /// <summary>
    /// The Formatter class provides utility methods for formatting data to be displayed in the console.
    /// </summary>
    public static class Formatter
    {
        /// <summary>
        /// Formats a given decimal amount as a currency string.
        /// The method will format the decimal amount based on the system's current culture settings, 
        /// showing the correct currency symbol and using appropriate formatting.
        /// </summary>
        /// <param name="amount">The decimal amount to be formatted.</param>
        /// <returns>A formatted currency string depending on system locale).</returns>
        public static string FormatCurrency(decimal amount)
        {
            // Formats the decimal amount as currency, using the default culture (including symbol, commas, and decimals)
            return string.Format("{0:C}", amount);
        }
    }
}
