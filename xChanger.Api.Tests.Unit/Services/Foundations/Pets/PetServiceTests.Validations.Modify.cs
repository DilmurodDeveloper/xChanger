//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using FluentAssertions;
using Moq;
using xChanger.Api.Models.Foundations.Pets;
using xChanger.Api.Models.Foundations.Pets.Exceptions;

namespace xChanger.Api.Tests.Unit.Services.Foundations.Pets
{
    public partial class PetServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfPetIsNullAndLogItAsync()
        {
            // given
            Pet nullPet = null;
            var nullPetException = new NullPetException();

            var expectedPetValidationException =
                new PetValidationException(nullPetException);

            // when
            ValueTask<Pet> modifyPetTask =
                this.petService.ModifyPetAsync(nullPet);

            PetValidationException actualPetValidationException =
                await Assert.ThrowsAsync<PetValidationException>(
                    modifyPetTask.AsTask);

            // then
            actualPetValidationException.Should()
                .BeEquivalentTo(expectedPetValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPetValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdatePetAsync(It.IsAny<Pet>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnModifyIfPetIsInvalidAndLogItAsync(
            string invalidText)
        {
            // given
            Pet invalidPet = new Pet
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
            ValueTask<Pet> modifyPetTask =
                this.petService.ModifyPetAsync(invalidPet);

            PetValidationException actualPetValidationException =
                await Assert.ThrowsAsync<PetValidationException>(
                    modifyPetTask.AsTask);

            // then
            actualPetValidationException.Should()
                .BeEquivalentTo(expectedPetValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPetValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdatePetAsync(It.IsAny<Pet>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
