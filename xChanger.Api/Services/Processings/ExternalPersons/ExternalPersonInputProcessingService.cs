//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Services.Foundations.ExternalPersons;

namespace xChanger.Api.Services.Processings.ExternalPersons
{
    public class ExternalPersonInputProcessingService : IExternalPersonInputProcessingService
    {
        private readonly IExternalPersonInputService externalPersonInputService;

        public ExternalPersonInputProcessingService(
            IExternalPersonInputService externalPersonInputService) =>
                this.externalPersonInputService = externalPersonInputService;

        public ValueTask UploadExternalPersonFileAsync(IFormFile file) =>
            this.externalPersonInputService.UploadExternalPersonFileAsync(file);
    }
}
