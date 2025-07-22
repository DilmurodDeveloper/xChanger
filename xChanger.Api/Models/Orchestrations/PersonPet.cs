//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using xChanger.Api.Models.Foundations.Persons;
using xChanger.Api.Models.Foundations.Pets;

namespace xChanger.Api.Models.Orchestrations
{
    public class PersonPet
    {
        public Person Person { get; set; }
        public List<Pet> Pets { get; set; }
    }
}
