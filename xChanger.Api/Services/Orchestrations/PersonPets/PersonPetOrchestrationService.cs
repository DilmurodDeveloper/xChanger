//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.Persons;
using xChanger.Api.Models.Foundations.Pets;
using xChanger.Api.Models.Orchestrations;
using xChanger.Api.Services.Processings.Persons;
using xChanger.Api.Services.Processings.Pets;

namespace xChanger.Api.Services.Orchestrations.PersonPets
{
    public class PersonPetOrchestrationService : IPersonPetOrchestrationService
    {
        private readonly IPersonProcessingService personProcessingService;
        private readonly IPetProcessingService petProcessingService;

        public PersonPetOrchestrationService(
            IPersonProcessingService personProcessingService,
            IPetProcessingService petProcessingService)
        {
            this.personProcessingService = personProcessingService;
            this.petProcessingService = petProcessingService;
        }

        public async ValueTask<PersonPet> ProcessPersonWithPetsAsync(PersonPet personPet)
        {
            Person processedPerson =
                await this.personProcessingService.UpsertPersonAsync(personPet.Person);

            PersonPet processedPersonPet = MapToPersonPet(processedPerson);

            foreach (Pet pet in personPet.Pets)
            {
                Pet processedPet = await this.petProcessingService.UpsertPetAsync(pet);
                processedPersonPet.Pets.Add(processedPet);
            }

            return processedPersonPet;
        }

        private static PersonPet MapToPersonPet(Person processedPerson)
        {
            return new PersonPet
            {
                Person = processedPerson,
                Pets = new List<Pet>()
            };
        }
    }
}
