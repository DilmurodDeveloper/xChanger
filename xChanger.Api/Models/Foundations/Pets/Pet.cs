//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

namespace xChanger.Api.Models.Foundations.Pets
{
    public class Pet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public PetType Type { get; set; }

        public Guid PersonId { get; set; }
    }
}
