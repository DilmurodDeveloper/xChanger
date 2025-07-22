//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using Xeptions;

namespace xChanger.Api.Models.Foundations.Persons.Exceptions
{
    public class PersonValidationException : Xeption
    {
        public PersonValidationException(Xeption innerException)
            : base(message: "Person validation error occured, fix the errors and try again.",
                  innerException)
        { }
    }
}
