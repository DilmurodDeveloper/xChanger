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
        public async Task ShouldAddPetAsync()
        {
            // given
            Pet randomPet = CreateRandomPet();
            Pet inputPet = randomPet;
            Pet storagePet = inputPet;
            Pet expectedPet = storagePet.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertPetAsync(inputPet))
                    .ReturnsAsync(storagePet);

            // when
            Pet actualPet =
                await this.petService.AddPetAsync(inputPet);

            // then
            actualPet.Should().BeEquivalentTo(expectedPet);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPetAsync(inputPet),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
