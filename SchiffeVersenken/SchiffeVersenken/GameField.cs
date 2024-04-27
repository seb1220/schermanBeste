using System.Collections.ObjectModel;
using System.ComponentModel;

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

        public bool IsValid { get; private set; } = false;
        public ObservableCollection<Cell> Field { get; set; }
        public int[] Ships { private set; get; }

        public GameField()
        {
            Field = new ObservableCollection<Cell>();
            initializeField();
            calculateShips();
        }

        public static event PropertyChangedEventHandler StaticPropertyChanged;

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

        public void calculateShips()
        {
            Ships = new int[] { 0, 0, 0, 0 };
            int placed_ships = 0;

            bool last = false;
            int streak = 0;

            for (int x = 0; x < Size; ++x) // vertical
            {
                for (int y = 0; y < Size; ++y)
                {
                    int field_number = Size * y + x;
                    Cell ship = Field[field_number];

                    ship.IsShipAllowed = true;

                    if (last && ship.IsShip)
                    {
                        if (streak == 0)
                            streak = 2;
                        else
                            streak += 1;
                    }
                    else if (last && streak != 0)
                    {
                        if (streak >= 5)
                        {
                            IsValid = false;
                        }
                        else
                        {
                            Ships[streak - 2]++;
                        }
                    }
                    else
                    {
                        streak = 0;
                    }
                    last = ship.IsShip;
                }
            }

            for (int y = 0; y < Size; ++y) // horizontal
            {
                for (int x = 0; x < Size; ++x)
                {
                    int field_number = Size * y + x;
                    Cell ship = Field[field_number];

                    if (ship.IsShip)
                    {
                        placed_ships++; // count ships

                        if ((field_number - 1 - Size) % Size != Size - 1 && field_number - 1 - Size >= 0) // top left diagonal
                            Field[field_number - 1 - Size].IsShipAllowed = false;

                        if ((field_number + 1 - Size) % Size != 0 && field_number + 1 - Size >= 0) // top right diagonal
                            Field[field_number + 1 - Size].IsShipAllowed = false;

                        if ((field_number - 1 + Size) % Size != Size - 1 && field_number - 1 + Size <= Size * Size) // bottom left diagonal
                            Field[field_number - 1 + Size].IsShipAllowed = false;

                        if ((field_number + 1 + Size) % Size != 0 && field_number + 1 + Size <= Size * Size) // bottom right diagonal
                            Field[field_number + 1 + Size].IsShipAllowed = false;
                    }

                    if (last && ship.IsShip)
                    {
                        if (streak == 0)
                            streak = 2;
                        else
                            streak += 1;
                    }
                    else if (last && streak != 0)
                    {
                        if (streak >= 5)
                        {
                            IsValid = false;
                        }
                        else
                        {
                            Ships[streak - 2]++;
                        }
                    }
                    else
                    {
                        streak = 0;
                    }
                }
            }

            if (Ships[0] == Ships2 && Ships[1] == Ships3 && Ships[2] == Ships4 && Ships[3] == Ships5)
                IsValid = true;
            else
                IsValid = false;

            int necessary_ships = Ships2 * 2 + Ships3 * 3 + Ships4 * 4 + Ships5 * 5;
            if (placed_ships != necessary_ships)
                IsValid = false;

        }
    }
}
