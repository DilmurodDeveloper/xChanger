﻿//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;
using Moq;
using Tynamix.ObjectFiller;
using xChanger.Api.Brokers.Loggings;
using xChanger.Api.Brokers.Storages;
using xChanger.Api.Models.Foundations.Persons;
using xChanger.Api.Services.Foundations.Persons;
using Xeptions;

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

        private IQueryable<Person> CreateRandomPersons()
        {
            return CreatePersonFiller()
                .Create(count: GetRandomNumber()).AsQueryable();
        }

        private static int GetRandomNumber() =>
           new IntRange(2, 10).GetValue();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static SqlException GetSqlError() =>
            (SqlException)RuntimeHelpers.GetUninitializedObject(typeof(SqlException));

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

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
