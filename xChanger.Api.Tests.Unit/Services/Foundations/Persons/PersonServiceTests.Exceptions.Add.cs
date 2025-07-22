//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
using xChanger.Api.Models.Foundations.Persons;
using xChanger.Api.Models.Foundations.Persons.Exceptions;

namespace xChanger.Api.Tests.Unit.Services.Foundations.Persons
{
    public partial class PersonServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Person somePerson = CreateRandomPerson();
            SqlException sqlException = GetSqlError();

            var failedPersonStorageException =
                new FailedPersonStorageException(sqlException);

            var expectedPersonDependencyException =
                new PersonDependencyException(failedPersonStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertPersonAsync(somePerson))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<Person> addPersonTask =
                this.personService.AddPersonAsync(somePerson);

            // then
            await Assert.ThrowsAsync<PersonDependencyException>(() =>
                addPersonTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPersonAsync(somePerson),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedPersonDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationOnAddIfDuplicateKeyErrorOccursAndLogItAsync()
        {
            // given
            Person somePerson = CreateRandomPerson();
            string someMessage = GetRandomString();

            var duplicateKeyException =
                new DuplicateKeyException(someMessage);

            var alreadyExistsPersonException =
                new AlreadyExistsPersonException(duplicateKeyException);

            var expectedPersonDependencyValidationException =
                new PersonDependencyValidationException(alreadyExistsPersonException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertPersonAsync(somePerson))
                    .ThrowsAsync(duplicateKeyException);

            // when
            ValueTask<Person> addPersonTask =
                this.personService.AddPersonAsync(somePerson);

            // then
            await Assert.ThrowsAsync<PersonDependencyValidationException>(() =>
                addPersonTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPersonAsync(somePerson),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPersonDependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
