//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using Xeptions;

namespace xChanger.Api.Models.Foundations.Persons.Exceptions
{
    public class NotFoundPersonException : Xeption
    {
        public NotFoundPersonException(Guid personId)
            : base(message: $"Couldn't find person with id {personId}")
        { }
    }
}
