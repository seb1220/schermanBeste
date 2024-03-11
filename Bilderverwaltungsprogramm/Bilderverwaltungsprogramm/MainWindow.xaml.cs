using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Bilderverwaltungsprogramm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> albums;

        public MainWindow()
        {
            InitializeComponent();

            albums = new List<string>();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddAlbum_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var dialog = new NewAlbumDialog(albums);

            // Display the dialog box and read the response
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                albums.Add(dialog.AlbumName);
                // MessageBox.Show("Your request will be processed.");

                Directory.CreateDirectory("images/" + dialog.AlbumName);

                albumSelector.Text = dialog.AlbumName; // TODO

                foreach (string album in albums)
                {
                    Debug.WriteLine(album);
                }
            }
        }

        private void AddAlbum_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AddPicture_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void AddPicture_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void MovePicture_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void MovePicture_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void RemovePicture_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void RemovePicture_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void RotateLeft_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void RotateLeft_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void RotateRight_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void RotateRight_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void Rotate180_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void Rotate180_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}
