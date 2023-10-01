using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InhetitanceAndPolymorphism
{
    public class Cooker
    {
        public void Cook(Food food)
        {
            food.Cook();
        }
    }

    public class Food
    {
        public List<string> Ingredients { get; set; }
        public int Duration { get; set; }
        public void Cook()
        {
            Console.WriteLine("Yemek pişiyor");
        }

    }

    public class VegatableFood:Food
    {

    }
    public class MeetFood : Food
    {

    }

    public class KuruFasulye : VegatableFood
    {

    }
    public class Kofte:MeetFood
    {

    }
}
