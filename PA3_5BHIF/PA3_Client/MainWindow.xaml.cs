using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace PA3_Client
{

    public class Cell : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        private int _value = -1;
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged("Value");
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

        public int Rows { set; get; }
        public int Cols { set; get; }
        public int Mines { set; get; }

        public int GameNumber { set; get; }

        private Transfer client;

        public ObservableCollection<FieldStatus> field = new ObservableCollection<FieldStatus>();

        public MainWindow()
        {
            this.DataContext = this;
            InitializeComponent();

            client = new Transfer(new TcpClient("127.0.0.1", 12345), new RecieverImpl(this));

            client.Start();



            
            if (File.Exists("config.xml"))
            {

                XmlSerializer serializer = new XmlSerializer(typeof(SaveData));

                
                FileStream stream = new FileStream("config.xml", FileMode.Open);


                SaveData data = (SaveData) serializer.Deserialize(stream);
                stream.Close();

                Cols = data.Cols;
                Rows = data.Rows;
                Mines = data.Mines;
                
                field = data.Fields;
                lbField.ItemsSource = field;
                GameNumber = data.GameNumber;


                MSG msg = new MSG();

                msg.Type = MessageType.NEW;
                msg.Config = new Config { GameNumber = this.GameNumber, Width = Cols, Height = Rows, Mines = this.Mines };

                Debug.WriteLine(msg.Config.GameNumber);
                Debug.WriteLine(msg.Config.Width);
                Debug.WriteLine(msg.Config.Height);
                Debug.WriteLine(msg.Config.Mines);


                client.Send(msg);

                File.Delete("config.xml");

            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            field = new ObservableCollection<FieldStatus>();

            lbField.ItemsSource = field;
            Rows = int.Parse(tbHeight.Text);
            Cols = int.Parse(tbWidth.Text);
            int mines = int.Parse(tbMinen.Text);
            //Redundant :(
            Mines = mines;

            for (int i = 0; i < Rows * Cols; i++)
                field.Add(new FieldStatus { Value = -1 });

            lbField.Items.Refresh();

            MSG msg = new MSG();

            
            msg.Type = MessageType.NEW;
            msg.Config = new Config { Width = Cols, Height = Rows, Mines = mines };

            client.Send(msg);

        }

        private void lbField_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lbField.SelectedIndex == -1)
            {
                return;
            }
            FieldStatus status = field[lbField.SelectedIndex];

            if (status.Value != -1)
                return;


            int index = lbField.SelectedIndex;

            int x = index % Cols;
            int y = index / Rows;

            MSG msg = new MSG();
            msg.Type = MessageType.PICK;
            msg.Pick = new Pick { X = x, Y = y };


            client.Send(msg);

        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (GameNumber == 0)
                return;
            XmlSerializer serializer = new XmlSerializer(typeof(SaveData));

            SaveData saveData = new SaveData();
            saveData.Fields = this.field;
            saveData.Rows = Rows;
            saveData.Cols = Cols;
            saveData.Mines = Mines;
            saveData.GameNumber = GameNumber;

            FileStream stream = new FileStream("config.xml", FileMode.Create);


            serializer.Serialize(stream, saveData);

            stream.Close();

        }
    }

    public class SaveData
    {

        public ObservableCollection<FieldStatus> Fields { get; set; }
        public int Rows { get; set; }
        public int Mines { get; set; }
        public int Cols { get; set; }
        public int GameNumber { get; set; }


    }
    public class FieldStatus
    {
        public int Value { get; set; }

        [XmlIgnore]
        public string ShowValue { private set { } get
            {
                return Value == -1 ? " " : Value.ToString();
            } 
        }

        public FieldStatus()
        {
            Value = -1;
        }

    }

}
