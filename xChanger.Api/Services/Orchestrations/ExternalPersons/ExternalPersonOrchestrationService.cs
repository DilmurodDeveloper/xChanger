//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.ExternalPersons;
using xChanger.Api.Services.Processings.ExternalPersons;

namespace xChanger.Api.Services.Orchestrations.ExternalPersons
{
    public class ExternalPersonOrchestrationService : IExternalPersonOrchestrationService
    {
        private readonly IExternalPersonProcessingService externalPersonProcessingService;
        private readonly IExternalPersonEventProcessingService externalPersonEventProcessingService;

        public ExternalPersonOrchestrationService(
            IExternalPersonProcessingService externalPersonProcessingService,
            IExternalPersonEventProcessingService externalPersonEventProcessingService)
        {
            this.externalPersonProcessingService = externalPersonProcessingService;
            this.externalPersonEventProcessingService = externalPersonEventProcessingService;
        }

        public async ValueTask RetrieveAndAddFormattedExternalPersonAsync()
        {
            List<ExternalPerson> formattedExternalPersonPets =
                await this.externalPersonProcessingService
                    .RetrieveFormattedExternalPersonAsync();

            await this.externalPersonEventProcessingService
                .AddExternalPerson(formattedExternalPersonPets);
        }
    }

}
