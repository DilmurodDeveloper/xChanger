//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.Pets;

namespace xChanger.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Pet> InsertPetAsync(Pet pet);
        IQueryable<Pet> SelectAllPets();
        ValueTask<Pet> SelectPetByIdAsync(Guid petId);
        ValueTask<Pet> UpdatePetAsync(Pet pet);
    }
}
