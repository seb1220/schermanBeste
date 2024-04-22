using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace Bilderverwaltung
{
    [Serializable]
    public class Pic : INotifyPropertyChanged
    {
        private string _Name { get; set; }

        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Name"));
            }
        }
        private string _Path { get; set; }

        public string Path
        {
            get { return _Path; }
            set
            {
                _Path = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Path"));
            }
        }

        [XmlIgnore]
        private ImageSource _template { get; set; }

        [XmlIgnore]
        public ImageSource Template
        {
            get { return _template; }
            set
            {
                _template = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Template"));
                OnPropertyChanged(new PropertyChangedEventArgs("Path"));
            }
        }

        public Pic(string name, string path)
        {
            Name = name;
            Path = path;
            Template = BitmapFrame.Create(new Uri("file://" + path), BitmapCreateOptions.None, BitmapCacheOption.None);
        }

        public Pic()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

    }
}
