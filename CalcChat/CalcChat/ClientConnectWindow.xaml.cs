using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Vier_Gewinnt;

namespace CalcChat
{
    /// <summary>
    /// Interaktionslogik für ClientConnectWindow.xaml
    /// </summary>
    public partial class ClientConnectWindow : Window
    {
        public Server server = new Server("127.0.0.1", 25565, "");
        private MainWindow mainWindow;

        public ClientConnectWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            tbIP.DataContext = server;
            tbPort.DataContext = server;
            tbUsername.DataContext = server;
            this.mainWindow = mainWindow;
        }


        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Connecting to server " + server.IP + " : " + server.Port + " with username " + server.ClientName);
            this.Close();

            mainWindow.Title = $"CalcChat of {server.ClientName}";
            mainWindow.ConnectClient();
        }
    }
}
