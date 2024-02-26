using ClosedXML.Excel;
using System.Data;

namespace WebApp.Command.Commands
{
    public class ExcelFile<T>
    {
        public readonly List<T> _list;

        public string FileName => $"{typeof(T).Name}.xlsx"; // excel file name

        public string FileType => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // excel file type

        public ExcelFile(List<T> list)
        {
            _list = list;
        }

        public MemoryStream Create()
        { 
            var wb = new XLWorkbook(); // excel workbook oluşturuyoruz 

            var ds = new DataSet(); // dataset oluşturuyoruz, çünkü excel'e dönüştürmek için dataset'e ihtiyacımız var

            ds.Tables.Add(GetTable()); // tabloyu dataset'e ekliyoruz

            wb.Worksheets.Add(ds); // dataset'i workbook'a ekliyoruz 

            var stream = new MemoryStream(); // memory stream oluşturuyoruz

            wb.SaveAs(stream); // workbook'u memory stream'e kaydediyoruz

            return stream;
        }

        private DataTable GetTable()
        { 
            var table = new DataTable();

            var type = typeof(T);

            type.GetProperties().ToList().ForEach(p => table.Columns.Add(p.Name, p.PropertyType)); // reflection sayesinde property'leri alıyoruz ve tabloya ekliyoruz

            _list.ForEach(item => table.Rows.Add(type.GetProperties().Select(p => p.GetValue(item)).ToArray())); // listeyi dönüp property'leri tabloya ekliyoruz

            return table;
        }
    }
}
