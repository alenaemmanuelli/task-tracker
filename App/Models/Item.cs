using System.Text.Json.Serialization;

namespace App.Models
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(TaskItem), "Task")]
    [JsonDerivedType(typeof(HabitItem), "Habit")]
    
    public abstract class Item
    {
        [JsonInclude]
        public int ID { get; init; }

        public string Name { get; set; }
        public string Description { get; set; }

        [JsonInclude]
        public string Label { get; set; } = "Incomplete";

        [System.Text.Json.Serialization.JsonIgnore]
        public bool IsComplete => Label != "Incomplete";

        [System.Text.Json.Serialization.JsonIgnore]
        public abstract string Type { get; }

        protected Item(int id, string name, string description)
        {
            ID = id;
            Name = name;
            Description = description;
        }

        public abstract void MarkComplete();

        public void EditDetails(string? name = null, string? description = null)
        {
            if (name != null) Name = name;
            if (description != null) Description = description;
        }

        public override string ToString()
        {
            return $"[{ID}] {Name} ({Type}) - {Label}: {Description}";
        }
    }
}
