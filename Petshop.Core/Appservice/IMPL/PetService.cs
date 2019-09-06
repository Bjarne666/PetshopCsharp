using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Petshop.Core.Domainservice;
using Petshop.Core.Entity;

namespace Petshop.Core.Appservice.IMPL
{
    public class PetService: IPetService

    {
        private readonly IPetRepository _petRepository;

        public PetService(IPetRepository petRepository)
        {
            _petRepository = petRepository;
        }

        public Pet NewPet(string name, string type, DateTime birthDate, string colour, double price, string previousowner)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidDataException("names are important");
            }

            if (string.IsNullOrEmpty(type))
            {
                throw new InvalidDataException("all pets are of certain type e.g. dog");
            }

            if (string.IsNullOrEmpty(colour))
            {
                throw new InvalidDataException("a pet is not invisible, it needs a colour");
            }

            if (birthDate > DateTime.Now)
            {
                throw new InvalidDataException("pets are born in the present not the future");
            }

            var pet = new Pet()
            {
                Name = name,
                Type = type,
                BirthDate = birthDate,
                Colour = colour,
                Price = price,
                PreviousOwner = previousowner
            };
            return pet;
        }

        public Pet CreatePet(Pet toCreate)
        {
            return _petRepository.CreatePet(toCreate);
        }

        public List<Pet> GetAllPets()
        {
            return _petRepository.GetAllPets().ToList();
        }

        public Pet UpdatePet(Pet toUpdate)
        {
            var pet = GetByIdPet(toUpdate.Id);
            pet.Name = toUpdate.Name;
            pet.Price = toUpdate.Price;
            pet.SoldDate = toUpdate.SoldDate;
            return pet;
        }

        public List<Pet> SeachPetByType(string type)
        {
            var List = _petRepository.GetAllPets();
            var PetTypeList = List.Where(pet => pet.Type.Equals(type));
            PetTypeList.OrderBy(pets => pets.Type);
            return PetTypeList.ToList();
        }

        public Pet GetByIdPet(int id)
        {
            return _petRepository.ReadById(id);
        }

        public Pet DeletePet(int id)
        {
            return _petRepository.Delete(id);
        }
    }
}
