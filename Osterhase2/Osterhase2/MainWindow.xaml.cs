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
        List<Personen> PersonList;
        public MainWindow()
        {
            InitializeComponent();

            DataConnection dataConnection = new DataConnection("SQLite", "Data Source = OsterhaseDB.db");

            PersonList = dataConnection.GetTable<Personen>().ToList();
            foreach (Personen person in PersonList)
                Console.WriteLine(person);

            //Database database = new Database();
            //database.Insert(new Person() { Latitude=12, Name="test", Longitude=12334});

            //var testPerson = database.GetTable<Person>().ToList();
            //foreach (var person in testPerson)
            //    Console.WriteLine(person.Name);

            MapCanvas.Children.Clear();
            PersonList.ForEach(p =>
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Width = 30;
                rectangle.Height = 30;
                rectangle.Fill = Brushes.Blue;

                Canvas.SetTop(rectangle, 10);
                Canvas.SetLeft(rectangle, 100);
                MapCanvas.Children.Add(rectangle);
            });
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
        }
    }
}