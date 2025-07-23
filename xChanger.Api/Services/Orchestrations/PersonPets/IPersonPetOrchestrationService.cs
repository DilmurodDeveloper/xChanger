//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.ExternalPersons;

namespace xChanger.Api.Services.Orchestrations.PersonPets
{
    public interface IPersonPetOrchestrationService
    {
        ValueTask ProcessExternalPersonAsync(ExternalPerson externalPerson);
    }

}
