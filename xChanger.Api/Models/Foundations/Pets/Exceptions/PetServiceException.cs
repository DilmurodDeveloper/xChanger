//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using Xeptions;

namespace xChanger.Api.Models.Foundations.Pets.Exceptions
{
    public class PetServiceException : Xeption
    {
        public PetServiceException(Xeption innerException)
            : base(message: "Person service error occurred, contact support",
                  innerException)
        { }
    }
}
