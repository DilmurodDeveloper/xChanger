//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using xChanger.Api.Models.Foundations.Persons.Exceptions;

namespace xChanger.Api.Tests.Unit.Services.Foundations.Persons
{
    public partial class PersonServiceTests
    {
        [Fact]
        public void ShouldThrowCriticalDependencyExceptionOnRetrieveAllWhenSqlExceptionOccursAndLogIt()
        {
            // given
            SqlException sqlException = GetSqlError();

            var failedPersonStorageException =
                new FailedPersonStorageException(sqlException);

            var expectedPersonDependencyException =
                new PersonDependencyException(failedPersonStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllPersons()).Throws(sqlException);

            // when
            Action retrieveAllPersonAction = () =>
                this.personService.RetrieveAllPersons();

            PersonDependencyException actualPersonDependencyException =
                Assert.Throws<PersonDependencyException>(retrieveAllPersonAction);

            // then
            actualPersonDependencyException.Should()
                .BeEquivalentTo(expectedPersonDependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllPersons(),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedPersonDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string exceptionMessage = GetRandomString();
            var serverException = new Exception(exceptionMessage);

            var failedPersonServiceException =
                new FailedPersonServiceException(serverException);

            var expectedPersonServiceException =
                new PersonServiceException(failedPersonServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllPersons()).
                    Throws(serverException);

            // when
            Action retrieveAllPersonActions = () =>
                this.personService.RetrieveAllPersons();

            PersonServiceException actualPersonServiceException =
                Assert.Throws<PersonServiceException>(retrieveAllPersonActions);

            //then
            actualPersonServiceException.Should()
                .BeEquivalentTo(expectedPersonServiceException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllPersons(),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPersonServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
