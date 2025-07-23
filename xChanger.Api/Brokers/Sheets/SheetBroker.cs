//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

namespace xChanger.Api.Brokers.Sheets
{
    public partial class SheetBroker : ISheetBroker, IDisposable
    {
        private readonly IConfiguration configuration;

        public SheetBroker(IConfiguration configuration) =>
            this.configuration = configuration;

        public void Dispose() { }

        private string GetSheetLocationWithName()
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fileName = this.configuration.GetConnectionString("SheetConnection");
            return Path.Combine(folderPath, fileName);
        }


        private FileInfo GetFileInfo() =>
            new FileInfo(fileName: GetSheetLocationWithName());
    }
}
