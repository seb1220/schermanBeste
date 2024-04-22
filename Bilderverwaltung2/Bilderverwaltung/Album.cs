using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Bilderverwaltung
{
    [Serializable]
    public class Album : INotifyPropertyChanged
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

        private ObservableCollection<Pic> _Images { get; set; }

        public ObservableCollection<Pic> Images
        {
            get { return _Images; }
            set
            {
                _Images = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Images"));
            }
        }

        public Album()
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
