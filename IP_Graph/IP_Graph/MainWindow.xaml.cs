using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;

namespace IP_Graph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataConnection db;

        ITable<Router> Routers;
        ITable<Connection> Connections;
        public MainWindow()
        {
            InitializeComponent();

            //db = new DataConnection(options);
            db = new DataConnection("SQLite", "Data Source=Routing.db");

            //Books = db.GetTable<Book>();

            // Debug.WriteLine("Name: " + Books.DatabaseName);
            // Debug.WriteLine("Table Name: " + Books.TableName);
            //Debug.WriteLine("Name: " + Books.Count());

            Routers = db.GetTable<Router>();
            Debug.WriteLine("Routers: " + Routers.Count());

            List<Router> router_list = Routers.ToList();
            router_list.ForEach(r => Debug.WriteLine(r.Country + " " + r.Region + " " + r.City));

            Connections = db.GetTable<Connection>();
            Debug.WriteLine("Connections: " + Connections.Count());

            List<Connection> connection_list = Connections.ToList();
            connection_list.ForEach(r => Debug.WriteLine(r.Endpoint1 + " " + r.Endpoint2 + " " + r.TransmissionTime));
        }
    }

    [Table("Router")]
    public class Router
    {
        [PrimaryKey, Identity, Column("ID")]
        public int Id { get; set; }

        [Column("Country")]
        public string Country { get; set; }

        [Column("Region")]
        public int Region { get; set; }

        [Column("City")]
        public string City { get; set; }
    }

    [Table("Connection")]
    public class Connection
    {
        [PrimaryKey, Identity, Column("ID")]
        public int Id { get; set; }

        [Column("Endpoint1")]
        public int Endpoint1 { get; set; }

        [Column("Endpoint1")]
        public int Endpoint2 { get; set; }

        [Column("TransmissionTime")]
        public double TransmissionTime { get; set; }
    }
}