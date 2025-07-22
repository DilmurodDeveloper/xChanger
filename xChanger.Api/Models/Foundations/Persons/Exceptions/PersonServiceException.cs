//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using Xeptions;

namespace xChanger.Api.Models.Foundations.Persons.Exceptions
{
    public class PersonServiceException : Xeption
    {
        public PersonServiceException(Xeption innerException)
            : base(message: "Person service error occurred, contact support",
                  innerException)
        { }
    }
}
