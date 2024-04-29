using Microsoft.Win32;
using System.IO;
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
using static System.Net.Mime.MediaTypeNames;

namespace Geoguesser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public CustomPicture CustomImage { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            Directory.Exists(Directory.GetCurrentDirectory() + "\\images");
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\images");

            CustomImage = new CustomPicture();
            this.DataContext = CustomImage;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomPicture));
            FileStream fileStream = new FileStream("test.xml", FileMode.Create);

            xmlSerializer.Serialize(fileStream, CustomImage);

            fileStream.Flush();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "Wilde Bild file (.png)|*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                Console.WriteLine(openFileDialog.FileName);
                string filename = System.IO.Path.GetFileName(openFileDialog.FileName);
                Console.WriteLine(System.IO.Path.GetFileName(openFileDialog.FileName));
                Console.WriteLine($"{Directory.GetCurrentDirectory()}\\images\\{filename}");
                
                File.Copy(openFileDialog.FileName, $"{Directory.GetCurrentDirectory()}\\images\\{filename}", true);
            }
        }

        private void CommandBinding_Executed_1(object sender, ExecutedRoutedEventArgs e)
        {
            var files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\images\\");

            foreach(string file in files)
            {
                Console.WriteLine(file);

                CustomImage.Name = System.IO.Path.GetFileNameWithoutExtension(file);
                CustomImage.SourcePath = file;
                // CustomImage.bitMap = (BitmapImage)BitmapImage.FromFile(file);
            }
        }
    }
}