//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using FluentAssertions;
using Moq;
using xChanger.Api.Models.Foundations.Persons;
using xChanger.Api.Models.Foundations.Persons.Exceptions;

namespace xChanger.Api.Tests.Unit.Services.Foundations.Persons
{
    public partial class PersonServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidPersonId = Guid.Empty;
            var invalidPersonException = new InvalidPersonException();

            invalidPersonException.AddData(
                key: nameof(Person.Id),
                values: "Id is required");

            var expectedPersonValidationException =
                new PersonValidationException(invalidPersonException);

            // when
            ValueTask<Person> retrievePersonById =
                this.personService.RetrievePersonByIdAsync(invalidPersonId);

            PersonValidationException actualPersonValidationException =
                await Assert.ThrowsAsync<PersonValidationException>(() =>
                    retrievePersonById.AsTask());

            // then
            actualPersonValidationException.Should()
                .BeEquivalentTo(expectedPersonValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPersonValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPersonByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
