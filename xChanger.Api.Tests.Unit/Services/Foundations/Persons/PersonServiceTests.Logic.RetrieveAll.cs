//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using xChanger.Api.Models.Foundations.Persons;

namespace xChanger.Api.Tests.Unit.Services.Foundations.Persons
{
    public partial class PersonServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllPerson()
        {
            // given
            IQueryable<Person> randomPerson = CreateRandomPersons();
            IQueryable<Person> storagePerson = randomPerson;
            IQueryable<Person> expectedPerson = storagePerson.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllPersons())
                    .Returns(storagePerson);

            // when
            IQueryable<Person> actualPerson =
                this.personService.RetrieveAllPersons();

            // then
            actualPerson.Should().BeEquivalentTo(expectedPerson);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllPersons(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
