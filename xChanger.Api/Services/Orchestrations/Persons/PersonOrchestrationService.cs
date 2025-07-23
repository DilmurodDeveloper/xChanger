//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Services.Processings.Persons;
using Xeptions;

namespace xChanger.Api.Services.Orchestrations.Persons
{
    public class PersonOrchestrationService : IPersonOrchestrationService
    {
        private readonly IPersonProcessingService personProcessingService;
        private readonly IPersonXMLProcessingService personXMLProcessingService;

        private const string XmlFilePath = @"C:\Users\User\Desktop\MetaXPorter\MetaXPorter.Api\Resources\Export template.xml";

        public PersonOrchestrationService(
            IPersonProcessingService personProcessingService,
            IPersonXMLProcessingService personXMLProcessingService)
        {
            this.personProcessingService = personProcessingService;
            this.personXMLProcessingService = personXMLProcessingService;
        }

        public async ValueTask ExportAllPersonWithPetsToXmlAsync()
        {
            try
            {
                var peopleWithPets =
                    this.personProcessingService.RetrieveAllPersonWithPets().ToList();

                await this.personXMLProcessingService.ExportPersonPetsToXml(
                    peopleWithPets, XmlFilePath);
            }
            catch (Exception exception)
            {
                throw new Xeption(
                    message: "Orchestration service error occurred, contact support",
                    innerException: exception);
            }
        }

        public async ValueTask<Stream> RetrievePersonWithPetsXmlFileAsync()
        {
            try
            {
                return await this.personXMLProcessingService
                    .RetrievePersonPetsXml(XmlFilePath);
            }
            catch (Exception exception)
            {
                throw new Xeption(
                    message: "Error occurred while retrieving the exported XML file.",
                    innerException: exception);
            }
        }
    }
}
