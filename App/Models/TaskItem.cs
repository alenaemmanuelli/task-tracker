using System;
using App.Models;  

namespace App.Models
{
    public class TaskItem : Item
    {
        public override string Type => "Task";

        public TaskItem(int id, string name, string description)
            : base(id, name, description)
        {
        }

        public override void MarkComplete()
        {
            if (Label == "Complete")
            {
                Console.WriteLine($"Task '{Name}' is already complete.");
                return;
            }

            Label = "Complete";
            Console.WriteLine($"Task '{Name}' marked complete!");
        }
    }
}
