//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.Pets;

namespace xChanger.Api.Services.Processings.Pets
{
    public class PetProcessingService : IPetProcessingService
    {
        public Pet ProcessPet(Pet pet)
        {
            return new Pet
            {
                Id = pet.Id,
                Name = pet.Name?.Trim(),
                Type = pet.Type
            };
        }
    }
}
