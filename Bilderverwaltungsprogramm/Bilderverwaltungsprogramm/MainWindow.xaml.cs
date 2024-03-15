using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Bilderverwaltungsprogramm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> Albums { get; set; }
        public List<Image> Images { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Albums = new List<string>();
            Images = new List<Image>();

            this.DataContext = this;
        }

        // FILE MENU //
        private void AddAlbum_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AddAlbum_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var dialog = new NewAlbumDialog(Albums);

            // Display the dialog box and read the response
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                Albums.Add(dialog.AlbumName);
                // MessageBox.Show("Your request will be processed.");

                Directory.CreateDirectory("images/" + dialog.AlbumName);

                albumSelector.Text = dialog.AlbumName;

                foreach (string album in Albums)
                {
                    Debug.WriteLine(album);
                }
            }

        }

        private void AddPicture_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (albumSelector.SelectedValue != null)
                e.CanExecute = true;
        }

        private void AddPicture_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string zipPath = @".\result.zip";

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PDF files (.)|*.zip";
            ofd.ShowDialog();

            if (ofd.FileName.EndsWith(".zip"))
            {
                zipPath = ofd.FileName;
            }
            Debug.WriteLine("zip path: " + zipPath);

            string extractPath = "images/" + albumSelector.Text;
            Debug.WriteLine("zip extract path: " + extractPath);

            extractPath = Path.GetFullPath(extractPath);

            if (!extractPath.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
                extractPath += Path.DirectorySeparatorChar;

            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".png", StringComparison.OrdinalIgnoreCase) || entry.FullName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
                    {
                        // Gets the full path to ensure that relative segments are removed.
                        string destinationPath = Path.GetFullPath(Path.Combine(extractPath, entry.FullName));

                        // Ordinal match is safest, case-sensitive volumes can be mounted within volumes that
                        // are case-insensitive.
                        if (destinationPath.StartsWith(extractPath, StringComparison.Ordinal))
                            entry.ExtractToFile(destinationPath);
                    }
                }
            }

            updatedImages();
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

        // EDIT MENU //
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

        private void albumSelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Debug.WriteLine("selection changed");
            updatedImages();
        }

        private void updatedImages()
        {
            foreach (var file in Directory.GetFiles("images/" + albumSelector.Text + "/"))
            {
                // Debug.WriteLine(file);
                var image = new Image();
                Stream imageStreamSource = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
                PngBitmapDecoder decoder = new PngBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                BitmapSource bitmapSource = decoder.Frames[0];
                image.Source = bitmapSource;
                image.Width = 100;
                image.Height = 100;
                image.Stretch = Stretch.Fill;
                Images.Add(image);

                Debug.WriteLine("Source: " + image.Source);
                Debug.WriteLine("Name: " + image.Name);
                Debug.WriteLine("Name: ");
            }
        }
    }
}
