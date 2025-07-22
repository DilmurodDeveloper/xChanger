//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using Xeptions;

namespace xChanger.Api.Models.Foundations.Persons.Exceptions
{
    public class AlreadyExistsPersonException : Xeption
    {
        public AlreadyExistsPersonException(Exception innerException)
            : base(message: "Person already exists", innerException)
        { }
    }
}
