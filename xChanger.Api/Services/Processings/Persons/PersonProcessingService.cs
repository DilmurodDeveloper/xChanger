//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.Persons;
using xChanger.Api.Services.Foundations.Persons;

namespace xChanger.Api.Services.Processings.Persons
{
    public class PersonProcessingService : IPersonProcessingService
    {
        private readonly IPersonService personService;

        public PersonProcessingService(IPersonService personService) =>
            this.personService = personService;

        public async ValueTask<Person> UpsertPersonAsync(Person person)
        {
            Person maybePerson = RetrievePerson(person);

            return maybePerson switch
            {
                null => await this.personService.AddPersonAsync(person),
                _ => await this.personService.ModifyPersonAsync(person)
            };
        }

        private Person RetrievePerson(Person person)
        {
            IQueryable<Person> retrievedPersons =
                this.personService.RetrieveAllPersons();

            return retrievedPersons.FirstOrDefault(storagePerson =>
                storagePerson.Id == person.Id);
        }

        public IQueryable<Person> RetrieveAllPerson() =>
            personService.RetrieveAllPersons();

        public IQueryable<Person> RetrieveAllPersonWithPets() =>
            personService.RetrieveAllPersonWithPets();
    }
}
