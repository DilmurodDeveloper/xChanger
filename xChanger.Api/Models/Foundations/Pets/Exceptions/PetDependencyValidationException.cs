//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using Xeptions;

namespace xChanger.Api.Models.Foundations.Pets.Exceptions
{
    public class PetDependencyValidationException : Xeption
    {
        public PetDependencyValidationException(Xeption innerException)
            : base(message: "Pet dependency validation error occurred, fix the errors and try again",
                   innerException)
        { }
    }
}
