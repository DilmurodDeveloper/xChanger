//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

namespace xChanger.Api.Services.Orchestrations.Persons
{
    public interface IPersonOrchestrationService
    {
        ValueTask ExportAllPersonWithPetsToXmlAsync();
        ValueTask<Stream> RetrievePersonWithPetsXmlFileAsync();
    }
}
