//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using FluentAssertions;
using Moq;
using xChanger.Api.Models.Foundations.Persons;

namespace xChanger.Api.Tests.Unit.Services.Foundations.Persons
{
    public partial class PersonServiceTests
    {
        [Fact]
        public async Task ShouldAddPersonAsync()
        {
            // given
            Person randomPerson = CreateRandomPerson();
            Person inputPerson = randomPerson;
            Person storagePerson = inputPerson;
            Person expectedPerson = storagePerson;

            this.storageBrokerMock.Setup(broker =>
                broker.InsertPersonAsync(inputPerson))
                    .ReturnsAsync(storagePerson);

            // when
            Person actualPerson =
                await this.personService.AddPersonAsync(inputPerson);

            // then
            actualPerson.Should().BeEquivalentTo(expectedPerson);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPersonAsync(inputPerson),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
