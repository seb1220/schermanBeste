using System.Windows.Media.Imaging;

namespace Bilderverwaltungsprogramm
{
    public class CustomImage
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public BitmapImage ImageSrc { get; set; }

        public CustomImage(string path, string name, BitmapImage imageSource)
        {
            Path = path;
            Name = name;
            ImageSrc = imageSource;
        }
    }
}
