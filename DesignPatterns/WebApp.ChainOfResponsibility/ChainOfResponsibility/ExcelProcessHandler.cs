using ClosedXML.Excel;
using System.Data;

namespace WebApp.ChainOfResponsibility.ChainOfResponsibility
{
    public class ExcelProcessHandler<T> : ProcessHandler
    {
        private DataTable GetTable(object o)
        { 
            var table = new DataTable();
            var type = typeof(T);
            type.GetProperties().ToList().ForEach(p => table.Columns.Add(p.Name, p.PropertyType)); // sayesinde T tipindeki nesnenin property'lerini alıp DataTable'ın column'larına ekliyoruz.

            var list = o as List<T>;

            if (list != null)
            {
                list.ForEach(item =>
                {
                    var row = table.NewRow();
                    type.GetProperties().ToList().ForEach(p => row[p.Name] = p.GetValue(item)); // sayesinde T tipindeki nesnenin property'lerini alıp DataTable'ın column'larına ekliyoruz.
                    table.Rows.Add(row);
                });
            }

            return table;
        }
        public override object Handle(object request)
        {
            var wb = new XLWorkbook(); // ClosedXML.Excel kütüphanesini kullanarak Excel dosyası oluşturuyoruz.

            var ds = new DataSet(); // DataSet oluşturuyoruz.

            var dt = GetTable(request); // T tipindeki nesneleri DataTable'a çeviriyoruz.

            ds.Tables.Add(dt); // DataTable'ı DataSet'e ekliyoruz.

            wb.Worksheets.Add(ds); // DataSet'i Excel dosyasına ekliyoruz.

            var ms = new MemoryStream(); // MemoryStream oluşturuyoruz.

            wb.SaveAs(ms); // Excel dosyasını MemoryStream'e kaydediyoruz.

            return base.Handle(ms); // MemoryStream'i ProcessHandler sınıfına gönderiyoruz.
        }
    }
}
