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

        public async ValueTask<Person> AddPersonAsync(Person person)
        {
            try
            {
                ValidatePersonNotNull(person);

                return await this.storageBroker.InsertPersonAsync(person);
            }
            catch (NullPersonException nullPersonException)
            {
                var personValidationException =
                    new PersonValidationException(nullPersonException);

                this.loggingBroker.LogError(personValidationException);

                throw personValidationException;
            }
        }
    }
}
