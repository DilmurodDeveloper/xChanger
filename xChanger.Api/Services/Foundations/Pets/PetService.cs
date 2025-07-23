//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Brokers.Loggings;
using xChanger.Api.Brokers.Storages;
using xChanger.Api.Models.Foundations.Pets;
using xChanger.Api.Models.Foundations.Pets.Exceptions;

namespace xChanger.Api.Services.Foundations.Pets
{
    public partial class PetService : IPetService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public PetService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Pet> AddPetAsync(Pet pet) =>
        TryCatch(async () =>
        {
            ValidatePetOnAdd(pet);

            return await this.storageBroker.InsertPetAsync(pet);
        });

        public IQueryable<Pet> RetrieveAllPets() =>
            TryCatch(() => this.storageBroker.SelectAllPets());

        public ValueTask<Pet> RetrievePetByIdAsync(Guid petId) =>
        TryCatch(async () =>
        {
            ValidatePetId(petId);

            Pet maybePet =
                await this.storageBroker.SelectPetByIdAsync(petId);

            ValidateStoragePet(maybePet, petId);

            return maybePet;
        });

        public async ValueTask<Pet> ModifyPetAsync(Pet pet)
        {
            try
            {
                ValidatePetOnModify(pet);

                Pet maybePet =
                    await this.storageBroker.SelectPetByIdAsync(pet.Id);

                ValidateAgainstStoragePetOnModify(pet, maybePet);

                return await storageBroker.UpdatePetAsync(pet);
            }
            catch (NullPetException nullPetException)
            {
                var petValidationException =
                    new PetValidationException(nullPetException);

                this.loggingBroker.LogError(petValidationException);

                throw petValidationException;
            }
            catch (InvalidPetException invalidPetException)
            {
                var petValidationException =
                    new PetValidationException(invalidPetException);

                this.loggingBroker.LogError(petValidationException);

                throw petValidationException;
            }
            catch (NotFoundPetException notFoundPetException)
            {
                var petValidationException =
                    new PetValidationException(notFoundPetException);

                this.loggingBroker.LogError(petValidationException);

                throw petValidationException;
            }
        }
    }
}
