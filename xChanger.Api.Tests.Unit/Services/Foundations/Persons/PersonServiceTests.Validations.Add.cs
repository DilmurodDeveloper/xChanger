//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using Moq;
using xChanger.Api.Models.Foundations.Persons;
using xChanger.Api.Models.Foundations.Persons.Exceptions;

namespace xChanger.Api.Tests.Unit.Services.Foundations.Persons
{
    public partial class PersonServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfPersonIsNullAndLogItAsync()
        {
            // given
            Person nullPerson = null;
            var nullPersonException = new NullPersonException();

            var expectedPersonValidationException =
                new PersonValidationException(nullPersonException);

            // when
            ValueTask<Person> addPersonTask =
                this.personService.AddPersonAsync(nullPerson);

            // then
            await Assert.ThrowsAsync<PersonValidationException>(() =>
                addPersonTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPersonValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPersonAsync(It.IsAny<Person>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfPersonIsInvalidAndLogItAsync(
            string invalidText)
        {
            // given
            var invalidPerson = new Person
            {
                Name = invalidText
            };

            var invalidPersonException = new InvalidPersonException();

            invalidPersonException.AddData(
                key: nameof(Person.Id),
                values: "Id is required");

            invalidPersonException.AddData(
                key: nameof(Person.Name),
                values: "Text is required");

            invalidPersonException.AddData(
                key: nameof(Person.Age),
                values: "Value must be greater than 0");

            var expectedPersonValidationException =
                new PersonValidationException(invalidPersonException);

            // when
            ValueTask<Person> addPersonTask =
                this.personService.AddPersonAsync(invalidPerson);

            // then
            await Assert.ThrowsAsync<PersonValidationException>(() =>
                addPersonTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPersonValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPersonAsync(It.IsAny<Person>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
