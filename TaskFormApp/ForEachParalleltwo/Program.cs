using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ForEachParalleltwo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            long totalByte = 0;

            var files = Directory.GetFiles(@"C:\Users\umuto\OneDrive - Istanbul Universitesi\Desktop\Yeni klasör (2)");
        }
    }
}


//long FilesByte = 0;
//Stopwatch sw = new Stopwatch();
//sw.Start();
//string picturePath = @"C:\Users\umuto\OneDrive - Istanbul Universitesi\Desktop\Yeni klasör (2)";
//var files = Directory.GetFiles(picturePath);

//Parallel.ForEach(files, (item) =>
//{
//    FileInfo f = new FileInfo(item);

//    Interlocked.Add(ref FilesByte, f.Length);

//}
//);

