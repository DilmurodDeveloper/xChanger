//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using Moq;
using xChanger.Api.Models.Foundations.Pets;
using xChanger.Api.Models.Foundations.Pets.Exceptions;

namespace xChanger.Api.Tests.Unit.Services.Foundations.Pets
{
    public partial class PetServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfPetIsNullAndLogItAsync()
        {
            // given
            Pet nullPet = null;
            var nullPetException = new NullPetException();

            var expectedPetValidationException =
                new PetValidationException(nullPetException);

            // when
            ValueTask<Pet> addPetTask =
                this.petService.AddPetAsync(nullPet);

            // then
            await Assert.ThrowsAsync<PetValidationException>(() =>
                addPetTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPetValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPetAsync(It.IsAny<Pet>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfPetIsInvalidAndLogItAsync(
            string invalidText)
        {
            // given
            var invalidPet = new Pet
            {
                Name = invalidText
            };

            var invalidPetException = new InvalidPetException();

            invalidPetException.AddData(
                key: nameof(Pet.Id),
                values: "Id is required");

            invalidPetException.AddData(
                key: nameof(Pet.Name),
                values: "Text is required");

            invalidPetException.AddData(
                key: nameof(Pet.PersonId),
                values: "Id is required");

            var expectedPetValidationException =
                new PetValidationException(invalidPetException);

            // when
            ValueTask<Pet> addPetTask =
                this.petService.AddPetAsync(invalidPet);

            // then
            await Assert.ThrowsAsync<PetValidationException>(() =>
                addPetTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPetValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPetAsync(It.IsAny<Pet>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
