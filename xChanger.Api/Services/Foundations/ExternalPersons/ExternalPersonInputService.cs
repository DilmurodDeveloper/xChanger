//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Brokers.Loggings;
using xChanger.Api.Brokers.Sheets;

namespace xChanger.Api.Services.Foundations.ExternalPersons
{
    public class ExternalPersonInputService : IExternalPersonInputService
    {
        private readonly ISheetBroker sheetBroker;
        private readonly ILoggingBroker loggingBroker;

        public ExternalPersonInputService(
            ISheetBroker sheetBroker,
            ILoggingBroker loggingBroker)
        {
            this.sheetBroker = sheetBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask UploadExternalPersonFileAsync(IFormFile file) =>
            await this.sheetBroker.UploadExternalPersonPetsFileAsync(file);
    }
}
