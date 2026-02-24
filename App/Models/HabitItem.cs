using System;
using System.Text.Json.Serialization;
using App.Models;

namespace App.Models
{
    public class HabitItem : Item
    {
        public override string Type => "Habit";

        [JsonInclude]
        public int CompletionCount { get; set; } = 0;

        public HabitItem(int id, string name, string description)
            : base(id, name, description)
        {
        }

        public override void MarkComplete()
        {
            CompletionCount++;
            Label = $"Completed {CompletionCount} time(s)";
            Console.WriteLine($"Habit '{Name}' has been completed {CompletionCount} time(s)!");
        }
    }
}
