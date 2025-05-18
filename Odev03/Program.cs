using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev03
{
    class Program
    {
        static void Main(string[] args)
        {
           AnimalManager animalManager=new AnimalManager();

            var sheep = new Sheep("Dolly", 2, 50);
            var wolf=new Wolf("Wolfie", 3, 70);
            var pig=new Piggy("Piggy", 4, 60);

            animalManager.AddPet(sheep);
            animalManager.AddPet(wolf);
            animalManager.AddPet(pig);

            animalManager.CopyAnimalsToLists();

            Console.WriteLine(" \n Feeding harbivores:");
            animalManager.FeedAllHerbivores();

            Console.WriteLine("\n Feeding carnivores:");
            animalManager.FeedAllCarnivores();

        }
    }
}
