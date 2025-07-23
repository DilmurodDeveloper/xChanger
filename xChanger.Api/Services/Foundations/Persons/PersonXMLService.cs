//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Brokers.Sheets;
using xChanger.Api.Models.Foundations.Persons;

namespace xChanger.Api.Services.Foundations.Persons
{
    public class PersonXMLService : IPersonXMLService
    {
        private readonly ISheetBroker sheetBroker;

        public PersonXMLService(ISheetBroker sheetBroker) =>
            this.sheetBroker = sheetBroker;

        public async ValueTask ExportPersonPetsToXml(
            IEnumerable<Person> persons, string filePath) =>
                await this.sheetBroker.SavePersonWithPetsToXmlFile(persons, filePath);

        public async ValueTask<Stream> RetrievePersonPetsXml(string filePath) =>
            await this.sheetBroker.RetrievePersonWithPetsXmlFile(filePath);
    }
}
