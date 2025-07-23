//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

namespace xChanger.Api.Brokers.Sheets
{
    public partial class SheetBroker
    {
        public async ValueTask UploadExternalPersonPetsFileAsync(IFormFile file)
        {
            string filePath = GetSheetLocationWithName();

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
    }
}
