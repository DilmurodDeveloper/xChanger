//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using xChanger.Api.Models.Foundations.Persons;
using xChanger.Api.Models.Foundations.Persons.Exceptions;

namespace xChanger.Api.Tests.Unit.Services.Foundations.Persons
{
    public partial class PersonServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Person randomPerson = CreateRandomPerson();
            Person somePerson = randomPerson;
            Guid personId = somePerson.Id;
            SqlException sqlException = GetSqlError();

            var failedPersonStorageException =
                new FailedPersonStorageException(sqlException);

            var expectedPersonDependencyException =
                new PersonDependencyException(failedPersonStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectPersonByIdAsync(personId))
                    .Throws(sqlException);

            // when
            ValueTask<Person> modifyPersonTask =
                this.personService.ModifyPersonAsync(somePerson);

            PersonDependencyException actualPersonDependencyException =
                await Assert.ThrowsAsync<PersonDependencyException>(() =>
                    modifyPersonTask.AsTask());

            // then
            actualPersonDependencyException.Should()
                .BeEquivalentTo(expectedPersonDependencyException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedPersonDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPersonByIdAsync(personId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdatePersonAsync(somePerson),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnModifyIfDatabaseUpdateExceptionOccursAndLogItAsync()
        {
            // given
            Person randomPerson = CreateRandomPerson();
            Person somePerson = randomPerson;
            Guid personId = somePerson.Id;
            var databaseUpdateException = new DbUpdateException();

            var failedPersonStorageException =
                new FailedPersonStorageException(databaseUpdateException);

            var expectedPersonDependencyException =
                new PersonDependencyException(failedPersonStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectPersonByIdAsync(personId))
                    .Throws(databaseUpdateException);

            // when
            ValueTask<Person> modifyPersonTask =
                this.personService.ModifyPersonAsync(somePerson);

            PersonDependencyException actualPersonDependencyException =
                await Assert.ThrowsAsync<PersonDependencyException>(() =>
                    modifyPersonTask.AsTask());

            // then
            actualPersonDependencyException.Should()
                .BeEquivalentTo(expectedPersonDependencyException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPersonDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPersonByIdAsync(personId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdatePersonAsync(somePerson),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
