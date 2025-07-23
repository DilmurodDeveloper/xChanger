//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.ExternalPersons;
using xChanger.Api.Services.Processings.ExternalPersons;

namespace xChanger.Api.Services.Orchestrations.ExternalPersons
{
    public class ExternalPersonPetEventOrchestrationService : IExternalPersonPetEventOrchestrationService
    {
        private readonly IExternalPersonEventProcessingService externalPersonEventProcessingService;

        public ExternalPersonPetEventOrchestrationService(
            IExternalPersonEventProcessingService externalPersonEventProcessingService) =>
                this.externalPersonEventProcessingService = externalPersonEventProcessingService;

        public ValueTask<List<ExternalPerson>> RetrieveExternalPerson() =>
            this.externalPersonEventProcessingService.RetrieveExternalPerson();
    }
}
