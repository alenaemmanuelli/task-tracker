using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using App.Models;


namespace App.Managers
{
    public class ItemManager
    {
        //fields
        private List<Item> items;

        //constructor
        public ItemManager()
        {
            items = new List<Item>();
        }

        //add item, also generates id
        public void AddItem(string name, string type, string description = "No Description")
        {
            int id = GenID();

            Item newItem = type switch
            {
                "Habit" => new HabitItem(id, name, description),
                "Task" => new TaskItem(id, name, description),

                // fallback
                _ => new TaskItem(id, name, description)
            };
            
            items.Add(newItem);
        }

        //delete item, checks for id then removes from list
        public bool DeleteItem(int id)
        {
            Item? itemToDelete = FetchByID(id);

            if (itemToDelete != null)
            {
                items.Remove(itemToDelete);
                return true; // deletion successful
            }

            return false;
        }

        //edit item, checks for id then calls editdetails
        public bool EditItem(int id, string? name = null, string? description = null)
        {
            Item? itemToEdit = FetchByID(id);

            if (itemToEdit != null)
            {
                itemToEdit.EditDetails(name, description);
                return true;
            }

            return false;
        }

        //mark completion, checks for id then updates label with markcomplete
        public bool MarkCompletion(int id)
        {
            Item? item = FetchByID(id);

            if (item != null)
            {
                item.MarkComplete();
                return true;
            }

            return false;
        }

        //helpers
        private int GenID()
        {
            Random rand = new Random();
            int id;

            do
            {
                id = rand.Next(0, 1000);
            }
            while (items.Any(item => item.ID == id));

            return id;
        }

        public Item? FetchByID(int id)
        {
            return items.FirstOrDefault(item => item.ID == id);
        }
        
        public List<Item> GetAllItems()
        {
            return items;
        }

        public void Clear()
        {
            items.Clear();
        }

        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            TypeInfoResolver = AppJsonContext.Default
        }; 

        public void Save(string filepath)
        {
            var json = JsonSerializer.Serialize(items, JsonOptions);
            File.WriteAllText(filepath, json);
        }

        public void Load(string filepath)
        {
            if (File.Exists(filepath))
            {
                var json = File.ReadAllText(filepath);
                var loaded = JsonSerializer.Deserialize<List<Item>>(json, JsonOptions);

                if (loaded != null)
                    items = loaded;
            }
        }
    }
}