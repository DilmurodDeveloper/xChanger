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

        public IQueryable<Person> RetrieveAllPersons()
        {
            try
            {
                return this.storageBroker.SelectAllPersons();
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
            catch (Exception exception)
            {
                var failedPersonServiceException =
                    new FailedPersonServiceException(exception);

                var personServiceException =
                    new PersonServiceException(failedPersonServiceException);

                this.loggingBroker.LogError(personServiceException);

                throw personServiceException;
            }
        }
    }
}
