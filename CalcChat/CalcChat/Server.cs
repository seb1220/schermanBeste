using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcChat
{
    public class Server : INotifyPropertyChanged
    {
        int _port;
        public int Port
        {
            get { return _port; }
            set
            {
                _port = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Port"));
            }
        }

        string _ip;
        public string IP
        {
            get { return _ip; }
            set
            {
                _ip = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IP"));
            }
        }

        string _clientName;
        public string ClientName
        {
            get { return _clientName; }
            set
            {
                _clientName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ClientName"));
            }
        }

        public Server(string ip, int port, string clientName)
        {
            _ip = ip;
            _port = port;
            _clientName = clientName;
        }
        // event for property change
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

    }
}
