//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.ExternalPersons;
using xChanger.Api.Models.Foundations.Persons;
using xChanger.Api.Models.Foundations.Pets;
using xChanger.Api.Models.Orchestrations;
using xChanger.Api.Services.Orchestrations.ExternalPersons;
using xChanger.Api.Services.Orchestrations.PersonPets;

namespace xChanger.Api.Services.Coordinations
{
    public class ExternalPersonWithPetsCoordinationService : IExternalPersonWithPetsCoordinationService
    {
        private readonly IExternalPersonOrchestrationService externalPersonOrchestrationService;
        private readonly IExternalPersonPetEventOrchestrationService externalPersonPetEventOrchestrationService;
        private readonly IPersonPetOrchestrationService personPetOrchestrationService;

        public ExternalPersonWithPetsCoordinationService(
            IExternalPersonOrchestrationService externalPersonOrchestrationService,
            IExternalPersonPetEventOrchestrationService externalPersonPetEventOrchestrationService,
            IPersonPetOrchestrationService personPetOrchestrationService)
        {
            this.externalPersonOrchestrationService = externalPersonOrchestrationService;
            this.externalPersonPetEventOrchestrationService = externalPersonPetEventOrchestrationService;
            this.personPetOrchestrationService = personPetOrchestrationService;
        }

        public async ValueTask<List<PersonPet>> CoordinateExternalPersonAsync()
        {
            await this.externalPersonOrchestrationService.RetrieveAndAddFormattedExternalPersonAsync();

            List<ExternalPerson> externalPersons =
                await this.externalPersonPetEventOrchestrationService.RetrieveExternalPerson();

            var personsWithPets = new List<PersonPet>();

            foreach (var externalPerson in externalPersons)
            {
                var personId = Guid.NewGuid();

                var person = new Person
                {
                    Id = personId,
                    Name = externalPerson.PersonName,
                    Age = externalPerson.Age
                };

                var pets = MapPets(externalPerson, personId);

                var personPet = new PersonPet
                {
                    Person = person,
                    Pets = pets
                };

                PersonPet processedPersonPet =
                    await this.personPetOrchestrationService.ProcessPersonWithPetsAsync(personPet);

                personsWithPets.Add(processedPersonPet);
            }

            return personsWithPets;
        }

        private List<Pet> MapPets(ExternalPerson externalPerson, Guid personId)
        {
            var pets = new List<Pet>();

            if (!string.IsNullOrWhiteSpace(externalPerson.PetOne))
            {
                pets.Add(new Pet
                {
                    Id = Guid.NewGuid(),
                    Name = externalPerson.PetOne,
                    Type = MapToPetType(externalPerson.PetOneType),
                    PersonId = personId
                });
            }

            if (!string.IsNullOrWhiteSpace(externalPerson.PetTwo))
            {
                pets.Add(new Pet
                {
                    Id = Guid.NewGuid(),
                    Name = externalPerson.PetTwo,
                    Type = MapToPetType(externalPerson.PetTwoType),
                    PersonId = personId
                });
            }

            if (!string.IsNullOrWhiteSpace(externalPerson.PetThree))
            {
                pets.Add(new Pet
                {
                    Id = Guid.NewGuid(),
                    Name = externalPerson.PetThree,
                    Type = MapToPetType(externalPerson.PetThreeType),
                    PersonId = personId
                });
            }

            return pets;
        }

        private PetType MapToPetType(string petType)
        {
            return Enum.TryParse(petType, ignoreCase: true, out PetType mappedPetType)
                ? mappedPetType
                : PetType.Other;
        }
    }
}
