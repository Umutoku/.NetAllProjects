using System;

namespace InhetitanceAndPolymorphism
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            Player.Weapon = new Knife();
            Player.Weapon = new Sniper();


            Cooker cooker = new Cooker();


        }
    }
}
