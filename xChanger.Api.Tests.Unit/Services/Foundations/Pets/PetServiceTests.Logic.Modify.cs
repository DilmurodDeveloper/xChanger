//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using xChanger.Api.Models.Foundations.Pets;

namespace xChanger.Api.Tests.Unit.Services.Foundations.Pets
{
    public partial class PetServiceTests
    {
        [Fact]
        public async Task ShouldModifyPetAsync()
        {
            // given
            Pet randomPet = CreateRandomPet();
            Pet inputPet = randomPet;
            Pet persistedPet = inputPet.DeepClone();
            Pet updatedPet = inputPet;
            Pet expectedPet = updatedPet.DeepClone();
            Guid InputPetId = inputPet.Id;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectPetByIdAsync(InputPetId))
                    .ReturnsAsync(persistedPet);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdatePetAsync(inputPet))
                    .ReturnsAsync(updatedPet);

            // when
            Pet actualPet =
                await this.petService.ModifyPetAsync(inputPet);

            // then
            actualPet.Should().BeEquivalentTo(expectedPet);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPetByIdAsync(InputPetId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdatePetAsync(inputPet),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
