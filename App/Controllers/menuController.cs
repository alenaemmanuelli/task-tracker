using App.Managers;
using App.Models;

namespace App.Controllers
{
    public class MenuController
    {
        private ItemManager itemManager;
        private const string FILENAME = "items.json";

        public MenuController(ItemManager manager)
        {
            itemManager = manager;
        }

        private void PauseMenu()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void DisplayItemsList()
        {
            var items = itemManager.GetAllItems();
            if (items.Count == 0)
            {
                Console.WriteLine("No items found.");
                return;
            }

            Console.WriteLine("=== Items ===");
            foreach (var item in items)
            {
                Console.WriteLine($"ID: {item.ID} | {item.Name}");
            }
        }

        private string? ConvertEmptyToNull(string? input)
        {
            return string.IsNullOrEmpty(input) ? null : input;
        }

        public void Run()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== Habit Tracker Menu ===");
                Console.WriteLine("1. View List");
                Console.WriteLine("2. Add Item");
                Console.WriteLine("3. Manage Item");
                Console.WriteLine("4. Mark Complete");
                Console.WriteLine("5. Save Data");
                Console.WriteLine("6. Load Data");
                Console.WriteLine("7. Exit");
                Console.WriteLine("8. Wipe List & Save File (Reset)");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        ViewList();
                        break;

                    case "2":
                        AddItemMenu();
                        break;

                    case "3":
                        ManageItemMenu();
                        break;

                    case "4":
                        MarkCompletionMenu();
                        break;

                    case "5":
                        SaveDataMenu();
                        break;

                    case "6":
                        LoadDataMenu();
                        break;
                        
                    case "7":
                        running = false;
                        break;

                    case "8":
                        WipeDataMenu();
                        break;
                }
            }
        }

        private void ViewList()
        {
            Console.Clear();
            var items = itemManager.GetAllItems();
            if (items.Count == 0)
            {
                Console.WriteLine("No items found.");
                PauseMenu();
                return;
            }
            
            foreach (var item in items)
                {
                    string status;
                    if (item is HabitItem habit)
                    {
                        status = $"[{habit.CompletionCount}]";
                    }
                    else
                    {
                        status = item.IsComplete ? "[✓]" : "[ ]";
                    }
                    Console.WriteLine($"{status} ID: {item.ID} | {item.Name} ({item.Type}) | {item.Description}");
                }
            
            PauseMenu();
        }

        private void AddItemMenu()
        {
            Console.Clear();
            string name = "";
            while (string.IsNullOrEmpty(name))
            {
                Console.Write("Enter item name: ");
                name = Console.ReadLine() ?? "";
                if (string.IsNullOrEmpty(name))
                    Console.WriteLine("Item name cannot be empty. Please try again.");
            }

            string type = "";
            while(true)
            {
                Console.WriteLine("\nPick type:");
                Console.WriteLine("1. Task");
                Console.WriteLine("2. Habit");
                Console.Write("Enter choice (1 or 2): ");
                
                string typeChoice = Console.ReadLine() ?? "";

                if (typeChoice == "1")
                {
                    type = "Task";
                    break;
                }
                if (typeChoice == "2")
                {
                    type = "Habit";
                    break;
                }

                Console.WriteLine("Invalid input. Type '1' for Task or '2' for Habit");
            }
            
            
            Console.Write("Enter description (optional): ");
            string description = Console.ReadLine() ?? "No Description";

            itemManager.AddItem(name, type, description);

            Console.WriteLine("Item added successfully!");
            PauseMenu();
        }

        private void ManageItemMenu()
        {
            Console.Clear();
            var items = itemManager.GetAllItems();
            
            if (items.Count == 0)
            {
                Console.WriteLine("No items found.");
                PauseMenu();
                return;
            }

            DisplayItemsList();

            Console.WriteLine();
            Console.Write("Enter item ID to manage: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("1. Edit Item");
                Console.WriteLine("2. Delete Item");
                Console.Write("Enter choice (1 or 2): ");

                string choice = Console.ReadLine() ?? "";

                if (choice == "1")
                {
                    Console.Write("Enter new name (leave blank to skip): ");
                    string? name = Console.ReadLine();
                    Console.Write("Enter new description (leave blank to skip): ");
                    string? description = Console.ReadLine();

                    if (itemManager.EditItem(id, ConvertEmptyToNull(name), ConvertEmptyToNull(description)))
                        Console.WriteLine("Item updated successfully!");
                    else
                        Console.WriteLine("Item not found.");
                }
                else if (choice == "2")
                {
                    if (itemManager.DeleteItem(id))
                        Console.WriteLine("Item deleted successfully!");
                    else
                        Console.WriteLine("Item not found.");
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid ID.");
            }
            PauseMenu();
        }

        private void MarkCompletionMenu()
        {
            Console.Clear();
            var items = itemManager.GetAllItems();
            
            if (items.Count == 0)
            {
                Console.WriteLine("No items found.");
                PauseMenu();
                return;
            }

            DisplayItemsList();

            Console.WriteLine();
            Console.Write("Enter item ID to mark complete: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (!itemManager.MarkCompletion(id))
                    Console.WriteLine("Item not found.");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid ID.");
            }
            
            PauseMenu();
        }

        private void SaveDataMenu()
        {
            itemManager.Save(FILENAME);
            Console.WriteLine($"Data saved to {FILENAME}");
            PauseMenu();
        }

        private void LoadDataMenu()
        {
            itemManager.Load(FILENAME);
            Console.WriteLine($"Data loaded from {FILENAME}");
            PauseMenu();
        }

        private void WipeDataMenu()
        {
            itemManager.Clear();
            itemManager.Save(FILENAME);
            Console.WriteLine("All items have been wiped and saved!");
            PauseMenu();
        }

    }
}
