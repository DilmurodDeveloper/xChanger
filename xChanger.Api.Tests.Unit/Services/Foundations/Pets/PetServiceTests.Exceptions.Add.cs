//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
using xChanger.Api.Models.Foundations.Pets;
using xChanger.Api.Models.Foundations.Pets.Exceptions;

namespace xChanger.Api.Tests.Unit.Services.Foundations.Pets
{
    public partial class PetServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Pet somePet = CreateRandomPet();
            SqlException sqlException = GetSqlError();

            var failedPetStorageException =
                new FailedPetStorageException(sqlException);

            var expectedPetDependencyException =
                new PetDependencyException(failedPetStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertPetAsync(somePet))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<Pet> addPetTask =
                this.petService.AddPetAsync(somePet);

            // then
            await Assert.ThrowsAsync<PetDependencyException>(() =>
                addPetTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPetAsync(somePet),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedPetDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationOnAddIfDuplicateKeyErrorOccursAndLogItAsync()
        {
            // given
            Pet somePet = CreateRandomPet();
            string someMessage = GetRandomString();
            var duplicateKeyException = new DuplicateKeyException(someMessage);

            var alreadyExistsPetException =
                new AlreadyExistsPetException(duplicateKeyException);

            var expectedPetDependencyValidationException =
                new PetDependencyValidationException(alreadyExistsPetException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertPetAsync(somePet))
                    .ThrowsAsync(duplicateKeyException);

            // when
            ValueTask<Pet> addPetTask =
                this.petService.AddPetAsync(somePet);

            // then
            await Assert.ThrowsAsync<PetDependencyValidationException>(() =>
                addPetTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPetAsync(somePet),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPetDependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
