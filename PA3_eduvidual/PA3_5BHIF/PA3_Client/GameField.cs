using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace PA3_Client
{
    [XmlInclude(typeof(Cell))]
    [Serializable]
    public class GameField : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        int width;
        public int Width
        {
            get { return width; }
            set
            {
                width = value;
                initializeField();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Width"));
            }
        }

        int height;
        public int Height
        {
            get { return height; }
            set
            {
                height = value;
                initializeField();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Height"));
            }
        }

        int mines;
        public int Mines
        {
            get { return mines; }
            set
            {
                mines = value;
                initializeField();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Mines"));
            }
        }

        [XmlArray("Field")]
        [XmlArrayItem("Cell")]
        public ObservableCollection<Cell> Field { get; set; }

        public GameField(int width, int height, int mines)
        {
            Width = width;
            Height = height;
            Mines = mines;

            Field = new ObservableCollection<Cell>();
            initializeField();
        }

        public GameField()
        {
        }

        public void CellHit(Point pos, int nearbyMines = -1)
        {
            Cell cell = CellByCoord(pos.X, pos.Y);
            cell.Value = nearbyMines;
        }

        private Cell CellByCoord(int x, int y)
        {
            foreach (Cell cell in Field)
            {
                if (cell == null) continue;
                if (cell.pos.X == x && cell.pos.Y == y) return cell;
            }

            return null;
        }

        private void initializeField()
        {
            if (Field == null)
                return;

            Field.Clear();
            for (int i = 0; i < Width; ++i)
            {
                for (int j = 0; j < Height; ++j)
                {
                    Field.Add(new Cell(new System.Drawing.Point(j, i), -1));
                }
            }
        }

    }
}
