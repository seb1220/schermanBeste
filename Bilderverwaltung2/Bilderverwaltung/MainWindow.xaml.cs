using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
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
using System.Xml.Serialization;
using Path = System.IO.Path;


namespace Bilderverwaltung
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly static string imageFolder = $"{Directory.GetCurrentDirectory()}\\Images";
        public ObservableCollection<Album> Albums { get; set; } = new ObservableCollection<Album>();
        static XmlSerializer serializer;
        static FileStream stream;
        public MainWindow()
        {
            InitializeComponent();
            serializer = new XmlSerializer(typeof(ObservableCollection<Album>));
            if (File.Exists($"{imageFolder}\\ProgramData.xml"))
            {
                Albums = DeserializeObject();
            }

            foreach (Album album in Albums)
            {
                foreach (Pic pic in album.Images)
                {
                    pic.Template = BitmapFrame.Create(new Uri(pic.Path), BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                    Debug.WriteLine(pic.Path);
                }
            }
            
            UpdateComboBoxAlbum();
            this.DataContext = this;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            SerializeObject(Albums);
            base.OnClosing(e);
        }


        // Für das "Neues Album"
        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = imageFolder;
            
            if (saveFileDialog.ShowDialog() == true)
            {
                if (!Directory.Exists(saveFileDialog.FileName))
                {
                    var directory = saveFileDialog.FileName;
                    Directory.CreateDirectory(directory);
                }
                else
                {
                    MessageBox.Show("Album already exists", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            UpdateComboBoxAlbum();
        }

        // Für das "Hinzufügen von Bildern"
        private void HinzufuegenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void HinzufuegenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string currentAlbum = cbAlbum.Text;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Zip Files (*zip)|*.zip";
            openFileDialog.InitialDirectory = imageFolder;

            if (openFileDialog.ShowDialog() == true)
            {
                string zipPath = openFileDialog.FileName;
                string extractPath = Directory.GetCurrentDirectory() + @"\Images\" + currentAlbum;

                // foreach file in the zip file, extract it to the current album
                using (ZipArchive archive = ZipFile.OpenRead(zipPath))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        // Den Namen der Datei extrahieren
                        string entryFileName = Path.GetFileName(entry.FullName);

                        // Den vollständigen Pfad zum Zielordner erstellen
                        string destinationPath = Path.Combine(extractPath, entryFileName);

                        // Die Datei extrahieren
                        entry.ExtractToFile(destinationPath, true);
                    }
                }
            }
            // Album welchem Bilder hinzugefügt wurden, aktualisieren indem diesem speziellen Album die Bilder hinzugefügt werden
            Album album = Albums.First(a => a.Name == currentAlbum);
            string albumPath = Path.Combine(imageFolder, currentAlbum);

            if (album != null)
            {
                foreach (string file in Directory.GetFiles(albumPath))
                {
                    Pic newPic = new Pic(Path.GetFileNameWithoutExtension(file), file);
                    album.Images.Add(newPic);
                }
            }
            

        }

        // Für das "Bilder im Album verschieben"
        private void VerschiebenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (cbAlbumDestination.SelectedItem != null && listboxPic.SelectedItems.Count > 0)
                e.CanExecute = true;
        }

        private void VerschiebenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedImages = listboxPic.SelectedItems.Cast<Pic>().ToList();
            var selectedAlbum = cbAlbumDestination.SelectedItem as Album;
            var currentAlbum = cbAlbum.SelectedItem as Album;

            foreach (Pic pic in selectedImages)
            {
                string destinationPath = Path.Combine(imageFolder, selectedAlbum.Name, pic.Name + ".png");
                try 
                {
                    File.Move(pic.Path, destinationPath);
                    currentAlbum.Images.Remove(pic);
                    selectedAlbum.Images.Add(pic);
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }

            // Update the listbox
            listboxPic.ItemsSource = currentAlbum.Images;
        }

        // Für das "Bilder löschen"
        private void DeleteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (listboxPic.SelectedItems.Count > 0)
                e.CanExecute = true;
        }

        private void DeleteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedImages = listboxPic.SelectedItems.Cast<Pic>().ToList();
            foreach (Pic pic in selectedImages)
            {
                try
                {
                    File.Delete(pic.Path);
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            // Update the listbox
            Album album = Albums.First(a => a.Name == cbAlbum.Text);
            foreach (Pic pic in selectedImages)
            {
                album.Images.Remove(pic);
            }
        }

        // Für das "90° im Uhrzeigersinn rotieren"
        private void RotateImUhrCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void RotateImUhrCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // rotate all choosen images 90° clockwise
            var selectedImages = listboxPic.SelectedItems.Cast<Pic>().ToList();
            foreach (Pic pic in selectedImages)
            {
                TransformedBitmap transformedBitmap = new TransformedBitmap((BitmapSource)pic.Template, new RotateTransform(90));
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(transformedBitmap));

                using (FileStream stream = new FileStream(pic.Path, FileMode.Create))
                {
                    try
                    {
                        encoder.Save(stream);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }

            // Update the listbox and before that the album
            Album album = Albums.First(a => a.Name == cbAlbum.Text);
            foreach (Pic pic in album.Images)
            {
                pic.Template = BitmapFrame.Create(new Uri("file://" + pic.Path), BitmapCreateOptions.None, BitmapCacheOption.None);
                  

                // q: does this work?
            }
            listboxPic.ItemsSource = album.Images;
        }

        // Für das "90° gegen den Uhrzeigersinn rotieren"
        private void RotateGeUhrCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void RotateGeUhrCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedImages = listboxPic.SelectedItems.Cast<Pic>().ToList();
            foreach (Pic pic in selectedImages)
            {
                TransformedBitmap transformedBitmap = new TransformedBitmap((BitmapSource)pic.Template, new RotateTransform(-90));
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(transformedBitmap));

                using (FileStream stream = new FileStream(pic.Path, FileMode.Create))
                {
                    try
                    {
                        encoder.Save(stream);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        // Für das "180° Rotieren"
        private void RotateCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void RotateCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var selectedImages = listboxPic.SelectedItems.Cast<Pic>().ToList();
            foreach (Pic pic in selectedImages)
            {
                TransformedBitmap transformedBitmap = new TransformedBitmap((BitmapSource)pic.Template, new RotateTransform(180));
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(transformedBitmap));

                using (FileStream stream = new FileStream(pic.Path, FileMode.Create))
                {
                    try
                    {
                        encoder.Save(stream);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void UpdateComboBoxAlbum()
        {
            //cbAlbum.Items.Clear();
            /*foreach (string album in Directory.GetDirectories(Directory.GetCurrentDirectory() + @"\Images"))
            {
                // add only the name of the directory to the combobox
                cbAlbum.Items.Add(album.Substring(album.LastIndexOf('\\') + 1));
            }*/

            Albums.Clear();
            foreach (string album in Directory.GetDirectories(imageFolder))
            {
                Album newAlbum = new Album();
                newAlbum.Name = album.Substring(album.LastIndexOf('\\') + 1);
                newAlbum.Images = new ObservableCollection<Pic>();

                foreach (string file in Directory.GetFiles(album))
                {
                    Pic newPic = new Pic(Path.GetFileNameWithoutExtension(file), file);
                    //newPic.Name = Path.GetFileNameWithoutExtension(file);
                    //newPic.Path = file;
                    newAlbum.Images.Add(newPic);
                }

                Albums.Add(newAlbum);
            }   

        }

        /*private void cbAlbum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string directory = Directory.GetCurrentDirectory() + @"\Images\" + cbAlbum.Text;
            Debug.WriteLine(directory);

            directory = Path.Combine(directory, cbAlbum.Text);
            Debug.WriteLine(cbAlbum.Text);
            // why is cbAlbum.Text empty?

            if (Directory.Exists(directory))
            {
                listboxPic.Items.Clear();
                foreach (string file in Directory.GetFiles(directory))
                {
                    listboxPic.Items.Add(file.Substring(file.LastIndexOf('\\') + 1));
                }
            }

        }*/

        private void cbAlbum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbAlbum.SelectedItem is Album selectedAlbum)
            {
                listboxPic.ItemsSource = selectedAlbum.Images;
            }
            else
            {
                listboxPic.ItemsSource = null;
            }
        }

        // Objekt serialisieren
        public static void SerializeObject(object obj)
        {
            using (FileStream stream = new FileStream($"{imageFolder}\\ProgramData.xml", FileMode.Create))
            {
                serializer.Serialize(stream, obj);
            }
        }

        // Objekt deserialisieren
        public static ObservableCollection<Album> DeserializeObject()
        {
            using (FileStream stream = new FileStream($"{imageFolder}\\ProgramData.xml", FileMode.Open))
            {
                return (ObservableCollection<Album>)serializer.Deserialize(stream);
            }
        }


    }



    public static class CustomCommands
    {
        // Fuer new bzw. "Neues Album"
        public static readonly RoutedUICommand New = new RoutedUICommand
            (
                "New",
                "New",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.N, ModifierKeys.Control)
                }
            );

        // Für Find bzw. "Bilder hinzufügen"
        public static readonly RoutedUICommand Hinzufuegen = new RoutedUICommand
            (
                "Hinzufuegen",
                "Hinzufuegen",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.H, ModifierKeys.Control)
                }
            );

        // Für Replace bzw. "Bilder in Album verschieben"
        public static readonly RoutedUICommand Verschieben = new RoutedUICommand
            (
                "Verschieben",
                "Verschieben",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.V, ModifierKeys.Control)
                }
            );

        // Für Replace bzw. "Bilder löschen"
        public static readonly RoutedUICommand Delete = new RoutedUICommand
            (
                "Delete",
                "Delete",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.D, ModifierKeys.Control)
                }
            );

        // Für RotateImUhr bzw. "90° im Uhrzeigersinn rotieren"
        public static readonly RoutedUICommand RotateImUhr = new RoutedUICommand
            (
                "RotateImUhr",
                "RotateImUhr",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                        new KeyGesture(Key.I, ModifierKeys.Control)
                }
                // Add icon for the command here for rotating the image 90° clockwise


                //new Image
                //{
                //    Source = new BitmapImage(new Uri("Images/rotate.png", UriKind.Relative))
                //}

            );

        // Für RotateImUhr bzw. "90° im Uhrzeigersinn rotieren"
        public static readonly RoutedUICommand RotateGeUhr = new RoutedUICommand
            (
                "RotateGeUhr",
                "RotateGeUhr",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                            new KeyGesture(Key.G, ModifierKeys.Control)
                }
            );

        // Für RotateImUhr bzw. "90° im Uhrzeigersinn rotieren"
        public static readonly RoutedUICommand Rotate = new RoutedUICommand
            (
                "Rotate",
                "Rotate",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                            new KeyGesture(Key.R, ModifierKeys.Control)
                }
            );

    }
    /*public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Command CanExecuteNewAlbumCommand
        private void CanExecuteNewAlbumCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        // Command ExecutedNewAlbumCommand
        private void ExecutedNewAlbumCommand(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("New Album");
        }

        
    }*/
}