//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using Microsoft.Data.SqlClient;
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

        public ValueTask<Person> RetrievePersonByIdAsync(Guid personId) =>
        TryCatch(async () =>
        {
            ValidatePersonId(personId);

            Person maybePerson =
                await this.storageBroker.SelectPersonByIdAsync(personId);

            ValidateStoragePerson(maybePerson, personId);

            return maybePerson;
        });

        public async ValueTask<Person> ModifyPersonAsync(Person person)
        {
            try
            {
                ValidatePersonOnModify(person);

                Person maybePerson =
                await this.storageBroker.SelectPersonByIdAsync(person.Id);

                ValidateAgainstStoragePersonOnModify(person, maybePerson);

                return await storageBroker.UpdatePersonAsync(person);
            }
            catch (NullPersonException nullPersonException)
            {
                var personValidationException =
                    new PersonValidationException(nullPersonException);

                this.loggingBroker.LogError(personValidationException);

                throw personValidationException;
            }
            catch (InvalidPersonException invalidPersonException)
            {
                var personValidationException =
                    new PersonValidationException(invalidPersonException);

                this.loggingBroker.LogError(personValidationException);

                throw personValidationException;
            }
            catch (NotFoundPersonException notFoundPersonException)
            {
                var personValidationException =
                    new PersonValidationException(notFoundPersonException);

                this.loggingBroker.LogError(personValidationException);

                throw personValidationException;
            }
            catch (SqlException sqlException)
            {
                var failedPersonStorageException =
                    new FailedPersonStorageException(sqlException);

                var personDependencyException =
                    new PersonDependencyException(failedPersonStorageException);

                this.loggingBroker.LogCritical(personDependencyException);

                throw personDependencyException;
            }
        }
    }
}
