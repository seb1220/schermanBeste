using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace SchiffeVersenken
{

    public class GameField
    {
        public static int Columns { get; private set; }
        public static int Rows { get; private set; }
        public static int Ships5 { get; private set; }
        public static int Ships4 { get; private set; }
        public static int Ships3 { get; private set; }
        public static int Ships2 { get; private set; }

        public static void setConfig(int columns, int rows, int ships5, int ships4, int ships3, int ships2)
        {
            Columns = columns;
            Rows = rows;
            Ships5 = ships5;
            Ships4 = ships4;
            Ships3 = ships3;
            Ships2 = ships2;
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs("Columns"));
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs("Rows"));
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
            for (int i = 0; i < Columns; ++i)
            {
                for (int j = 0; j < Rows; ++j)
                {
                    Field.Add(new Cell(new Point(i, j)));
                }
            }
        }
    }
}
