using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace PA3_UE
{
    public class Cell : INotifyPropertyChanged
    {
        GameField parent;
        public Point pos { get; set; }

        private CellType type;

        private int nearbyMines;

        public void SetNearbyMines(int mines)
        {
            nearbyMines = mines;
        }

        public void SetType(CellType type)
        {
            if (type == CellType.Unkown)
                Color = Brushes.White;
            else if (type == CellType.Cleared)
                Color = Brushes.GreenYellow;
            else if (type == CellType.Mine)
                Color = Brushes.Red;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        SolidColorBrush _Color = Brushes.White;
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
            type = CellType.Unkown;
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public override string ToString() => $"P:{pos};Type={type};{nearbyMines}";

    }

    public enum CellType
    {
        Unkown,
        Cleared,
        Mine
    }
}
