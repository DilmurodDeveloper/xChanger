//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

namespace xChanger.Api.Services.Foundations.ExternalPersons
{
    public interface IExternalPersonInputService
    {
        ValueTask UploadExternalPersonFileAsync(IFormFile file);
    }
}
