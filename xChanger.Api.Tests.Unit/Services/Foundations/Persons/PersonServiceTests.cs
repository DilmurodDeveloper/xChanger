//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using Moq;
using Tynamix.ObjectFiller;
using xChanger.Api.Brokers.Loggings;
using xChanger.Api.Brokers.Storages;
using xChanger.Api.Models.Foundations.Persons;
using xChanger.Api.Services.Foundations.Persons;

namespace xChanger.Api.Tests.Unit.Services.Foundations.Persons
{
    public partial class PersonServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IPersonService personService;

        public PersonServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            this.personService = new PersonService(
                storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static Person CreateRandomPerson() =>
            CreatePersonFiller().Create();

        private static int GetRandomNumber() =>
           new IntRange(2, 10).GetValue();

        private static Filler<Person> CreatePersonFiller()
        {
            var filler = new Filler<Person>();
            int positiveNumber = GetRandomNumber();

            filler.Setup()
                .OnType<int>().Use(positiveNumber);

            return filler;
        }
    }
}
