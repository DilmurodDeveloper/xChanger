//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.Persons;

namespace xChanger.Api.Brokers.Sheets
{
    public partial interface ISheetBroker
    {
        ValueTask SavePersonWithPetsToXmlFile(
            IEnumerable<Person> personWithPets,
            string filePath);

        ValueTask<MemoryStream> RetrievePersonWithPetsXmlFile(string filePath);
    }
}
