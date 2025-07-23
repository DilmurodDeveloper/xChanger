//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Brokers.Sheets;
using xChanger.Api.Models.Foundations.ExternalPersons;

namespace xChanger.Api.Services.Foundations.ExternalPersons
{
    public class ExternalPersonService : IExternalPersonService
    {
        private readonly ISheetBroker sheetBroker;

        public ExternalPersonService(ISheetBroker sheetBroker) =>
            this.sheetBroker = sheetBroker;

        public ValueTask<List<ExternalPerson>> RetrieveAllExternalPersonsAsync() =>
            this.sheetBroker.ReadAllExternalPersonPetsAsync();
    }
}
