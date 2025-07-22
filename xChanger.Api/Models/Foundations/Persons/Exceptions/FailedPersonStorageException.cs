//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using Xeptions;

namespace xChanger.Api.Models.Foundations.Persons.Exceptions
{
    public class FailedPersonStorageException : Xeption
    {
        public FailedPersonStorageException(Exception innerException)
            : base(message: "Failed person storage error occurred, contact support",
                  innerException)
        { }
    }
}
