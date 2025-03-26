using System;
using System.Collections.Generic;
using DocumentLibrary; 
using DocumentLibrary.Models; 
using DocumentLibrary.Exceptions;

namespace Odev02Konsolu
{
    class Program
    {
        static void Main(string[] args)
        {
            DocumentManager manager = new DocumentManager();

            try
            {
                manager.AddDocument(new Book("123-456-789", "C# Programming", 2023, 350, "John Doe"));
                manager.AddDocument(new Volume("987-654-321", "Advanced Topics", 2021, 400, 2, 5));
                manager.AddDocument(new Magazine("456-789-123", "Tech Monthly", 2022, 60, 10, FrequencyType.Monthly));

                Console.WriteLine("All Documents:");
                foreach (var doc in manager.FindMagazinesByFrequency(FrequencyType.Unknown))
                {
                    Console.WriteLine(doc);
                }



                Console.WriteLine("\nDocuments with 'Tech' in the title:");
                var foundDocs = manager.FindByTitlePhrase("Tech");
                foreach (var doc in foundDocs)
                {
                    Console.WriteLine(doc);
                }


                Console.WriteLine("\nMonthly Magazines:");
                var monthlyMags = manager.FindMagazinesByFrequency(FrequencyType.Monthly);
                foreach (var mag in monthlyMags)
                {
                    Console.WriteLine(mag);
                }


                Console.WriteLine("\nPrinting documents:");
                foreach (var doc in manager.FindMagazinesByFrequency(FrequencyType.Unknown))
                {
                    Console.WriteLine(doc.Print());
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Hata: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
