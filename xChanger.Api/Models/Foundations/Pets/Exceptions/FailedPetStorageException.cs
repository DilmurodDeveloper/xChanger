//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using Xeptions;

namespace xChanger.Api.Models.Foundations.Pets.Exceptions
{
    public class FailedPetStorageException : Xeption
    {
        public FailedPetStorageException(Exception innerException)
            : base(message: "Failed pet storage error occurred, contact support",
                  innerException)
        { }
    }
}
