//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using Xeptions;

namespace xChanger.Api.Models.Foundations.Persons.Exceptions
{
    public class PersonDependencyException : Xeption
    {
        public PersonDependencyException(Xeption innerException)
            : base(message: "Person dependency error occurred, contact support",
                  innerException)
        { }
    }
}
