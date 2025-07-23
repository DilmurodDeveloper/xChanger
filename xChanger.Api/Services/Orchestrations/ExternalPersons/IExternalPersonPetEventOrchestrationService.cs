//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.ExternalPersons;

namespace xChanger.Api.Services.Orchestrations.ExternalPersons
{
    public interface IExternalPersonPetEventOrchestrationService
    {
        ValueTask<List<ExternalPerson>> RetrieveExternalPerson();
    }
}
