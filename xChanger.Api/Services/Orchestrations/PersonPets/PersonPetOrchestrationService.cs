//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.ExternalPersons;
using xChanger.Api.Models.Foundations.Persons;
using xChanger.Api.Models.Foundations.Pets;
using xChanger.Api.Services.Foundations.Persons;
using xChanger.Api.Services.Foundations.Pets;
using xChanger.Api.Services.Processings.ExternalPersons;
using xChanger.Api.Services.Processings.Persons;
using xChanger.Api.Services.Processings.Pets;

namespace xChanger.Api.Services.Orchestrations.PersonPets
{
    public class PersonPetOrchestrationService : IPersonPetOrchestrationService
    {
        private readonly IPersonService personService;
        private readonly IPetService petService;
        private readonly IExternalPersonProcessingService externalPersonProcessingService;
        private readonly IPersonProcessingService personProcessingService;
        private readonly IPetProcessingService petProcessingService;

        public PersonPetOrchestrationService(
            IPersonService personService,
            IPetService petService,
            IExternalPersonProcessingService externalPersonProcessingService,
            IPersonProcessingService personProcessingService,
            IPetProcessingService petProcessingService)
        {
            this.personService = personService;
            this.petService = petService;
            this.externalPersonProcessingService = externalPersonProcessingService;
            this.personProcessingService = personProcessingService;
            this.petProcessingService = petProcessingService;
        }


        public async ValueTask ProcessExternalPersonAsync(ExternalPerson externalPerson)
        {
            ExternalPerson cleanedExternalPerson =
                this.externalPersonProcessingService.ProcessExternalPerson(externalPerson);

            Person person = MapToPerson(cleanedExternalPerson);
            person = this.personProcessingService.ProcessPerson(person);
            Person addedPerson = await this.personService.AddPersonAsync(person);
            IEnumerable<Pet> pets = MapToPets(cleanedExternalPerson, addedPerson.Id);

            foreach (Pet pet in pets)
            {
                Pet cleanedPet = this.petProcessingService.ProcessPet(pet);
                await this.petService.AddPetAsync(cleanedPet);
            }
        }

        private Person MapToPerson(ExternalPerson externalPerson) =>
            new Person
            {
                Id = Guid.NewGuid(),
                Name = externalPerson.PersonName,
                Age = externalPerson.Age
            };

        private IEnumerable<Pet> MapToPets(ExternalPerson externalPerson, Guid personId)
        {
            var pets = new List<Pet>();

            if (!string.IsNullOrWhiteSpace(externalPerson.PetOne))
                pets.Add(new Pet
                {
                    Id = Guid.NewGuid(),
                    Name = externalPerson.PetOne,
                    Type = ConvertToPetType(externalPerson.PetOneType),
                    PersonId = personId
                });

            if (!string.IsNullOrWhiteSpace(externalPerson.PetTwo))
                pets.Add(new Pet
                {
                    Id = Guid.NewGuid(),
                    Name = externalPerson.PetTwo,
                    Type = ConvertToPetType(externalPerson.PetTwoType),
                    PersonId = personId
                });

            if (!string.IsNullOrWhiteSpace(externalPerson.PetThree))
                pets.Add(new Pet
                {
                    Id = Guid.NewGuid(),
                    Name = externalPerson.PetThree,
                    Type = ConvertToPetType(externalPerson.PetThreeType),
                    PersonId = personId
                });

            return pets;
        }

        private PetType ConvertToPetType(string petTypeText)
        {
            return petTypeText?.ToLower() switch
            {
                "cat" => PetType.Cat,
                "dog" => PetType.Dog,
                "parrot" => PetType.Parrot,
                _ => PetType.Other
            };
        }
    }
}
