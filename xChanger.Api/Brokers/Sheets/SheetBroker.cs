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

        private string GetSheetLocationWithName() =>
            this.configuration.GetConnectionString("SheetConnection");

        private FileInfo GetFileInfo() =>
            new FileInfo(fileName: GetSheetLocationWithName());
    }
}
