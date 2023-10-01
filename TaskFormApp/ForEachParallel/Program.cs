using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ForEachParallel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string picturePath = @"C:\Users\umuto\OneDrive - Istanbul Universitesi\Desktop\Yeni klasör (2)";
            var files = Directory.GetFiles(picturePath);

            Parallel.ForEach(files, (item) =>
            {
                Image img = new Bitmap(item);
                var thumbnail = img.GetThumbnailImage(50, 50, () => false, IntPtr.Zero);
                thumbnail.Save(Path.Combine(picturePath, "thumbnail",Path.GetFileName(item)));
            }
            );
            sw.Stop();
            Console.WriteLine("işlem bitti"+sw.ElapsedMilliseconds);

            sw.Reset();
            sw.Start();
            files.ToList().ForEach(x =>
            {
                Image img = new Bitmap(x);
                var thumbnail = img.GetThumbnailImage(50, 50, () => false, IntPtr.Zero);
                thumbnail.Save(Path.Combine(picturePath, "thumbnail", Path.GetFileName(x)));
            }
            );
            sw.Stop();
            Console.WriteLine("işlem bitti" + sw.ElapsedMilliseconds);
        }
    }
}
