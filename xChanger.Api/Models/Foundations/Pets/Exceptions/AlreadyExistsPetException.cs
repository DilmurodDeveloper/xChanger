//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using Xeptions;

namespace xChanger.Api.Models.Foundations.Pets.Exceptions
{
    public class AlreadyExistsPetException : Xeption
    {
        public AlreadyExistsPetException(Exception innerException)
            : base(message: "Pet already exists", innerException)
        { }
    }
}
