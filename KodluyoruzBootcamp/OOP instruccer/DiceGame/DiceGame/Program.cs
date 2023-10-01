using System;

namespace DiceGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*
             *İki oyuncu olacak 
             * Her bir oyuncuda iki adet zar olacak
             * Oyuncular sırası ile zar atar
             * Zarlar karşılaştırılır
             * Büyük atan kazanır
             * 
             * Nesneler
             * 1.Oyun
             * 2.Oyuncu
             * 3.Zar
             */

            Game game = new Game();
            game.PlayerOne = new Player { Name = "Türkay" };
            game.PlayerTwo = new Player { Name = "Umut" };
            game.PlayAndTurn();
            game.ShowWinner();


        }
    }
}
