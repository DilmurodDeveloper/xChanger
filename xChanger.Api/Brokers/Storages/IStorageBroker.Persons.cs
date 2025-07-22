//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.Persons;

namespace xChanger.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Person> InsertPersonAsync(Person person);
        IQueryable<Person> SelectAllPersons();
        IQueryable<Person> SelectAllPersonsWithPets();
        ValueTask<Person> SelectPersonByIdAsync(Guid personId);
        ValueTask<Person> UpdatePersonAsync(Person person);
    }
}
