using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace SchiffeVersenken
{

    public class GameField
    {
        public static int Size { get; private set; }
        public static int Ships5 { get; private set; }
        public static int Ships4 { get; private set; }
        public static int Ships3 { get; private set; }
        public static int Ships2 { get; private set; }

        public static void setConfig(int size, int ships5, int ships4, int ships3, int ships2)
        {
            Size = size;
            Ships5 = ships5;
            Ships4 = ships4;
            Ships3 = ships3;
            Ships2 = ships2;
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs("Size"));
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs("Ships5"));
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs("Ships4"));
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs("Ships3"));
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs("Ships2"));
        }

        public ObservableCollection<Cell> Field { get; set; }

        public GameField()
        {
            Field = new ObservableCollection<Cell>();
            initializeField();
        }

        public static event PropertyChangedEventHandler StaticPropertyChanged;

        private void initializeField()
        {
            for (int i = 0; i < Size; ++i)
            {
                for (int j = 0; j < Size; ++j)
                {
                    Field.Add(new Cell(new Point(i, j)));
                }
            }
        }
    }
}
