using CalculatorWPF;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Vier_Gewinnt;
using System.IO;
using System.Xml.Serialization;

namespace CalcChat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, Receiver
    {
        ClientConnectWindow clientConnectWindow;
        Server clientInfo;
        ObservableCollection<Entry> entries = new ObservableCollection<Entry>();

        //Network
        public static List<Transfer> transferList = new List<Transfer>();
        Transfer clientTransfer;

        private static bool serverRunning = false;

        // XML-Serializer
        static XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Entry>));
        static string projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

        public MainWindow()
        {
            InitializeComponent();
            
            // Check whether Server is already running
            if (!serverRunning)
            {
                StartServer();
                serverRunning = true;
            }

            clientConnectWindow = new ClientConnectWindow(this);
            clientConnectWindow.Show();
            clientInfo = clientConnectWindow.server;
            this.Title = $"CalcChat of {clientInfo.ClientName}";
            lbHistory.ItemsSource = entries;
        }

        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            string input = tbExpression.Text;
            /*if (input == string.Empty)
            {
                MessageBox.Show("Please enter an expression");
                return;
            }

            List<Token> tokens = Tokenizer.Tokenize(input);
            Parser parser = new Parser(tokens);
            var exp = parser.Parse();

            List<string> variables = new List<string>();
            foreach (Token t in tokens)
            {
                if (t.type == Token.Type.Variable)
                {
                    variables.Add(t.Text);
                }
            }
            // ask for values of variables
            Dictionary<string, double> values = new Dictionary<string, double>();

            foreach (string s in variables)
            {
                //inputbox should be displayed in the middle of the screen
                string value = Microsoft.VisualBasic.Interaction.InputBox
                    ("Please enter a value for " + s,
                    "Value for " + s, "0", (int)SystemParameters.PrimaryScreenWidth / 2,
                    (int)SystemParameters.PrimaryScreenHeight / 2);
                values.Add(s, double.Parse(value));
            }

            var result = exp.Evaluate(values);

            Entry entry = new Entry(clientInfo.ClientName, input, result.ToString());
            entries.Add(entry);
            
            Debug.WriteLine(clientInfo.ClientName);
            Debug.WriteLine(5%3);
            */

            MSG msg = new MSG
            {
                Type = MSG.MSGType.CalcRequest,
                ClientName = clientInfo.ClientName,
                Expression = input
            };

            clientTransfer.Send(msg);
            tbExpression.Text = string.Empty;

        }

        public void StartServer()
        {
            Thread serverThread = new Thread(CreateServer);
            serverThread.IsBackground = true;
            serverThread.Start();
        }

        private void CreateServer()
        {
            TcpListener tcpListener = null;
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, 25565);
                tcpListener.Start();

                //Clients annehmen
                while (true)
                {
                    TcpClient client = tcpListener.AcceptTcpClient();
                    Transfer transfer = new Transfer(client, new ServerReciever());
                    transfer.Start();
                    transferList.Add(transfer);
                    /*if (transferList.Count > 1)
                    {
                        transfer.Send(new MSG { Type = MSG.MSGType.Configruation });
                    }*/
                    MessageBox.Show("Client connected!");
                }

            }
            catch (SocketException ex)
            {
                MessageBox.Show("Connection to Client failed!: " + ex.Message);
            }
        }

        public void AddDebugInfo(Transfer t, string m, bool sent)
        {
            Debug.Write("Message was sent or received!");
        }

        public void ReceiveMessage(MSG m, Transfer t)
        {
            if (m.Type == MSG.MSGType.CalcResponse)
            { 
                Entry entry = new Entry(m.ClientName, m.Expression, m.Result, m.Time);
                entries.Add(entry);
                lbHistory.Items.Refresh();
            }
        }

        public void TransferDisconnected(Transfer t)
        {
            t.Stop();
        }

        public void ConnectClient()
        {
            // client starten
            TcpClient client = new TcpClient();
            try
            {
                client.Connect(clientInfo.IP, clientInfo.Port);
                clientTransfer = new Transfer(client, this);
                clientTransfer.Start();
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Connection to Server failed!: " + ex.Message);
            }
        }

        // Save CanExecute-Handler
        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = lbHistory.Items.Count > 0;
        }

        // Save Executed-Handler
        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // List of entries from lb
            ObservableCollection<Entry> entries = new ObservableCollection<Entry>();
            entries = (ObservableCollection<Entry>)lbHistory.ItemsSource;

            MessageBox.Show($"Saving entries to ChatHistory.xml with {entries.Count} elements!");
            SerializeObject(entries);
        }

        // Open CanExecute-Handler
        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        // Open Executed-Handler
        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Add entries at the beginning of the listbox
            ObservableCollection<Entry> entries = DeserializeObject();
            foreach (Entry entry in entries)
            {
                this.entries.Insert(0, entry);
            }
        }

        // Objekt serialisieren
        public static void SerializeObject(object obj)
        {
            using (FileStream stream = new FileStream($"{projectFolder}\\ChatHistory.xml", FileMode.Create))
            {
                serializer.Serialize(stream, obj);
            }
        }

        // Objekt deserialisieren
        public static ObservableCollection<Entry> DeserializeObject()
        {
            using (FileStream stream = new FileStream($"{projectFolder}\\ChatHistory.xml", FileMode.Open))
            {
                return (ObservableCollection<Entry>)serializer.Deserialize(stream);
            }
        }

    }

    public static class CustomCommands
    {
        public static readonly RoutedUICommand Save = new RoutedUICommand
            (
                "Save",
                "Save",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.S, ModifierKeys.Control)
                }
            );

        public static readonly RoutedUICommand Open = new RoutedUICommand
            (
                "Open",
                "Open",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.O, ModifierKeys.Control)
                }
            );
    }
}