//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using xChanger.Api.Models.Foundations.Persons.Exceptions;
using xChanger.Api.Models.Orchestrations;
using xChanger.Api.Services.Coordinations;
using xChanger.Api.Services.Orchestrations.Persons;
using xChanger.Api.Services.Processings.ExternalPersons;

namespace xChanger.Api.Controllers
{
    public class PersonsController : RESTFulController
    {
        private readonly IExternalPersonInputProcessingService externalPersonInputProcessingService;
        private readonly IExternalPersonWithPetsCoordinationService externalPersonWithPetsCoordinationService;
        private readonly IPersonOrchestrationService personOrchestrationService;

        public PersonsController(
            IExternalPersonInputProcessingService externalPersonInputProcessingService,
            IExternalPersonWithPetsCoordinationService externalPersonWithPetsCoordinationService,
            IPersonOrchestrationService personOrchestrationService)
        {
            this.externalPersonInputProcessingService = externalPersonInputProcessingService;
            this.externalPersonWithPetsCoordinationService = externalPersonWithPetsCoordinationService;
            this.personOrchestrationService = personOrchestrationService;
        }

        [HttpPost("upload-and-store")]
        public async ValueTask<ActionResult<List<PersonPet>>> UploadAndStorePerson(IFormFile file)
        {
            await this.externalPersonInputProcessingService.UploadExternalPersonFileAsync(file);

            List<PersonPet> storedPerson =
                await this.externalPersonWithPetsCoordinationService.CoordinateExternalPersonAsync();

            return Ok(storedPerson);
        }

        [HttpGet("export/download")]
        public async ValueTask<ActionResult> DownloadPersonWithPetsXml()
        {
            try
            {
                await this.personOrchestrationService.ExportAllPersonWithPetsToXmlAsync();

                Stream xmlFileStream =
                    await this.personOrchestrationService.RetrievePersonWithPetsXmlFileAsync();

                return File(xmlFileStream, "application/xml", "PersonWithPets.xml");
            }
            catch (PersonDependencyException personDependencyException)
            {
                return InternalServerError(personDependencyException.InnerException);
            }
            catch (PersonServiceException personServiceException)
            {
                return InternalServerError(personServiceException.InnerException);
            }
        }
    }
}
