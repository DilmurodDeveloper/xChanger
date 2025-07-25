﻿//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.Persons;

namespace xChanger.Api.Services.Processings.Persons
{
    public interface IPersonProcessingService
    {
        ValueTask<Person> UpsertPersonAsync(Person person);
        IQueryable<Person> RetrieveAllPerson();
        IQueryable<Person> RetrieveAllPersonWithPets();
    }
}
