//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.ExternalPersons;
using xChanger.Api.Services.Foundations.ExternalPersons;

namespace xChanger.Api.Services.Processings.ExternalPersons
{
    public class ExternalPersonEventProcessingService : IExternalPersonEventProcessingService
    {
        private readonly IExternalPersonEventService externalPersonEventService;

        public ExternalPersonEventProcessingService(
            IExternalPersonEventService externalPersonEventService) =>
                this.externalPersonEventService = externalPersonEventService;

        public ValueTask AddExternalPerson(List<ExternalPerson> externalPersons) =>
            this.externalPersonEventService.AddExternalPerson(externalPersons);

        public ValueTask<List<ExternalPerson>> RetrieveExternalPerson() =>
            this.externalPersonEventService.RetrieveExternalPerson();
    }
}
