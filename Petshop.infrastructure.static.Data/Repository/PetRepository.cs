using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Petshop.Core.Domainservice;
using Petshop.Core.Entity;

namespace Petshop.Infrastructure.Static.Data.Repository
{
   public class PetRepository: IPetRepository
   {
       
        public Pet CreatePet(Pet newPet)
        {
            newPet.Id = FakeDb.Id++;
            FakeDb.AllPets.ToList().Add(newPet);
            return newPet;
        }

        public Pet ReadById(int id)
        {
            return FakeDb.AllPets.FirstOrDefault(pet => pet.Id == id);
        }

        public IEnumerable<Pet> GetAllPets()
        {
            return FakeDb.AllPets;
        }

        public Pet Update(Pet toUpdate)
        {
            var petFromDatabase = ReadById(toUpdate.Id);
            if (petFromDatabase == null)
            {
                return null;
            }

            petFromDatabase.Name = toUpdate.Name;
            petFromDatabase.Price = toUpdate.Price;
            petFromDatabase.SoldDate = toUpdate.SoldDate;
            return petFromDatabase;
        }

        public Pet Delete(int id)
        {
            var foundPet = ReadById(id);
            if (foundPet != null)
            {
                return null;
            }
            FakeDb.AllPets.ToList().Remove(foundPet);
            return foundPet;
        }
    }
}
