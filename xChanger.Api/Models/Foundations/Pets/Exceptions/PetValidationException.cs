//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using Xeptions;

namespace xChanger.Api.Models.Foundations.Pets.Exceptions
{
    public class PetValidationException : Xeption
    {
        public PetValidationException(Xeption innerException)
            : base(message: "Pet validation error occured, fix the errors and try again.",
                  innerException)
        { }
    }
}
