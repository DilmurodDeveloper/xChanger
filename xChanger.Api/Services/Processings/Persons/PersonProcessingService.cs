//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.Persons;

namespace xChanger.Api.Services.Processings.Persons
{
    public class PersonProcessingService : IPersonProcessingService
    {
        public Person ProcessPerson(Person person)
        {
            return new Person
            {
                Id = person.Id,
                Name = person.Name?.Trim(),
                Age = person.Age
            };
        }
    }
}
