//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.ExternalPersons;

namespace xChanger.Api.Services.Foundations.ExternalPersons
{
    public interface IExternalPersonService
    {
        ValueTask<List<ExternalPerson>> RetrieveAllExternalPersonsAsync();
    }
}
