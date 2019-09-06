using System;
using System.Collections.Generic;
using System.Text;
using Petshop.Core.Appservice;
using Petshop.Core.Entity;

namespace Petshop
{
    public class Printer : IPrinter
    {
        private readonly IPetService _petService;

        public Printer(IPetService petService)
        {
            _petService = petService;
        }

        public void BeginUI()
        {
            string[] menuItems =
            {
                "List All Pets",
                "Add a new Pet",
                "Delete a Pet in the system",
                "Update a Pet",
                "filter search for Pet type",
                "Exit"

            };
            var menuselection = ShowMenu(menuItems);

            while (menuselection != 6)
            {
                switch (menuselection)
                {
                    case 1:
                        var pets = _petService.GetAllPets();
                        ListPets(pets);
                        break;
                    case 2:
                        AddPet();
                        break;
                    case 3:
                        DeletePet();
                        break;
                    case 4:
                        UpdatePet();
                        break;
                    case 5:
                        SearchPetByType();
                        break;
                    default:
                        break;
                }

                menuselection = ShowMenu(menuItems);
            }
            Console.WriteLine("we hope you have enjoyed your experience");

    }

        private void SearchPetByType()
        {
            Console.WriteLine("write the type of pet you wish to search for: \n");
            string type;
            while ((type= Console.ReadLine()).Length==0)
            {
                Console.WriteLine("an example of a pet type: Fox, Cat, Dog, Tiger");
            }

            List<Pet> TypePetsReturned = _petService.SeachPetByType(type);
            Results(TypePetsReturned);
        }

        private void Results(List<Pet> typePetsReturned)
        {
            if (typePetsReturned.Count != 0)
            {
                Console.WriteLine("search brought back: ");
                foreach (var pet in typePetsReturned)
                {
                    Console.WriteLine(pet.Name + " " + pet.Type);
                }
            }
            else
            {
                Console.WriteLine("No Pets Found");
            }
        }

      

        private void UpdatePet()
        {
            var petidToupdate = FindPetByID();
            var PetToUpdate = _petService.GetByIdPet(petidToupdate);

            Console.WriteLine("update in progress " + PetToUpdate.Name + " " + PetToUpdate.Type);

            Console.WriteLine("pet's new name: ");
            string newPetName;
            while ((newPetName = Console.ReadLine()).Length ==0)
            {
                Console.WriteLine("Your pet needs a name");
            }

            Console.WriteLine(" new price for the pet: ");
            Console.WriteLine("\n year it was sold : ");
            int years;
            while ((!int.TryParse(Console.ReadLine(),out years)))
            {
                Console.WriteLine("a year consists of four digits... example 1996");
            }

            Console.WriteLine("month it was sold : ");
            int month;
            while ((!int.TryParse(Console.ReadLine(), out month)))
            {
                Console.WriteLine("we have 12 months");
            }
            Console.WriteLine(" the specific date it was sold : ");
            int day;
            while ((!int.TryParse(Console.ReadLine(), out day)))
            {
                Console.WriteLine("the days in a month range from between 1 to 28-31");
            }

            var birthDate = DateTime.Now.AddYears(years).AddMonths(month).AddDays(day);

            _petService.UpdatePet(new Pet()
            {
                Id = petidToupdate,
                Name = newPetName,
                SoldDate = birthDate,
                Price = PetToUpdate.Price,

            });
        }

        private void DeletePet()
        {
            var DeletePet = FindPetByID();
            _petService.DeletePet(DeletePet);
        }

        private int FindPetByID()
        {
            Console.WriteLine("put in pet id: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("please write a number");
            }

            return id;
        }

        private int ShowMenu(string[] menuItems)
        {
            Console.WriteLine("please make a selection\n");

            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine($"{(i + 1)}: {menuItems[i]}");
            }

            int menuselection;
            while (!int.TryParse(Console.ReadLine(),out menuselection)|| menuselection<1 ||menuselection>6)
            {
             Console.WriteLine("please select a number between 1-6");   
            }

            return menuselection;
        }

        private void AddPet()
        {
            Console.WriteLine("insert Pet type: ");
            string type;
            while ((type = Console.ReadLine()).Length ==0)
            {
                Console.WriteLine("pet needs a type");
            }

            Console.WriteLine("pet's name:");
            string name;
            while ((name = Console.ReadLine()).Length == 0)
            {
                Console.WriteLine("your pet needs a name");
            }
            
            Console.WriteLine("date of birth: ");
            int years;
            while (!int.TryParse(Console.ReadLine(),out years))
            {
                Console.WriteLine("what year was it born");
            }

            Console.WriteLine("what month was it born in");
            int month;
            while (!int.TryParse(Console.ReadLine(), out month))
            {
                Console.WriteLine("what month was it born");
            }

            Console.WriteLine("what day was it born on");
            int day;
            while (!int.TryParse(Console.ReadLine(), out day))
            {
                Console.WriteLine("what year was it born");
            }

            DateTime birthDate = DateTime.Today.AddYears(years).AddMonths(month).AddDays(day);

            Console.WriteLine("Colour: ");
            string colour;
            while ((colour = Console.ReadLine()).Length==0)
            {
                Console.WriteLine("a pet needs a colour to be identified");
            }

            Console.WriteLine("Price: ");
            double price;
            while (!double.TryParse(Console.ReadLine(),out price))
            {
                Console.WriteLine("how much is it gonna cost");
            }

            Console.WriteLine("who was the previous owner of the pet?");
            string previousOwner;
            while ((previousOwner = Console.ReadLine()).Length==0)
            {
                Console.WriteLine("this is not the black market, we can't accept pets without any previous owner");
            }

            Pet pet = _petService.NewPet(name, type, birthDate, colour, price, previousOwner);
            Console.WriteLine("pet has been added");
            _petService.CreatePet(pet);
        }

        private void ListPets(List<Pet> pets)
        {
            Console.WriteLine("\n system list of pets: ");
            foreach (var pet in pets)
            {
                Console.WriteLine($"Id: {pet.Id} Name: {pet.Name}" + $"Type: {pet.Type} Colour: {pet.Colour}" + $"Birthdate: {pet.BirthDate}");
                
            }
            Console.WriteLine("\n");
        }
    }
}
