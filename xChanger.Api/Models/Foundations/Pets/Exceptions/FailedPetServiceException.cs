//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using Xeptions;

namespace xChanger.Api.Models.Foundations.Pets.Exceptions
{
    public class FailedPetServiceException : Xeption
    {
        public FailedPetServiceException(Exception innerException)
            : base(message: "Failed pet service error occurred, contact support",
                  innerException)
        { }
    }
}
