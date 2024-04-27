using DataModel;
using LinqToDB;
using LinqToDB.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Osterhase2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Personen> personList;
        DataConnection dataConnection;

        public string InputName {  get; set; }
        public double InputLongitude { get; set; } = 16.209652;
        public double InputLatitude { get; set; } = 47.786898;
        public MainWindow()
        {
            InitializeComponent();

            dataConnection = new DataConnection("SQLite", "Data Source = OsterhaseDB.db");

            personList = dataConnection.GetTable<Personen>().ToList();
            foreach (Personen person in personList)
                Console.WriteLine(person);

            //Database database = new Database();
            //database.Insert(new Person() { Latitude=12, Name="test", Longitude=12334});

            //var testPerson = database.GetTable<Person>().ToList();
            //foreach (var person in testPerson)
            //    Console.WriteLine(person.Name);

            this.DataContext = this;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RefreshPeople();
        }

        private void RefreshPeople()
        {
            MapCanvas.Children.Clear();
            personList.ForEach(p =>
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Width = 40;
                rectangle.Height = 40;
                rectangle.Fill = Brushes.Blue;

                Canvas.SetBottom(rectangle, GetY((double)p.Latitude));
                Canvas.SetLeft(rectangle, GetX((double)p.Longitude));
                MapCanvas.Children.Add(rectangle);
            });
        }

        private void Add_Click(object sender, RoutedEventArgs e) // todo: maybe custom command for disable when name = ""
        {
            dataConnection.Insert(new Personen() { Name = InputName, Longitude = (decimal)InputLongitude, Latitude = (decimal)InputLatitude });
            personList = dataConnection.GetTable<Personen>().ToList();
            RefreshPeople();
        }

        private double GetX(double longitude)
        {
            return ((longitude - 16.209652) / (16.281017 - 16.209652)) * MapCanvas.ActualWidth; // TODO: remvoe widht of rect
        }

        private double GetY (double latitude) 
        {
            return ((latitude - 47.786898) / (47.846533 - 47.786898)) * MapCanvas.ActualHeight; // TODO: remvoe widht of rect
        }


        // TODO for ue 3 -> watch wikipedea graph video and find out how the graph was sliced into parts
    }
}