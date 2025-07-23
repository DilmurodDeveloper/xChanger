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
        public async Task ShouldModifyPersonAsync()
        {
            // given
            Person randomPerson = CreateRandomPerson();
            Person inputPerson = randomPerson;
            Person storagePerson = inputPerson.DeepClone();
            Person updatedPerson = inputPerson;
            Person expectedPerson = updatedPerson.DeepClone();
            Guid InputPersonId = inputPerson.Id;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectPersonByIdAsync(InputPersonId))
                    .ReturnsAsync(storagePerson);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdatePersonAsync(inputPerson))
                    .ReturnsAsync(updatedPerson);

            // when
            Person actualPerson =
                await this.personService
                    .ModifyPersonAsync(inputPerson);

            // then
            actualPerson.Should().BeEquivalentTo(expectedPerson);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPersonByIdAsync(InputPersonId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdatePersonAsync(inputPerson),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
