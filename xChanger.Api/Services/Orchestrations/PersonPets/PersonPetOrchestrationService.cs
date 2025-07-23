//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.ExternalPersons;
using xChanger.Api.Models.Foundations.Persons;
using xChanger.Api.Models.Foundations.Pets;
using xChanger.Api.Services.Foundations.Persons;
using xChanger.Api.Services.Foundations.Pets;

namespace xChanger.Api.Services.Orchestrations.PersonPets
{
    public class PersonPetOrchestrationService : IPersonPetOrchestrationService
    {
        private readonly IPersonService personService;
        private readonly IPetService petService;

        public PersonPetOrchestrationService(
            IPersonService personService,
            IPetService petService)
        {
            this.personService = personService;
            this.petService = petService;
        }

        public async ValueTask ProcessExternalPersonAsync(ExternalPerson externalPerson)
        {
            Person person = MapToPerson(externalPerson);
            Person addedPerson = await this.personService.AddPersonAsync(person);

            IEnumerable<Pet> pets = MapToPets(externalPerson, addedPerson.Id);

            foreach (Pet pet in pets)
            {
                await this.petService.AddPetAsync(pet);
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
