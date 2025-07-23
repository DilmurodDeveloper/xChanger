//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

namespace xChanger.Api.Services.Orchestrations.ExternalPersons
{
    public interface IExternalPersonOrchestrationService
    {
        ValueTask ProcessAllExternalPersonsAsync();
    }
}
