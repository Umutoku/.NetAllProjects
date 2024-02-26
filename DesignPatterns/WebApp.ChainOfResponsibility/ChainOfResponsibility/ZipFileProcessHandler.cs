using System.IO.Compression;

namespace WebApp.ChainOfResponsibility.ChainOfResponsibility
{
    public class ZipFileProcessHandler<T> : ProcessHandler
    {
        public override object Handle(object request)
        {
            var memoryStream = request as MemoryStream;

            memoryStream.Position = 0; // MemoryStream'in pozisyonunu sıfırlıyoruz çünkü MemoryStream'den okuma yapacağız.

            using (var packageStream = new MemoryStream()) // Zip dosyasını oluşturmak için MemoryStream oluşturuyoruz.
            {
                using (var archive = new ZipArchive(packageStream, ZipArchiveMode.Create, true)) 
                {
                    var zipArchiveEntry = archive.CreateEntry($"{typeof(T).Name}"); // Zip dosyasına ekleyeceğimiz dosyanın adını belirtiyoruz.

                    using (var zipStream = zipArchiveEntry.Open())
                    {
                        memoryStream.CopyTo(zipStream); // MemoryStream'deki veriyi Zip dosyasına kopyalıyoruz.
                    }
                }

                return base.Handle(packageStream); // Zip dosyasını ProcessHandler sınıfına gönderiyoruz.
            }

        }
    }
}
