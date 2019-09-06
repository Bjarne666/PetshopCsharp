using System;
using System.Collections.Generic;
using System.Text;
using Petshop.Core.Entity;

namespace Petshop.Core.Domainservice
{
    public interface IPetRepository
    {
        Pet CreatePet(Pet newPet);
        Pet ReadById(int id);
        IEnumerable<Pet> GetAllPets();
        Pet Update(Pet petUpdate);
        Pet Delete(int id);
    }
}
