//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.ExternalPersons;

namespace xChanger.Api.Services.Processings.ExternalPersons
{
    public class ExternalPersonProcessingService : IExternalPersonProcessingService
    {
        public ExternalPerson ProcessExternalPerson(ExternalPerson externalPerson)
        {
            return new ExternalPerson
            {
                PersonName = externalPerson.PersonName,
                Age = externalPerson.Age,
                PetOne = externalPerson.PetOne?.Trim().Replace("-", string.Empty),
                PetOneType = externalPerson.PetOneType?.Trim().Replace("-", string.Empty),
                PetTwo = externalPerson.PetTwo?.Trim().Replace("-", string.Empty),
                PetTwoType = externalPerson.PetTwoType?.Trim().Replace("-", string.Empty),
                PetThree = externalPerson.PetThree?.Trim().Replace("-", string.Empty),
                PetThreeType = externalPerson.PetThreeType?.Trim().Replace("-", string.Empty)
            };
        }
    }
}
