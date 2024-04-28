using DataModel;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Reflection;
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

        public string InputName { get; set; } = "";
        public double InputLongitude { get; set; } = 16.209652;
        public double InputLatitude { get; set; } = 47.786898;
        public int K { get; set; } = 2;

        private List<(double x, double y, int cluster)> points;
        private List<(double x, double y)> clusters;
        public MainWindow()
        {
            InitializeComponent();

            dataConnection = new DataConnection("SQLite", "Data Source = OsterhaseDB.db");

            points = new List<(double, double, int)>();
            personList = dataConnection.GetTable<Personen>().ToList();
            foreach (Personen person in personList)
            {
                points.Add(new (GetX((double)person.Longitude), GetY((double)person.Latitude), -1));
                Console.WriteLine(person);
            }

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
            points.Clear();
            foreach (Personen person in personList)
                points.Add(new(GetX((double)person.Longitude), GetY((double)person.Latitude), -1));

            K_Means(K);

            Random random = new Random();
            List<SolidColorBrush> brushes = Enumerable.Range(0, K)
                .Select(_ => new SolidColorBrush(Color.FromRgb((byte)random.Next(0, 256), (byte)random.Next(0, 256), (byte)random.Next(0, 256))))
                .ToList();

            MapCanvas.Children.Clear();
            points.ForEach(p =>
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Width = 40;
                rectangle.Height = 40;
                rectangle.Fill = brushes[p.cluster];

                Canvas.SetLeft(rectangle, p.x * (MapCanvas.ActualWidth - 40));
                Canvas.SetTop(rectangle, p.y * (MapCanvas.ActualHeight - 40));
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
            return Math.Abs(longitude - 16.281017) / (16.281017 - 16.209652);
        }

        private double GetY (double latitude) 
        {
            return (latitude - 47.786898) / (47.846533 - 47.786898);
        }

        private void K_Means(int k)
        {
            // place initial cluster centers
            int row_width = (int)Math.Round(Math.Sqrt(k), 0);
            int rows = (int)Math.Ceiling((double)k / row_width);
            int last_row_width = k % row_width;

            clusters = new List<(double x, double y)>();
            for (int i = 0; i < k; i++)
            {
                if (i >= k - last_row_width)
                {
                    clusters.Add(new ((i - (k - last_row_width) + 1) / (double)last_row_width - (1.0 / last_row_width / 2), (i / row_width + 1) / (double)rows - (1.0 / rows / 2)));
                } else
                {
                    clusters.Add(new((i % row_width + 1) / (double)row_width - (1.0 / row_width / 2), (i / row_width + 1) / (double)rows - (1.0 / rows / 2)));
                }
            }

            // k means
            bool converged = true;
            while (converged)
            {
                converged = false;
                var oldClusters = new List<(double x, double y)>(clusters);

                for (int i = 0; i < points.Count; ++i)
                {
                    List<double> dists = clusters.Select(c => Distance(points[i].x, points[i].y, c.x, c.y)).ToList();

                    var newPoint = points[i];
                    newPoint.cluster = dists.IndexOf(dists.Min());
                    points[i] = newPoint;
                }

                for (int i = 0; i < clusters.Count; ++i)
                {
                    var cluster = clusters[i];
                    cluster.x = points.Where(p => p.cluster == i).Sum(p => p.x) / points.Where(p => p.cluster == i).Count();
                    cluster.y = points.Where(p => p.cluster == i).Sum(p => p.y) / points.Where(p => p.cluster == i).Count();

                    if (clusters[i].x != cluster.x || clusters[i].y != cluster.y)
                        converged = true;

                    clusters[i] = cluster;
                }
            }
        }

        private double Distance(double x1, double y1, double x2, double y2) 
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        private void CalculateHelpers_Click(object sender, RoutedEventArgs e)
        {
            if (K > 0 && K <= 20)
            {
                RefreshPeople();
            } else
            {
                MessageBox.Show("The Easterrabbit only has 1 - 10 helpers available");
            }
        }

        private void MapCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point p = Mouse.GetPosition(MapCanvas);
            p.X = p.X / MapCanvas.ActualWidth;
            p.Y = p.Y / MapCanvas.ActualHeight;

            // get neares cluster
            int nearest_cluster = -1;
            double dist = 2;
            for (int i = 0; i < clusters.Count; i++)
            {
                if (Distance(p.X, p.Y, clusters[i].x, clusters[i].y) < dist)
                {
                    nearest_cluster = i;
                    dist = Distance(p.X, p.Y, clusters[i].x, clusters[i].y);
                }
            }

            Console.WriteLine(clusters[nearest_cluster].x);
            Console.WriteLine(p.X + " " + p.Y);

            NearestNeigbor(p, nearest_cluster);
        }

        private void NearestNeigbor(Point p, int cluster)
        {
            List<(double x, double y, bool visited, int next)> nn_points = points.Where(p => p.cluster == cluster).Select(point =>  (point.x, point.y, false, -1)).ToList();
            nn_points.Add((p.X, p.Y, true, -1));

            int current_index = nn_points.Count - 1;

            while (nn_points.Where(point => point.visited == false).ToList().Count > 0)
            {
                var current_node = nn_points[current_index];
                double dist = 2;
                int index = -1;
                for (int j = 0; j < nn_points.Count; j++)
                {
                    if (nn_points[j].visited != true && j != current_index && Distance(current_node.x, current_node.y, nn_points[j].x, nn_points[j].y) < dist)
                    {
                        index = j;
                        dist = Distance(current_node.x, current_node.y, nn_points[j].x, nn_points[j].y);
                    }
                }

                current_node.visited = true;
                current_node.next = index;
                nn_points[current_index] = current_node;

                current_index = index;
            }

            current_index = nn_points.Count - 1;
            int next = nn_points[current_index].next;
            while (next != -1)
            {
                var current_node = nn_points[current_index];

                double y1 = Math.Abs(current_node.y);
                double y2 = Math.Abs(nn_points[current_node.next].y);

                Line line = new Line();
                line.X1 = current_node.x * MapCanvas.ActualWidth;
                line.Y1 = y1 * MapCanvas.ActualHeight;
                line.X2 = nn_points[current_node.next].x * MapCanvas.ActualWidth;
                line.Y2 = y2 * MapCanvas.ActualHeight;
                line.StrokeThickness = 3;
                line.Stroke = Brushes.Black;

                MapCanvas.Children.Add(line);

                current_index = current_node.next;
                next = nn_points[current_index].next;
            }
            
            Console.WriteLine("Drawing line");
        }
    }
}