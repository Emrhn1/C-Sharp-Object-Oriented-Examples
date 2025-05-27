using System.Text.Json;
using ContactManager.Shared;

namespace ContactManager.JsonPlugin
{
    [Info("Your Name")]
    public class JsonPlugin : IPluginable
    {
        public string Format => "JSON";

        public List<Contact> Load(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Contact>>(jsonString) ?? new List<Contact>();
        }

        public void Save(List<Contact> contacts, string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(contacts, options);
            File.WriteAllText(filePath, jsonString);
        }
    }
} 