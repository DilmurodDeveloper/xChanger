//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.Persons;
using xChanger.Api.Services.Foundations.Persons;

namespace xChanger.Api.Services.Processings.Persons
{
    public class PersonXMLProcessingService : IPersonXMLProcessingService
    {
        private readonly IPersonXMLService personXMLService;

        public PersonXMLProcessingService(IPersonXMLService personXMLService) =>
            this.personXMLService = personXMLService;

        public ValueTask ExportPersonPetsToXml(IEnumerable<Person> persons, string filePath) =>
            this.personXMLService.ExportPersonPetsToXml(persons, filePath);

        public ValueTask<Stream> RetrievePersonPetsXml(string filePath) =>
            this.personXMLService.RetrievePersonPetsXml(filePath);
    }
}
