using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ForEachParalelApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int total = 0;

            Parallel.ForEach(Enumerable.Range(1, 100).ToList(), () => 0, (x, loop, subtotal) =>
            {
                subtotal += x;
                return subtotal;
            },(y)=>Interlocked.And(ref total,y));


            Parallel.For(0, 100, () => 0, (x, loop, subtotal) =>
            {
                subtotal += x;
                return subtotal;
            }, (y) => Interlocked.And(ref total, y));




            //long totalbyte = 0;

            //var files = Directory.GetFiles("c:/desktop/pictures");
            //Parallel.For(0, files.Length, (index) =>
            //{
            //    var file = new FileInfo(files[index]); 
            //    Interlocked.Add(ref totalbyte, file.Length);
            //});

            //Console.WriteLine("Total byte" + totalbyte.ToString());
        }
    }
}
