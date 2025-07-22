//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using Microsoft.EntityFrameworkCore;
using xChanger.Api.Models.Foundations.Persons;

namespace xChanger.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Person> Persons { get; set; }

        public async ValueTask<Person> InsertPersonAsync(Person person) =>
            await InsertAsync(person);

        public IQueryable<Person> SelectAllPersons() => SelectAll<Person>();

        public IQueryable<Person> SelectAllPersonsWithPets() =>
            this.Persons.Include(person => person.Pets);

        public async ValueTask<Person> SelectPersonByIdAsync(Guid personId) =>
            await SelectAsync<Person>(personId);

        public async ValueTask<Person> UpdatePersonAsync(Person person) =>
            await UpdateAsync(person);
    }
}
