//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Brokers.Loggings;
using xChanger.Api.Brokers.Storages;
using xChanger.Api.Models.Foundations.Persons;
using xChanger.Api.Models.Foundations.Persons.Exceptions;

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

        public IQueryable<Person> RetrieveAllPeopleWithPets() =>
            TryCatch(() => this.storageBroker.SelectAllPersonsWithPets());

        public IQueryable<Person> RetrieveAllPersonWithPets()
        {
            throw new NotImplementedException();
        }

        public async ValueTask<Person> RetrievePersonByIdAsync(Guid personId)
        {
            try
            {
                ValidatePersonId(personId);

                return await this.storageBroker.SelectPersonByIdAsync(personId);
            }
            catch (InvalidPersonException invalidPersonException)
            {
                var personValidationException =
                    new PersonValidationException(invalidPersonException);

                this.loggingBroker.LogError(personValidationException);

                throw personValidationException;
            }
        }
    }
}
