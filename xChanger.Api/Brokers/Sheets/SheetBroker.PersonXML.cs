//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using System.Xml.Serialization;
using xChanger.Api.Models.Foundations.Persons;

namespace xChanger.Api.Brokers.Sheets
{
    public partial class SheetBroker
    {
        public async ValueTask SavePeopleWithPetsToXmlFile(
            IEnumerable<Person> peopleWithPets,
            string filePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Person>));
            await using FileStream fileStream = new FileStream(filePath, FileMode.Create);
            xmlSerializer.Serialize(fileStream, peopleWithPets);
        }

        public async ValueTask<MemoryStream> RetrievePeopleWithPetsXmlFile(string filePath)
        {
            byte[] fileBytes = await File.ReadAllBytesAsync(filePath);
            return new MemoryStream(fileBytes);
        }
    }
}
