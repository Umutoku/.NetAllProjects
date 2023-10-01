using System;

namespace MethodOverloading
{
    public enum Geometry
    {
        Square,
        Circle
    }
    
    public enum TurkishManAffinity
    {
        Baba=1,
        Abi=2,
        Amca=4,
        Dayı=8,
        Dede=16
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Örnek 1 Kare:{alternativeGetArea(15)}");
            Console.WriteLine($"Örnek 1 Kare:{alternativeGetArea(15,geometry:"Circle")}");
            Console.WriteLine($"Örnek 1 Kare:{alternativeGetAreaWithEnum(4,geometry: Geometry.Square)}");

            TurkishManAffinity affinity = TurkishManAffinity.Abi | TurkishManAffinity.Dayı;
            Console.WriteLine($"Türkayın akrabalık değeri: {affinity}");
        }

        /// <summary>
        /// get area for Circle or Square
        /// </summary>
        /// <param name="unit1Length"></param>
        /// <param name="geometry"></param>
        /// <returns></returns>
        static double getArea(int unit1Length, string geometry)
        {
            double calculatedValue = 0;
            switch (geometry)
            {
                case "Square":
                    calculatedValue = unit1Length * unit1Length;
                    break;
                case "Circle":
                    calculatedValue = (unit1Length * unit1Length) / Math.PI;
                    break;

            }
            return calculatedValue;
        }
        static double alternativeGetArea(int unit1Length, int unit2Length = 1, string geometry = "Square")
        {
            double calculatedValue = 0;
            switch (geometry)
            {
                case "Square":
                    calculatedValue = unit1Length * unit1Length;
                    break;
                case "Circle":
                    calculatedValue = (unit1Length * unit1Length) / Math.PI;
                    break;
                default:
                    Console.WriteLine("Bu kadar");
                    break;
            }

            return (calculatedValue);
        }

        static double alternativeGetAreaWithEnum(int unit1Length, int unit2Length = 1,Geometry geometry=Geometry.Square)
        {
            double returnValue = 0;
            switch (geometry)
            {
                
                case Geometry.Square:
                    returnValue = Math.Pow(unit1Length, 2);
                    break;
                case Geometry.Circle:
                    returnValue = Math.Pow(unit1Length, 2) * Math.PI;
                    break;
                default:
                    break;

            }
            return returnValue;
        }

    }
}
