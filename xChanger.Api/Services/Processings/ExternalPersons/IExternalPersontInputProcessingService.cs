//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

namespace xChanger.Api.Services.Processings.ExternalPersons
{
    public interface IExternalPersontInputProcessingService
    {
        ValueTask UploadExternalPersonFileAsync(IFormFile file);
    }
}
