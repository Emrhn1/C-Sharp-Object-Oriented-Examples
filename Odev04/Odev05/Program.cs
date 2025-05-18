using Odev05;
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        int x = 5, y = 10;
        Console.WriteLine($"Before swap: x={x}, y={y}");
        GenericMethods.Swap(ref x, ref y);
        Console.WriteLine($"After swap: x={x}, y={y}");

        // 2. DisplayAndReset metodu testi
        GenericMethods.DisplayAndReset("Hello", 42);

        // 3. CreateInstance metodu testi
        var newHuman = GenericMethods.CreateInstance<Human>();
        Console.WriteLine($"New human created: {newHuman}");

        Console.WriteLine($"Max of 5 and 10: {GenericMethods.Max(5, 10)}");

        // 5. SortParams metodu testi
        var sorted = GenericMethods.SortParams(3, 1, 4, 1, 5, 9, 2);
        Console.WriteLine($"Sorted numbers: {string.Join(", ", sorted)}");

        // 6. CreateDictionary metodu testi
        var dict = GenericMethods.CreateDictionary(1, "One");
        Console.WriteLine("Dictionary created:");
        GenericMethods.DisplayDictionary(dict);

        // 7. DisplayDictionary metodu testi
        var testDict = new Dictionary<int, string> { { 1, "One" }, { 2, "Two" } };
        Console.WriteLine("Displaying dictionary:");
        GenericMethods.DisplayDictionary(testDict);

        // 8. CreateCollection metodu testi
        var smallCollection = GenericMethods.CreateCollection(1, 2);
        Console.WriteLine($"Small collection type: {smallCollection.GetType().Name}");
        var largeCollection = GenericMethods.CreateCollection(1, 2, 3, 4);
        Console.WriteLine($"Large collection type: {largeCollection.GetType().Name}");

    }
}
