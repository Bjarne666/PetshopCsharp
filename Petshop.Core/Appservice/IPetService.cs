using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Petshop.Core;
using Petshop.Core.Entity;

namespace Petshop.Core.Appservice
{
    public interface IPetService
    {
        Pet NewPet(string name, string type, DateTime birthDate, string colour, double price, string previousowner);
        Pet CreatePet(Pet toCreate);
        List<Pet> GetAllPets();
        Pet UpdatePet(Pet toUpdate);
        List<Pet> SeachPetByType(string type);
        Pet GetByIdPet(int id);
        Pet DeletePet(int id);
    }
}