using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PA3_UE
{

    public class GameField : INotifyPropertyChanged
    {
        public int Size { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void ChangeSize(int size)
        {
            Size = size;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Size"));
            initializeField();
        }

        public ObservableCollection<Cell> Field { get; set; }

        public GameField()
        {
            Field = new ObservableCollection<Cell>();
            initializeField();
        }

        public void CellHit(int cell, CellType type, int nearbyMines = -1)
        {
            Field?[cell].SetType(type);

            if (nearbyMines != -1)
                Field?[cell].SetNearbyMines(nearbyMines);
        }

        private void initializeField()
        {
            Field.Clear();
            for (int i = 0; i < Size; ++i)
            {
                for (int j = 0; j < Size; ++j)
                {
                    Field.Add(new Cell(this, new System.Windows.Point(i, j)));
                }
            }
        }

    }
}
