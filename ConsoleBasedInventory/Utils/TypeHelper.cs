using System;

namespace ConsoleBasedInventory.Utils
{
    public static class TypeHelper
    {
        /// <summary>
        /// Prompts the user for input, validates the input using a provided validation function,
        /// and converts the input to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to convert the input to.</typeparam>
        /// <param name="prompt">The prompt message to display to the user.</param>
        /// <param name="validate">A function to validate the user input.</param>
        /// <param name="errorMessage">The error message to display if the input is invalid.</param>
        /// <returns>The converted input value of type T.</returns>
        public static T GetValueFromInput<T>(string prompt, Func<string, bool> validate, string errorMessage)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()!;

                // Validate the input using the provided validation function
                if (validate(input))
                {
                   // Attempt to convert the input to the specified type
                   return (T)Convert.ChangeType(input, typeof(T));
                    
                }

                // Display the provided error message if validation fails
                Console.WriteLine(errorMessage);

            }
        }
    }
}
