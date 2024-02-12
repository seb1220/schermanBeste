using System.Windows;

namespace SchiffeVersenken
{
    /// <summary>
    /// Interaktionslogik für StartDialog.xaml
    /// </summary>
    public partial class StartDialog : Window
    {
        public int Columns { get; set; } = 6;
        public int Rows { get; set; } = 6;
        public int Ships5 { get; set; } = 1;
        public int Ships4 { get; set; } = 2;
        public int Ships3 { get; set; } = 3;
        public int Ships2 { get; set; } = 4;

        public StartDialog()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        private void okButton_Click(object sender, RoutedEventArgs e) =>
            DialogResult = true;
    }
}
