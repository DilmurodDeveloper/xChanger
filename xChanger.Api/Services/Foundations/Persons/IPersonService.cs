//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.Persons;

namespace xChanger.Api.Services.Foundations.Persons
{
    public interface IPersonService
    {
        ValueTask<Person> AddPersonAsync(Person person);
        IQueryable<Person> RetrieveAllPersons();
    }
}
