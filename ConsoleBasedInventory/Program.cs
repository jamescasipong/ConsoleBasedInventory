
using ConsoleBasedInventory.Interface;
using ConsoleBasedInventory.Manager;


// Create an instance of the InventoryManager class thrugh the IInventoryManager interface
IInventoryManager inventoryManager = new InventoryManager();

// Create an instance of the ConsoleManager class through the IConsoleManager interface
// Inject the IInventoryManager instance into the ConsoleManager
IConsoleManager consoleManager = new ConsoleManager(inventoryManager);


// Run the console manager to start the application
consoleManager.Run();