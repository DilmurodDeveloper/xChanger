//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Orchestrations;

namespace xChanger.Api.Services.Coordinations
{
    public interface IExternalPersonWithPetsCoordinationService
    {
        ValueTask<List<PersonPet>> CoordinateExternalPersonAsync();
    }
}
