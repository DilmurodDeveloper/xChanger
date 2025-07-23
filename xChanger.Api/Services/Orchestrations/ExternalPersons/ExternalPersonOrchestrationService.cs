//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.ExternalPersons;
using xChanger.Api.Services.Foundations.ExternalPersons;
using xChanger.Api.Services.Orchestrations.PersonPets;
using xChanger.Api.Services.Processings.ExternalPersons;

namespace xChanger.Api.Services.Orchestrations.ExternalPersons
{
    public class ExternalPersonOrchestrationService : IExternalPersonOrchestrationService
    {
        private readonly IExternalPersonService externalPersonService;
        private readonly IPersonPetOrchestrationService personPetOrchestrationService;
        private readonly IExternalPersonProcessingService externalPersonProcessingService;


        public ExternalPersonOrchestrationService(
            IExternalPersonService externalPersonService,
            IPersonPetOrchestrationService personPetOrchestrationService,
            IExternalPersonProcessingService externalPersonProcessingService)
        {
            this.externalPersonService = externalPersonService;
            this.personPetOrchestrationService = personPetOrchestrationService;
            this.externalPersonProcessingService = externalPersonProcessingService;
        }

        public async ValueTask ProcessAllExternalPersonsAsync()
        {
            List<ExternalPerson> externalPersons =
                await this.externalPersonService.RetrieveAllExternalPersonsAsync();

            List<ExternalPerson> cleanedExternalPersons = externalPersons
                .Select(ep => this.externalPersonProcessingService.ProcessExternalPerson(ep))
                .ToList();

            foreach (ExternalPerson externalPerson in cleanedExternalPersons)
            {
                await this.personPetOrchestrationService.ProcessExternalPersonAsync(externalPerson);
            }
        }

    }

}
