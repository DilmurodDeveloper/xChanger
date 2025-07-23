//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using Xeptions;

namespace xChanger.Api.Models.Foundations.Persons.Exceptions
{
    public class LockedPersonException : Xeption
    {
        public LockedPersonException(Exception innerException)
            : base(message: "Person is locked, please try again", innerException)
        { }
    }
}
