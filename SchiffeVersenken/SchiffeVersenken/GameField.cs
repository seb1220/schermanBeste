using System.Collections.ObjectModel;
using System.Windows;

namespace SchiffeVersenken
{

    public class GameField
    {
        public static int Columns { get; set; }
        public static int Rows { get; set; }
        public static int Ships5 { get; set; }
        public static int Ships4 { get; set; }
        public static int Ships3 { get; set; }
        public static int Ships2 { get; set; }

        public ObservableCollection<Cell> Field { get; set; }

        public GameField()
        {
            Field = new ObservableCollection<Cell>();
            initializeField();
        }

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
