//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Brokers.Loggings;
using xChanger.Api.Brokers.Storages;
using xChanger.Api.Models.Foundations.Persons;

namespace xChanger.Api.Services.Foundations.Persons
{
    public partial class PersonService : IPersonService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public PersonService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Person> AddPersonAsync(Person person) =>
        TryCatch(async () =>
        {
            ValidatePersonOnAdd(person);

            return await this.storageBroker.InsertPersonAsync(person);
        });

        public IQueryable<Person> RetrieveAllPersons() =>
            TryCatch(() => this.storageBroker.SelectAllPersons());

        public IQueryable<Person> RetrieveAllPersonWithPets() =>
            TryCatch(() => this.storageBroker.SelectAllPersonsWithPets());

        public ValueTask<Person> RetrievePersonByIdAsync(Guid personId) =>
        TryCatch(async () =>
        {
            ValidatePersonId(personId);

            Person maybePerson =
                await this.storageBroker.SelectPersonByIdAsync(personId);

            ValidateStoragePerson(maybePerson, personId);

            return maybePerson;
        });

        public ValueTask<Person> ModifyPersonAsync(Person person) =>
        TryCatch(async () =>
        {
            ValidatePersonOnModify(person);

            Person maybePerson =
            await this.storageBroker.SelectPersonByIdAsync(person.Id);

            ValidateAgainstStoragePersonOnModify(person, maybePerson);

            return await storageBroker.UpdatePersonAsync(person);
        });
    }
}
