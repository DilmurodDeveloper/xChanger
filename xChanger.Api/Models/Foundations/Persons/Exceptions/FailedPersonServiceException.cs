//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using Xeptions;

namespace xChanger.Api.Models.Foundations.Persons.Exceptions
{
    public class FailedPersonServiceException : Xeption
    {
        public FailedPersonServiceException(Exception innerException)
            : base(message: "Failed person service error occurred, contact support",
                  innerException)
        { }
    }
}
