//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using Xeptions;

namespace xChanger.Api.Models.Foundations.Pets.Exceptions
{
    public class NotFoundPetException : Xeption
    {
        public NotFoundPetException(Guid petId)
            : base(message: $"Couldn't find pet with id {petId}")
        { }
    }
}
