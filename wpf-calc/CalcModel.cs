using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_calc
{
    class CalcModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _stringParse;
        public string StringParse
        {
            get => _stringParse;
            set
            {
                _stringParse = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StringParse)));
            }
        }
        private string _resultParse;
        public string ResultParse
        {
            get => _resultParse;
            set
            {
                _resultParse = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ResultParse)));
            }
        }
    }
}