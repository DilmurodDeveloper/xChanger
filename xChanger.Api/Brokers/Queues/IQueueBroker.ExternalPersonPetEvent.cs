//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.ExternalPersons;

namespace xChanger.Api.Brokers.Queues
{
    public partial interface IQueueBroker
    {
        ValueTask AddExternalPersonPets(List<ExternalPerson> externalPersonPets);
        ValueTask<List<ExternalPerson>> ReadExternalPersonPets();
    }
}
