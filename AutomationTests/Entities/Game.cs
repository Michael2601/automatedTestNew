using System;
using System.Collections.Generic;
using System.Text;

namespace AutomationTests.Entities
{
    internal class Game
    {
        public string name { get; set; }
        public string genre { get; set; }
        public string price { get; set; }

        public Game() { 
        }
        public Game(string name, string genre, string price) {
            this.name = name;
            this.genre = genre;
            this.price = price;
        }

        public override string ToString()
        {
            return $"Game{{Name: {name}, genre: {genre}, price: {price}}}";
        }
    }
}
