//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.Persons;

namespace xChanger.Api.Brokers.Sheets
{
    public partial interface ISheetBroker
    {
        ValueTask SavePeopleWithPetsToXmlFile(
            IEnumerable<Person> peopleWithPets,
            string filePath);

        ValueTask<MemoryStream> RetrievePeopleWithPetsXmlFile(string filePath);
    }
}
