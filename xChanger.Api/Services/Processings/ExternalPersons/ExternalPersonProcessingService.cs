//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.ExternalPersons;
using xChanger.Api.Services.Foundations.ExternalPersons;

namespace xChanger.Api.Services.Processings.ExternalPersons
{
    public class ExternalPersonProcessingService : IExternalPersonProcessingService
    {
        private readonly IExternalPersonService externalPersonService;

        public ExternalPersonProcessingService(
            IExternalPersonService externalPersonService) =>
                this.externalPersonService = externalPersonService;

        public async ValueTask<List<ExternalPerson>> RetrieveFormattedExternalPersonAsync()
        {
            var retrievedExternalPersonPets =
                await this.externalPersonService.RetrieveAllExternalPersonsAsync();

            List<ExternalPerson> formattedExternalPersonPets =
                FormatProperties(retrievedExternalPersonPets);

            return formattedExternalPersonPets;
        }

        private List<ExternalPerson> FormatProperties(List<ExternalPerson> retrievedExternalPersonPets)
        {
            var formattedExternalPersonPets = retrievedExternalPersonPets.Select(retrievedPersonPet =>
                new ExternalPerson()
                {
                    PersonName = retrievedPersonPet.PersonName,
                    Age = retrievedPersonPet.Age,
                    PetOne = retrievedPersonPet.PetOne.Trim().Replace("-", string.Empty),
                    PetOneType = retrievedPersonPet.PetOneType.Trim().Replace("-", string.Empty),
                    PetTwo = retrievedPersonPet.PetTwo.Trim().Replace("-", string.Empty),
                    PetTwoType = retrievedPersonPet.PetTwoType.Trim().Replace("-", string.Empty),
                    PetThree = retrievedPersonPet.PetThree.Trim().Replace("-", string.Empty),
                    PetThreeType = retrievedPersonPet.PetThreeType.Trim().Replace("-", string.Empty),
                });

            return formattedExternalPersonPets.ToList();
        }
    }
}
