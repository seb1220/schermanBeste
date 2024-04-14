using System.Windows;

namespace PA3_UE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GameField Field { get; set; }
        private GameManager gm;
        public MainWindow()
        {
            Field = new GameField();
            GameManager gm = new GameManager(Field);

            InitializeComponent();

            SizeDispay.DataContext = Field;
            FieldComponent.DataContext = Field;
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            SizeSlider.IsEnabled = false;
            FieldComponent.SelectedIndex = -1;

            gm.StartGame(Field.Size);
        }

        private void SizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Field.ChangeSize((int)SizeSlider.Value);
        }

        private void FieldComponent_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (FieldComponent.SelectedIndex != -1)
                return;

            gm.Hit(FieldComponent.SelectedIndex);
        }
    }
}