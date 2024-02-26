using DinkToPdf.Contracts;
using DinkToPdf;
using System.Text;

namespace WebApp.Command.Commands
{
    public class PdfFile<T>
    {
        public readonly List<T> _list;

        public readonly HttpContext _context;

        public PdfFile(HttpContext context, List<T> list)
        {
            _context = context;
            _list = list;
        }

        public string FileName => $"{typeof(T).Name}.pdf";

        public string FileType => "application/octet-stream";

        public MemoryStream Create()
        {
            var type = typeof(T); // tipi alıyoruz

            var sb = new StringBuilder();

            sb.Append($@"<html>
                          <head></head>
                          <body>
                            <div class='text-center'><h1>{type.Name} tablo</h1></div>
                            <table class='table table-striped' align='center'>");

            sb.Append("<tr>");
            type.GetProperties().ToList().ForEach(p => sb.Append($"<th>{p.Name}</th>"));
            sb.Append("</tr>");

            _list.ForEach(item =>
            {
                sb.Append("<tr>");
                type.GetProperties().ToList().ForEach(p => sb.Append($"<td>{p.GetValue(item)}</td>"));
                sb.Append("</tr>");
            });

            sb.Append("</table></body></html>");

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                        ColorMode = ColorMode.Color, 
                        Orientation = Orientation.Portrait, // sayfa yönü
                        PaperSize = PaperKind.A4, // sayfa boyutu
    },
                Objects = {
                new ObjectSettings() {
                        PagesCount = true, // sayfa numaralarını göster
                        HtmlContent = sb.ToString(), // html içeriği
                        WebSettings = { DefaultEncoding = "utf-8",UserStyleSheet=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/lib/bootstrap/dist/css/bootstrap.css") }, // css dosyası
                        HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 } // header ayarları
        }
    }
            };

            var converter = _context.RequestServices.GetRequiredService<IConverter>(); // converter'ı alıyoruz

            MemoryStream stream = new MemoryStream(converter.Convert(doc)); // memory stream oluşturuyoruz ve dökümanı dönüştürüp memory stream'e kaydediyoruz

            return stream;
        }
    }
}
