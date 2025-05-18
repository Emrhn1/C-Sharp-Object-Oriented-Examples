using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Odev03
{
   public class AnimalManager
    {
        public List<Pet> Animals = new List<Pet>();
        public List<IPlants> Herbivores = new List<IPlants>();
        public List<ICarnivorous> Carnivores= new List<ICarnivorous>();

        public void AddPet(Pet pet)
        {
            Animals.Add(pet);
        }

        public void CopyAnimalsToLists()
        {
            foreach (var animal in Animals)
            {
                if (animal is Piggy pig)
                {
                    Herbivores.Add(pig as IPlants);
                    Carnivores.Add(pig as ICarnivorous);
                }
                else
                {
                    if (animal is IPlants herbivore)
                        Herbivores.Add(herbivore);
                    if (animal is ICarnivorous carnivore)
                        Carnivores.Add(carnivore);
                }
            }
        }


        public void FeedAllHerbivores()
        {
            foreach (var herbivore in Herbivores)
            {
                herbivore.FindFood();
                herbivore.EatPlant();
            }
        }

        public void FeedAllCarnivores()
        {
            foreach (var carnivore in Carnivores)
            {
                carnivore.FindFood();
                carnivore.EatMeat();
            }
        }
    }
}
