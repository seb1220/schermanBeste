using System;
using System.Collections.Generic;
using System.Linq;
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

namespace WpfChat
{
    /// <summary>
    /// Interaktionslogik für Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        MainWindow mainWindow;
        public Login(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.username = LoginUserName.Text;
            mainWindow.password = LoginPassword.Password;
            mainWindow.isLogin = true;

            this.Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (!RegisterPassword.Password.Equals(RegisterConfirmPassword.Password))
            {
                MessageBox.Show("The passwords are not the same!");
                return;
            }

            mainWindow.username = RegisterUserName.Text;
            mainWindow.password = RegisterPassword.Password;
            mainWindow.isLogin = false;

            this.Close();
        }
    }
}
