using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

namespace PA3_Client
{

    public class Cell : INotifyPropertyChanged
    {
        public int X { get; set; }
        public int Y { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _text = "";
        public string Text
        {
            get
            {
                if (Value < 0)
                    return "";
                else
                    return Value.ToString();
            }
        }

        private int _value;
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;

                if (value == -1)
                    Color = Brushes.White;
                else if (value == 0)
                    Color = Brushes.LightGray;
                else if (value == 1)
                    Color = Brushes.Blue;
                else if (value == 2)
                    Color = Brushes.Green;
                else if (value == 3)
                    Color = Brushes.LightGreen;
                else if (value == 4)
                    Color = Brushes.Purple;
                else if (value == 5)
                    Color = Brushes.Maroon;
                else if (value == 6)
                    Color = Brushes.Turquoise;
                else if (value == 7)
                    Color = Brushes.Black;
                else if (value == 8)
                    Color = Brushes.Gray;
                else if (value == -2)
                    Color = Brushes.Red;

                OnPropertyChanged("Value");
                OnPropertyChanged("Text");
            }
        }
        private bool _isBomb = false;
        public bool IsBomb
        {
            get
            {
                return _isBomb;
            }
            set
            {
                _isBomb = value;
                OnPropertyChanged("IsBomb");
            }
        }
        [XmlIgnore]
        SolidColorBrush _Color = Brushes.White;
        [XmlIgnore]
        public SolidColorBrush Color
        {
            get { return _Color; }
            set
            {
                _Color = value;
                OnPropertyChanged("Color");
            }
        }

        public Cell(System.Drawing.Point pos, int value)
        {
            X = pos.X;
            Y = pos.Y;
            this.Value = value;
        }

        public Cell()
        {
        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public GameField Field { get; set; }
        private GameManager gm;
        public MainWindow()
        {
            LoadFromXML();

            Debug.WriteLine("asldfjaslöjdflasjfkljdf2");
            Debug.WriteLine(Field.Field[0].Value);

            InitializeComponent();

            WidthSlider.DataContext = Field;
            HeightSlider.DataContext = Field;
            MinesSlider.DataContext = Field;

            FieldComponent.DataContext = Field;

            Debug.WriteLine("asldfjaslöjdflasjfkljdf3");
            Debug.WriteLine(Field.Field[0].Value);
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            WidthSlider.IsEnabled = false;
            HeightSlider.IsEnabled = false;
            MinesSlider.IsEnabled = false;
            FieldComponent.SelectedIndex = -1;

            gm.StartGame(Field.Width, Field.Height, Field.Mines);
        }

        private void FieldComponent_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (FieldComponent.SelectedIndex == -1)
                return;


            Cell cell = FieldComponent.SelectedItem as Cell;
            if (cell.Value != -1)
                return;

            Debug.WriteLine("Sending Pick");
            gm.Hit(cell.X, cell.Y);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (gm.GameNumber == 0)
            {
                File.Delete("field.xml");
                File.Delete("gm.xml");
                return;
            }

            XmlSerializer fieldSerializer = new XmlSerializer(typeof(GameField));
            XmlSerializer gameSerializer = new XmlSerializer(typeof(GameManager));

            using (FileStream writer = new FileStream("field.xml", FileMode.Create))
            {
                fieldSerializer.Serialize(writer, Field);
            }

            using (FileStream writer = new FileStream("gm.xml", FileMode.Create))
            {
                gameSerializer.Serialize(writer, gm);
            }

            Debug.WriteLine("Safed Game and Field");
        }

        private void LoadFromXML()
        {
            XmlSerializer fieldSerializer = new XmlSerializer(typeof(GameField));
            XmlSerializer gameSerializer = new XmlSerializer(typeof(GameManager));

            if (File.Exists("field.xml"))
            {
                using (FileStream reader = new FileStream("field.xml", FileMode.Open))
                {
                    Field = (GameField)fieldSerializer.Deserialize(reader);
                }
                for (int i = 0; i < Field.Height * Field.Width; i++)
                    Field.Field.RemoveAt(0);
                Debug.WriteLine("asldfjaslöjdflasjfkljdf0");
                Debug.WriteLine($"Count: {Field.Field.Count}");
            }
            else
            {
                Debug.WriteLine("Creating new Field");
                Field = new GameField(5, 5, 10);
            }

            Debug.WriteLine("asldfjaslöjdflasjfkljdf1");
            Debug.WriteLine(Field.Field[0].Value);

            if (File.Exists("gm.xml"))
            {
                using (FileStream reader = new FileStream("gm.xml", FileMode.Open))
                {
                    gm = (GameManager)gameSerializer.Deserialize(reader);
                }
                gm.Field = Field;
                gm.Reconnect();
            }
            else
            {
                Debug.WriteLine("Creating new GameManager");
                gm = new GameManager(Field);
            }


        }
    }
}
