using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using wpf_calc;

namespace wpf_calc
{
    class CalcViewModel : INotifyPropertyChanged
    {
        private static Parser _parser = new Parser();

        private static CalcModel _calcModel = new CalcModel();

        public event PropertyChangedEventHandler PropertyChanged;
        private readonly Dictionary<string, List<string>> _errorsByPropertyName = new Dictionary<string, List<string>>();

        CommandBinding commandBinding = new CommandBinding();


        public string ParseStr
        {
            get => _calcModel.StringParse;
            set
            {
                if (_calcModel.StringParse != value)
                {
                    _calcModel.StringParse = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ParseStr)));
                }
            }
        }
        public RelayCommand StartParsing
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    string result = _parser.Parse(ParseStr);
                    ParseStr = _parser.Parse(ParseStr);
                });
            }
        }

        public RelayCommand AddOper
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                        char[] pattern = { '+', '-', '/', '*' };
                        string operation = obj as string;
                        if(ParseStr is null){
                            if(operation == "-"){
                                ParseStr += operation;
                            }
                        }

                        else if ("+-/*,".Contains(ParseStr[ParseStr.Length - 1].ToString()))
                        {
                            return;
                        }
                        else
                            ParseStr += operation;
                  });
            }
        }

        public RelayCommand ClearStr
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                        ParseStr = "";
                  });
            }
        }

        public RelayCommand Backspace
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                        if(ParseStr != null && ParseStr != "")
                            ParseStr = ParseStr.Remove(ParseStr.Length - 1);
                  });
            }
        }

        public RelayCommand AddNumber
        {
            get
            {
                return new RelayCommand(obj =>
                  {
                      string number = obj as string;

                      if (ParseStr != "0")
                          ParseStr += number;
                      else
                        ParseStr = number;
                  });
            }
        }

    }
}