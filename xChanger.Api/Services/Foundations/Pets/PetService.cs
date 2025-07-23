//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using EFxceptions.Models.Exceptions;
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

        public async ValueTask<Pet> AddPetAsync(Pet pet)
        {
            try
            {
                ValidatePetOnAdd(pet);

                return await this.storageBroker.InsertPetAsync(pet);
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
            catch (SqlException sqlException)
            {
                var failedPetStorageException =
                    new FailedPetStorageException(sqlException);

                var petDependencyException =
                    new PetDependencyException(failedPetStorageException);

                this.loggingBroker.LogCritical(petDependencyException);

                throw petDependencyException;
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsPetException =
                    new AlreadyExistsPetException(duplicateKeyException);

                var petDependencyValidationException =
                    new PetDependencyValidationException(alreadyExistsPetException);

                this.loggingBroker.LogError(petDependencyValidationException);

                throw petDependencyValidationException;
            }
        }
    }
}
