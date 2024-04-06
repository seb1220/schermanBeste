using System.Collections.Generic;
using System.Windows;

namespace Bilderverwaltungsprogramm
{
    /// <summary>
    /// Interaktionslogik für SelectAlbumDialog.xaml
    /// </summary>
    public partial class SelectAlbumDialog : Window
    {
        public List<string> Albums { get; set; }

        public int SelectedAlbum { get; set; }

        public SelectAlbumDialog(List<string> albums, string selectedAlbum)
        {
            InitializeComponent();

            albums.Remove(selectedAlbum);
            Albums = albums;

            this.DataContext = this;
        }

        private void okButton_Click(object sender, RoutedEventArgs e) =>
            DialogResult = true;

        private void cancelButton_Click(object sender, RoutedEventArgs e) =>
            DialogResult = false;

        private void AlbumSelection_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            okButton.IsEnabled = true;
        }
    }
}
