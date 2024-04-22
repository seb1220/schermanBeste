using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcChat
{
    [Serializable]
    public class Entry : INotifyPropertyChanged
    {
        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Username"));
            }
        }

        private string _calcExpression;
        public string CalcExpression
        {
            get { return _calcExpression; }
            set
            {
                _calcExpression = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CalcExpression"));
            }
        }

        private string _calcResult;

        public string CalcResult
        {
            get { return _calcResult; }
            set
            {
                _calcResult = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CalcResult"));
            }
        }

        private DateTime _time;

        public DateTime Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Time"));
            }
        }
        
        public Entry(string username, string calcExpression, string calcResult)
        {
            _username = username;
            _calcExpression = calcExpression;
            _calcResult = calcResult;
        }

        public Entry(string username, string calcExpression, string calcResult, DateTime time)
        {
            _username = username;
            _calcExpression = calcExpression;
            _calcResult = calcResult;
            _time = time;
        }

        public Entry()
        {
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

       
    }
}
