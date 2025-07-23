//- - - - - - - - - - - - - - - - - - - - - - - - - -
// Copyright (c) Coalition of Good-Hearted Engineers
// Free to Use for Precise File Conversion
//- - - - - - - - - - - - - - - - - - - - - - - - - -

using OfficeOpenXml;
using xChanger.Api.Models.Foundations.ExternalPersons;

namespace xChanger.Api.Brokers.Sheets
{
    public partial class SheetBroker
    {
        public async ValueTask<List<ExternalPerson>> ReadAllExternalPersonPetsAsync()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var externalPersons = new List<ExternalPerson>();
            using var sheetBroker = new SheetBroker(this.configuration);
            FileInfo file = sheetBroker.GetFileInfo();
            int row = 2, column = 1;

            using var excelPackage =
                new ExcelPackage(file);

            ExcelWorksheet workSheet =
                excelPackage.Workbook.Worksheets[PositionID: 0];

            await excelPackage.LoadAsync(file);

            while (!IsTrailingFinalRow(row, column, workSheet))
            {
                ExternalPerson externalPerson = new ExternalPerson();

                externalPerson.PersonName = workSheet.Cells[row, column].Value.ToString();
                externalPerson.Age = int.Parse(workSheet.Cells[row, column + 1].Value.ToString());
                externalPerson.PetOne = workSheet.Cells[row, column + 2].Value.ToString();
                externalPerson.PetOneType = workSheet.Cells[row, column + 3].Value.ToString();
                externalPerson.PetTwo = workSheet.Cells[row, column + 4].Value.ToString();
                externalPerson.PetTwoType = workSheet.Cells[row, column + 5].Value.ToString();
                externalPerson.PetThree = workSheet.Cells[row, column + 6].Value.ToString();
                externalPerson.PetThreeType = workSheet.Cells[row, column + 7].Value.ToString();
                externalPersons.Add(externalPerson);
                row++;
            }

            return externalPersons;

            static bool IsTrailingFinalRow(int row, int column, ExcelWorksheet workSheet) =>
                String.IsNullOrEmpty(workSheet.Cells[row, column].Value?.ToString());
        }
    }
}
