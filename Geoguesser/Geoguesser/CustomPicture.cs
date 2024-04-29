using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Geoguesser
{
    public class CustomPicture : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get => _name; 
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }
        // public BitmapImage bitMap { get; set; }

        private string _path;
        public string SourcePath { get => _path; set
            {
                _path = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SourcePath)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
