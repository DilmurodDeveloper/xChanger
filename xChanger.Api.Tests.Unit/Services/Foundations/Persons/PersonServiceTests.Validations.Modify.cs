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
        public async Task ShouldThrowValidationExceptionOnModifyIfPersonIsNullAndLogItAsync()
        {
            // given
            Person nullPerson = null;
            var nullPersonException = new NullPersonException();

            var expectedPersonValidationException =
                new PersonValidationException(nullPersonException);

            // when
            ValueTask<Person> modifyPersonTask =
                this.personService.ModifyPersonAsync(nullPerson);

            PersonValidationException actualPersonValidationException =
                await Assert.ThrowsAsync<PersonValidationException>(() =>
                    modifyPersonTask.AsTask());

            // then
            actualPersonValidationException.Should()
                .BeEquivalentTo(expectedPersonValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPersonValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdatePersonAsync(It.IsAny<Person>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
