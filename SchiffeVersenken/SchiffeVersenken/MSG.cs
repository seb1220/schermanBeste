using System.Collections.Generic;

namespace SchiffeVersenken
{
    public class MSG
    {
        public PacketType Type { get; set; }

        // Configuration
        public int Start { get; set; }  // 0 Server, 1 Client 
        public int Size { get; set; }  // Field is a square https://de.wikipedia.org/wiki/Schiffe_versenken
        public int TwoShips { get; set; }
        public int ThreeShips { get; set; }
        public int FourShips { get; set; }
        public int FiveShips { get; set; }

        // Field
        public List<int> ShipList { get; set; } // 0 no ship, 1 ship - list.Count = size^2, Only Client to Server

        // Shot
        public int Index { get; set; }

        // ShotResponse
        public bool Hit { get; set; }
        public bool Sunk { get; set; } // Sunk = true, if every part of the ship is hit

        // Finish
        public int Winner { get; set; } // 0 Server, 1 Client, Server calculates winner
    }

    public enum PacketType
    {
        Configuration, Field, Shot, ShotResponse, Finish
    }
}
