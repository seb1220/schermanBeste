using GraphControl;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace IP_Graph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataConnection db;

        List<Router> Routers;
        List<Connection> Connections;
        Dictionary<int, List<(int to, double dist)>> searchConns;
        public MainWindow()
        {
            InitializeComponent();

            db = new DataConnection("SQLite", "Data Source=Routing.db");

            var router_table = db.GetTable<Router>();
            //Debug.WriteLine("Routers: " + Routers.Count());
            Routers = router_table.ToList();
            //router_list.ForEach(r => Debug.WriteLine(r.Country + " " + r.Region + " " + r.City));

            var connection_table = db.GetTable<Connection>();
            //Debug.WriteLine("Connections: " + Connections.Count());
            Connections = connection_table.ToList();
            //connection_list.ForEach(r => Debug.WriteLine(r.Endpoint1 + " " + r.Endpoint2 + " " + r.TransmissionTime));

            foreach (Router router in Routers)
            {
                Node node = new Node();
                node.Text = router.Id.ToString();
                // node.Colors.Add(Colors.Red);
                GraphComponent.Nodes.Add(node);
            }

            foreach (Connection conn in Connections)
            {
                Edge edge = new Edge();
                edge.First = GraphComponent.Nodes.Where(n => n.Text.Equals(conn.Endpoint1.ToString())).First();
                edge.Second = GraphComponent.Nodes.Where(n => n.Text.Equals(conn.Endpoint2.ToString())).First();

                //Debug.WriteLine("Node1: " + GraphComponent.Nodes.Where(n => n.Text.Equals(conn.Endpoint1.ToString())).First().Text);
                //Debug.WriteLine("Node2: " + GraphComponent.Nodes.Where(n => n.Text.Equals(conn.Endpoint2.ToString())).First().Text);

                edge.Text = conn.TransmissionTime.ToString();
                GraphComponent.Edges.Add(edge);
            }

            searchConns = new Dictionary<int, List<(int to, double dist)>>();
            foreach (Router router in Routers)
            {
                searchConns[router.Id] = new List<(int to, double dist)>();
                foreach (Connection conn in Connections)
                {
                    if (conn.Endpoint1 == router.Id)
                    {
                        searchConns[router.Id].Add((conn.Endpoint2, conn.TransmissionTime));
                    }
                    else if (conn.Endpoint2 == router.Id)
                    {
                        searchConns[router.Id].Add((conn.Endpoint1, conn.TransmissionTime));
                    }
                }
            }
        }

        private void GraphComponent_EndChanged(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("EndChanged");

            if (GraphComponent.Start != null && GraphComponent.End != null)
            {
                RunDijkstra();

                GraphComponent.Start = null;
                GraphComponent.End = null;
            }
        }

        private void GraphComponent_StartChanged(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("StartChanged");

            if (GraphComponent.Start != null && GraphComponent.End != null)
            {
                RunDijkstra();

                GraphComponent.Start = null;
                GraphComponent.End = null;
            }
        }

        private void RunDijkstra()
        {
            Random r = new Random();
            Color RndColor = Color.FromRgb((Byte)r.Next(0, 256), (Byte)r.Next(0, 256), (Byte)r.Next(0, 256));

            int start = int.Parse(GraphComponent.Start.Text);
            int end = int.Parse(GraphComponent.End.Text);

            var searchNodes = new Dictionary<int, (int pre, double dist, bool finished)>();
            foreach (var router in Routers)
                searchNodes[router.Id] = (-1, 9999999, false);

            var queue = new PriorityQueue<(int, double), double>(); // (node, dits), dist
            queue.Enqueue((start, 0), 0);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                int current_node_id = current.Item1;
                var current_node = searchNodes[current_node_id];
                double current_node_dist = current.Item2;


                if (current_node_id == end)
                    break; // finished

                foreach (var conn in searchConns[current_node_id])
                {
                    if (searchNodes[conn.to].finished)
                        continue;

                    if (searchNodes[conn.to].dist <= current_node_dist + conn.dist)
                        continue;

                    double new_dist = current_node_dist + conn.dist;
                    searchNodes[conn.to] = (current_node_id, new_dist, false);
                    queue.Enqueue((conn.to, new_dist), new_dist);
                }

                current_node.finished = true;
                searchNodes[current_node_id] = current_node;

            }

            int id = end;
            while (id != -1)
            {
                var node = GraphComponent.Nodes.Where(n => n.Text.Equals(id.ToString())).First();
                node.Colors.Add(RndColor);
                id = searchNodes[id].pre;
            }
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

        [Column("Endpoint2")]
        public int Endpoint2 { get; set; }

        [Column("TransmissionTime")]
        public double TransmissionTime { get; set; }
    }
}