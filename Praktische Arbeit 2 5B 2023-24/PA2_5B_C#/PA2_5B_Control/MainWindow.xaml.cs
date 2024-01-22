using PA2_5B;
using System.Collections.Generic;
using System.Windows;

namespace PA2_5B_Control
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Beispielcode und -zeichnung, kann entfernt werden
            code.Text = "TURN RIGHT 45\r\nDRAW 50\r\nTURN RIGHT 30\r\nDRAW 50\r\nTURN LEFT 30\r\nDRAW 50";
            painter.Rotate(45);
            painter.Draw(50);
            painter.Rotate(30);
            painter.Draw(50);
            painter.Rotate(-30);
            painter.Draw(50);
        }

        private void draw_Click(object sender, RoutedEventArgs e)
        {
            List<Token> tokens = new List<Token>();

            Interpreter interpreter = new Interpreter(tokens);

            interpreter.Parse();

            interpreter.Execute(painter);
        }
    }
}
