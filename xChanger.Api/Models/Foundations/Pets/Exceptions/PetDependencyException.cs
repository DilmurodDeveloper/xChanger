//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using Xeptions;

namespace xChanger.Api.Models.Foundations.Pets.Exceptions
{
    public class PetDependencyException : Xeption
    {
        public PetDependencyException(Xeption innerException)
            : base(message: "Pet dependency error occurred, contact support",
                  innerException)
        { }
    }
}
