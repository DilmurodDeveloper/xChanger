//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using Xeptions;

namespace xChanger.Api.Models.Foundations.Pets.Exceptions
{
    public class LockedPetException : Xeption
    {
        public LockedPetException(Exception innerException)
            : base(message: "Pet is locked, please try again",
                  innerException)
        { }
    }
}
