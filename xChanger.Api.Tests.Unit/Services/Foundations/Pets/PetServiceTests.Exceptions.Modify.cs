//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using xChanger.Api.Models.Foundations.Pets;
using xChanger.Api.Models.Foundations.Pets.Exceptions;

namespace xChanger.Api.Tests.Unit.Services.Foundations.Pets
{
    public partial class PetServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Pet randomPet = CreateRandomPet();
            Pet somePet = randomPet;
            Guid petId = somePet.Id;
            SqlException sqlException = GetSqlError();

            var failedPetStorageException =
                new FailedPetStorageException(sqlException);

            var expectedPetDependencyException =
                new PetDependencyException(failedPetStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectPetByIdAsync(petId))
                    .Throws(sqlException);

            // when
            ValueTask<Pet> modifyPetTask =
                this.petService.ModifyPetAsync(somePet);

            PetDependencyException actualPetDependencyException =
                await Assert.ThrowsAsync<PetDependencyException>(() =>
                    modifyPetTask.AsTask());

            // then
            actualPetDependencyException.Should()
                .BeEquivalentTo(expectedPetDependencyException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedPetDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPetByIdAsync(petId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdatePetAsync(somePet),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnModifyIfDatabaseUpdateExceptionOccursAndLogItAsync()
        {
            // given
            Pet randomPet = CreateRandomPet();
            Pet somePet = randomPet;
            Guid petId = somePet.Id;
            var databaseUpdateException = new DbUpdateException();

            var failedPetStorageException =
                new FailedPetStorageException(databaseUpdateException);

            var expectedPetDependencyException =
                new PetDependencyException(failedPetStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectPetByIdAsync(petId))
                    .Throws(databaseUpdateException);

            // when
            ValueTask<Pet> modifyPetTask =
                this.petService.ModifyPetAsync(somePet);

            PetDependencyException actualPetDependencyException =
                await Assert.ThrowsAsync<PetDependencyException>(() =>
                    modifyPetTask.AsTask());

            // then
            actualPetDependencyException.Should()
                .BeEquivalentTo(expectedPetDependencyException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPetDependencyException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPetByIdAsync(petId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdatePetAsync(somePet),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
