//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using Xeptions;

namespace xChanger.Api.Models.Foundations.Persons.Exceptions
{
    public class PersonDependencyValidationException : Xeption
    {
        public PersonDependencyValidationException(Xeption innerException)
            : base(message: "Person dependency validation error occurred, fix the errors and try again",
                   innerException)
        { }
    }
}
