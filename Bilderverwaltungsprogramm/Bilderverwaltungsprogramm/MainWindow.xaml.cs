using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Bilderverwaltungsprogramm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> Albums { get; set; }
        public ObservableCollection<CustomImage> Images { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Albums = new List<string>();
            Images = new ObservableCollection<CustomImage>();

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
            ofd.Filter = "ZIP files (.)|*.zip";
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
                            entry.ExtractToFile(destinationPath, true);
                    }
                }
            }

            updatedImages();
        }

        private void MovePicture_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ImageBox.SelectedIndex >= 0)
                e.CanExecute = true;
        }

        private void MovePicture_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var dialog = new SelectAlbumDialog(Albums, (string)albumSelector.SelectedValue);

            // Display the dialog box and read the response
            bool? result = dialog.ShowDialog();

            if (result == false)
                return;

            string destination_album = dialog.Albums[dialog.SelectedAlbum];

            foreach (var image in ImageBox.SelectedItems)
            {
                if (image == null)
                    continue;

                string source_path = ((CustomImage)image).Path;
                string file_name = source_path.Split('/')[source_path.Split('/').Length - 2];
                string destination_path = "image/" + destination_album + "/" + file_name;

                Debug.WriteLine(source_path);
                Debug.WriteLine(destination_path);

                Directory.Move(source_path, destination_path);
            }

            updatedImages();
        }

        private void RemovePicture_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ImageBox.SelectedIndex >= 0)
                e.CanExecute = true;
        }

        private void RemovePicture_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            foreach (var image in ImageBox.SelectedItems)
            {
                if (image == null)
                    continue;

                Directory.Delete(((CustomImage)image).Path);
            }

            updatedImages();
        }

        // EDIT MENU //
        private void RotateLeft_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ImageBox.SelectedIndex >= 0)
                e.CanExecute = true;
        }

        private void RotateLeft_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            foreach (var srcImage in ImageBox.SelectedItems)
            {
                if (srcImage == null)
                    continue;

                CustomImage customImage = (CustomImage)srcImage;

                BitmapImage image = customImage.ImageSrc;

                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = new Uri(customImage.Path, UriKind.Relative);

                //image.BeginInit();
                // here
                image.Rotation = Rotation.Rotate270;

                //image.EndInit();
            }

            updatedImages();
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
            Images.Clear();
            foreach (var file in Directory.GetFiles("images/" + albumSelector.Text + "/"))
            {
                var names = file.Replace('.', '/').Split('/');
                var name = names[names.Length - 2];
                var bitmap = new BitmapImage(new Uri(file, UriKind.Relative));

                Images.Add(new CustomImage(file, name, bitmap));

                Debug.WriteLine(file);
                Debug.WriteLine(name);
                Debug.WriteLine(bitmap.Width);
            }
            Debug.WriteLine(Images.Count);
        }
    }
}
