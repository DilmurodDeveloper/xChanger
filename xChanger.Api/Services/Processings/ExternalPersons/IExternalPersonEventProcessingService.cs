//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.ExternalPersons;

namespace xChanger.Api.Services.Processings.ExternalPersons
{
    public interface IExternalPersonEventProcessingService
    {
        ValueTask AddExternalPerson(List<ExternalPerson> externalPersons);
        ValueTask<List<ExternalPerson>> RetrieveExternalPerson();
    }
}
