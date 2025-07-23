//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.ExternalPersons;
using xChanger.Api.Services.Foundations.ExternalPersons;
using xChanger.Api.Services.Orchestrations.PersonPets;

namespace xChanger.Api.Services.Orchestrations.ExternalPersons
{
    public class ExternalPersonOrchestrationService : IExternalPersonOrchestrationService
    {
        private readonly IExternalPersonService externalPersonService;
        private readonly IPersonPetOrchestrationService personPetOrchestrationService;

        public ExternalPersonOrchestrationService(
            IExternalPersonService externalPersonService,
            IPersonPetOrchestrationService personPetOrchestrationService)
        {
            this.externalPersonService = externalPersonService;
            this.personPetOrchestrationService = personPetOrchestrationService;
        }

        public async ValueTask ProcessAllExternalPersonsAsync()
        {
            List<ExternalPerson> externalPersons =
                await this.externalPersonService.RetrieveAllExternalPersonsAsync();

            foreach (ExternalPerson externalPerson in externalPersons)
            {
                await this.personPetOrchestrationService.ProcessExternalPersonAsync(externalPerson);
            }
        }
    }

}
