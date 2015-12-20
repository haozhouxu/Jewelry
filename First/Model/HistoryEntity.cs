using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First.Model
{
    public class HistoryEntity : NotifyPropertyBase
    {
        #region 字段
        private string _guid;
        private string _state;
        private string _time;
        private string _who;
        private double _price;
        private string _returntime;

        #endregion

        #region 属性

        public string Guid
        {
            get
            {
                return _guid;
            }
            set
            {
                if (_guid != value)
                {
                    _guid = value;
                    NotifyPropertyChanged("BuyTime");
                }
            }
        }

        public string State
        {
            get
            {
                return _state;
            }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    NotifyPropertyChanged("State");
                }
            }
        }

        public string Time
        {
            get
            {
                return _time;
            }

            set
            {
                if (_time != value)
                {
                    _time = value;
                    NotifyPropertyChanged("Time");
                }
            }
        }

        public string Who
        {
            get
            {
                return _who;
            }
            set
            {
                if (_who != value)
                {
                    _who = value;
                    NotifyPropertyChanged("Who");
                }
            }
        }

        public Double Price
        {
            get
            {
                return _price;
            }

            set
            {
                if (_price != value)
                {
                    _price = value;
                    NotifyPropertyChanged("Price");
                }
            }
        }

        public string Returntime
        {
            get
            {
                return _returntime;
            }

            set
            {
                if (_returntime != value)
                {
                    _returntime = value;
                    NotifyPropertyChanged("Returntime");
                }
            }
        }

        #endregion

    }
}
