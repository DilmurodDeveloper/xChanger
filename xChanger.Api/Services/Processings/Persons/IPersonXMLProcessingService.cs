//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.Persons;

namespace xChanger.Api.Services.Processings.Persons
{
    public interface IPersonXMLProcessingService
    {
        ValueTask ExportPersonPetsToXml(IEnumerable<Person> persons, string filePath);
        ValueTask<Stream> RetrievePersonPetsXml(string filePath);
    }
}
