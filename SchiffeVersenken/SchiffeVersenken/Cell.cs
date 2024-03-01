using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace SchiffeVersenken
{
    public class Cell : INotifyPropertyChanged
    {
        GameField parent;
        public Point pos { get; set; }

        bool ship = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsShip
        {
            get { return ship; }
            set
            {
                ship = value;
                if (!IsShipAllowed)
                    return;

                if (value)
                    Color = Brushes.Gray;
                if (!value)
                    Color = Brushes.AliceBlue;
            }
        }
        public bool IsHit { get; set; }
        public bool IsMiss { get; set; }
        public bool IsShipAllowed { get; set; }
        SolidColorBrush _Color = Brushes.AliceBlue;
        public SolidColorBrush Color
        {
            get { return _Color; }
            set
            {
                _Color = value;
                OnPropertyChanged("Color");
            }
        }

        public Cell(GameField parent, Point pos)
        {
            this.parent = parent;
            this.pos = pos;
            IsShip = false;
            IsHit = false;
            IsMiss = false;
            IsShipAllowed = true;
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public override string ToString() => $"P:{pos};IsShip={IsShip};IsShipAllowed={IsShipAllowed};IsHit={IsHit};IsMiss={IsMiss};";

        internal void toggleShip()
        {
            IsShip = !IsShip;
            parent.calculateShips();
        }
    }
}
