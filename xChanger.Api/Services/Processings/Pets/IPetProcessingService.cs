//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.Pets;

namespace xChanger.Api.Services.Processings.Pets
{
    public interface IPetProcessingService
    {
        Pet ProcessPet(Pet pet);
    }
}
