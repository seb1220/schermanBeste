using System.Collections.ObjectModel;
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

namespace WpfChat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string username;
        public string password;
        public bool isLogin;

        private NetworkManager networkManager;

        public ObservableCollection<Chat> Chats;
        public ObservableCollection<string> AvailableChats;
        public MainWindow()
        {
            AvailableChats = new ObservableCollection<string>();
            Chats = new ObservableCollection<Chat>();

            Login login = new Login(this);
            login.ShowDialog();

            Console.WriteLine("Username: " + username);
            Console.WriteLine("Password: " + password);

            networkManager = new NetworkManager(this, username, password, isLogin);

            InitializeComponent();

            AvailableChatsBox.DataContext = AvailableChats;
            ChatTabs.DataContext = Chats;
        }

        private void NewMessage_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("New message: " + SendMessageConent.Text + " to " + Chats[ChatTabs.SelectedIndex].Name);
            networkManager.SendMessage(Chats[ChatTabs.SelectedIndex].Name, SendMessageConent.Text);
        }

        private void NewChat_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("New chat: " + NewChatName.Text);
            networkManager.CreateRoom(NewChatName.Text);
        }

        private void EnterChat_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Joining Chat: " + AvailableChatsBox.SelectedIndex);
            if (AvailableChatsBox.SelectedIndex >= 0)
            {
                Console.WriteLine("Joining Chat: " + AvailableChats[AvailableChatsBox.SelectedIndex]);
                networkManager.JoinRoom(AvailableChats[AvailableChatsBox.SelectedIndex]);
            }
        }
    }
}