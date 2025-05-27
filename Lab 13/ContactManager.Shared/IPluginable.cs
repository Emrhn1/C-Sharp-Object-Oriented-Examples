using System.Collections.Generic;

namespace ContactManager.Shared
{
    public interface IPluginable
    {
        void Save(List<Contact> contacts, string filePath);
        List<Contact> Load(string filePath);
        string Format { get; }
    }
} 