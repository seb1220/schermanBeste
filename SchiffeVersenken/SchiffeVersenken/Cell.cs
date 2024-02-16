using System.Windows;

namespace SchiffeVersenken
{
    public class Cell
    {
        public Point pos { get; set; }
        public bool IsShip { get; set; }
        public bool IsHit { get; set; }
        public bool IsMiss { get; set; }
        public bool IsShipAllowed { get; set; }

        public Cell(Point pos)
        {
            this.pos = pos;
            IsShip = false;
            IsHit = false;
            IsMiss = false;
            IsShipAllowed = true;
        }

        public override string ToString() => $"Cell at {pos} with IsShip={IsShip}, IsHit={IsHit}, IsMiss={IsMiss}, IsShipAllowed={IsShipAllowed}";
    }
}
