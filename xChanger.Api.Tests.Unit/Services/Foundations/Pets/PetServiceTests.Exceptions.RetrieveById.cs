//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using xChanger.Api.Models.Foundations.Pets;
using xChanger.Api.Models.Foundations.Pets.Exceptions;

namespace xChanger.Api.Tests.Unit.Services.Foundations.Pets
{
    public partial class PetServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveByIdIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Guid someId = Guid.NewGuid();
            SqlException sqlException = GetSqlError();

            var failedPetStorageException =
                new FailedPetStorageException(sqlException);

            var expectedPetDependencyException =
                new PetDependencyException(failedPetStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectPetByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<Pet> retrievePetById =
                this.petService.RetrievePetByIdAsync(someId);

            PetDependencyException actualPetDependencyException =
                await Assert.ThrowsAsync<PetDependencyException>(() =>
                    retrievePetById.AsTask());

            // then
            actualPetDependencyException.Should()
                .BeEquivalentTo(expectedPetDependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPetByIdAsync(someId),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedPetDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveByIdAsyncIfServiceErrorOccursAndLogItAsync()
        {
            // given
            Guid someId = Guid.NewGuid();
            var serverException = new Exception();

            var failedPetServiceException =
                new FailedPetServiceException(serverException);

            var expectedPetServiceException =
                new PetServiceException(failedPetServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectPetByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(serverException);

            // when
            ValueTask<Pet> retrievePetById =
                this.petService.RetrievePetByIdAsync(someId);

            PetServiceException actualPetServiceException =
                await Assert.ThrowsAsync<PetServiceException>(() =>
                    retrievePetById.AsTask());

            // then
            actualPetServiceException.Should()
                .BeEquivalentTo(expectedPetServiceException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPetByIdAsync(someId),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPetServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
