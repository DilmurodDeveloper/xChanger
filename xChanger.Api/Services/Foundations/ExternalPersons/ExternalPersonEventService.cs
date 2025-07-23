//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Brokers.Queues;
using xChanger.Api.Models.Foundations.ExternalPersons;

namespace xChanger.Api.Services.Foundations.ExternalPersons
{
    public class ExternalPersonEventService : IExternalPersonEventService
    {
        private readonly IQueueBroker queueBroker;

        public ExternalPersonEventService(IQueueBroker queueBroker) =>
            this.queueBroker = queueBroker;

        public ValueTask AddExternalPerson(List<ExternalPerson> externalPersonPets) =>
            this.queueBroker.AddExternalPersonPets(externalPersonPets);

        public ValueTask<List<ExternalPerson>> RetrieveExternalPerson() =>
            this.queueBroker.ReadExternalPersonPets();
    }
}
