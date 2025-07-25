﻿//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Brokers.Loggings;
using xChanger.Api.Brokers.Storages;
using xChanger.Api.Models.Foundations.Pets;

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

        public ValueTask<Pet> ModifyPetAsync(Pet pet) =>
        TryCatch(async () =>
        {
            ValidatePetOnModify(pet);

            Pet maybePet =
                await this.storageBroker.SelectPetByIdAsync(pet.Id);

            ValidateAgainstStoragePetOnModify(pet, maybePet);

            return await storageBroker.UpdatePetAsync(pet);
        });
    }
}
