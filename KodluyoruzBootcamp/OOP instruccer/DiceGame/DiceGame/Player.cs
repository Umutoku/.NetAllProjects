using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    public class Player
    {
        public string Name { get; set; }
        public Dice DiceOne { get; set; }
        public Dice DiceTwo { get; set; }

        public Player()
        {
            DiceOne = new Dice();
            DiceTwo = new Dice();
        }

        private int NumberOfDiceOne = 0;
        private int NumberOfDiceTwo = 0;

        public void Play()
        {
             NumberOfDiceOne = DiceOne.Number;
             NumberOfDiceTwo = DiceTwo.Number;
        }

        public int Score { get { return NumberOfDiceOne + NumberOfDiceTwo; } }


    }
}
