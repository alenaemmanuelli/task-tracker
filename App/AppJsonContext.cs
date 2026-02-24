using System.Text.Json.Serialization;
using App.Models;

namespace App
{
    [JsonSerializable(typeof(List<Item>))]

    public partial class AppJsonContext : JsonSerializerContext{}
}