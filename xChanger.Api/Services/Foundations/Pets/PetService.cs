//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using Microsoft.Data.SqlClient;
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

        public async ValueTask<Pet> RetrievePetByIdAsync(Guid petId)
        {
            try
            {
                ValidatePetId(petId);

                Pet maybePet =
                    await this.storageBroker.SelectPetByIdAsync(petId);

                ValidateStoragePet(maybePet, petId);

                return maybePet;
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
            catch (SqlException sqlException)
            {
                var failedPetStorageException =
                    new FailedPetStorageException(sqlException);

                var petDependencyException =
                    new PetDependencyException(failedPetStorageException);

                this.loggingBroker.LogCritical(petDependencyException);

                throw petDependencyException;
            }
            catch (Exception exception)
            {
                var failedPetServiceException =
                    new FailedPetServiceException(exception);

                var petServiceException =
                    new PetServiceException(failedPetServiceException);

                this.loggingBroker.LogError(petServiceException);

                throw petServiceException;
            }
        }
    }
}
