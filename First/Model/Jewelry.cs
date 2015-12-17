using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace First.Model
{
    public class Jewelry: NotifyPropertyBase
    {
        #region 字段
        private string _guid;
        private Image _image;
        private string _buyTime;
        private Double _buyPrice;
        private string _buyWho;
        private Double _goldPrice;
        private string _type;
        private string _color;
        private string _mark;
        private string _buySource;
        private string _ownWho;

        private string _state;

        private string _borrowTime;
        private string _borrowWho;
        private Double _borrowPirce;
        private string _borrowReturnTime;

        private string _saleTime;
        private string _saleWho;
        private Double _salePirce;
        private string _saleState;

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
                    NotifyPropertyChanged("Guid");
                }
            }
        }

        public Image Image
        {
            get
            {
                return _image;
            }

            set
            {
                if (_image != value)
                {
                    _image = value;
                    NotifyPropertyChanged("Image");
                }
            }
        }

        public string BuyTime
        {
            get
            {
                return _buyTime;
            }

            set
            {
                if (_buyTime != value)
                {
                    _buyTime = value;
                    NotifyPropertyChanged("BuyTime");
                }
            }
        }

        public Double BuyPrice
        {
            get
            {
                return _buyPrice;
            }

            set
            {
                if (_buyPrice != value)
                {
                    _buyPrice = value;
                    NotifyPropertyChanged("BuyPrice");
                }
            }
        }

        public string BuyWho
        {
            get
            {
                return _buyWho;
            }

            set
            {
                if (_buyWho != value)
                {
                    _buyWho = value;
                    NotifyPropertyChanged("BuyWho");
                }
            }
        }

        public double GoldPrice
        {
            get
            {
                return _goldPrice;
            }

            set
            {
                if (_goldPrice != value)
                {
                    _goldPrice = value;
                    NotifyPropertyChanged("GoldPrice");
                }
            }
        }

        public string Type
        {
            get
            {
                return _type;
            }

            set
            {
                if (_type != value)
                {
                    _type = value;
                    NotifyPropertyChanged("Type");
                }
            }
        }

        public string Color
        {
            get
            {
                return _color;
            }

            set
            {
                if (_color != value)
                {
                    _color = value;
                    NotifyPropertyChanged("Color");
                }
            }
        }

        public string Mark
        {
            get
            {
                return _mark;
            }

            set
            {
                if (_mark != value)
                {
                    _mark = value;
                    NotifyPropertyChanged("Mark");
                }
            }
        }

        public string BuySource
        {
            get
            {
                return _buySource;
            }

            set
            {
                if (_buySource != value)
                {
                    _buySource = value;
                    NotifyPropertyChanged("BuySource");
                }
            }
        }

        public string OwnWho
        {
            get
            {
                return _ownWho;
            }

            set
            {
                if (_ownWho != value)
                {
                    _ownWho = value;
                    NotifyPropertyChanged("OwnWho");
                }
            }
        }

        public string BorrowTime
        {
            get
            {
                return _borrowTime;
            }

            set
            {
                if (_borrowTime != value)
                {
                    _borrowTime = value;
                    NotifyPropertyChanged("BorrowTime");
                }
            }
        }

        public string BorrowWho
        {
            get
            {
                return _borrowWho;
            }

            set
            {
                if (_borrowWho != value)
                {
                    _borrowWho = value;
                    NotifyPropertyChanged("BorrowWho");
                }
            }
        }

        public Double BorrowPirce
        {
            get
            {
                return _borrowPirce;
            }

            set
            {
                if (_borrowPirce != value)
                {
                    _borrowPirce = value;
                    NotifyPropertyChanged("BorrowPirce");
                }
            }
        }

        public string BorrowReturnTime
        {
            get
            {
                return _borrowReturnTime;
            }

            set
            {
                if (_borrowReturnTime != value)
                {
                    _borrowReturnTime = value;
                    NotifyPropertyChanged("BorrowReturnTime");
                }
            }
        }

        public string SaleTime
        {
            get
            {
                return _saleTime;
            }

            set
            {
                if (_saleTime != value)
                {
                    _saleTime = value;
                    NotifyPropertyChanged("SaleTime");
                }
            }
        }

        public string SaleWho
        {
            get
            {
                return _saleWho;
            }

            set
            {
                if (_saleWho != value)
                {
                    _saleWho = value;
                    NotifyPropertyChanged("SaleWho");
                }
            }
        }

        public Double SalePirce
        {
            get
            {
                return _salePirce;
            }

            set
            {
                if (_salePirce != value)
                {
                    _salePirce = value;
                    NotifyPropertyChanged("SalePirce");
                }
            }
        }

        public string SaleState
        {
            get
            {
                return _saleState;
            }

            set
            {
                if (_saleState != value)
                {
                    _saleState = value;
                    NotifyPropertyChanged("SaleState");
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



        #endregion

        #region 方法
        public Jewelry()
        {
            //_guid = System.Guid.NewGuid().ToString();
            //BuyTime = System.DateTime.Now;
            //BorrowTime = System.DateTime.Now;
            //SaleTime = System.DateTime.Now;
            //BorrowReturnTime = System.DateTime.Now;
            //Image = new Image();
        }

        public Jewelry(bool isNew)
        {
            if (isNew)
            {
                _guid = System.Guid.NewGuid().ToString();
                Image = new Image();
                State = "未卖";
            }
        }

        public static Jewelry GetExample()
        {
            Jewelry je= new Jewelry();
            je.Image = new Image();
            je.Image.Source = new BitmapImage(new Uri(@"C:\xhz\ms.jpg", UriKind.Absolute));
            je.Type = "戒指";
            je.GoldPrice = 999.99;
            je.State = "未卖";
            //je.TotalWeight = 10.9;
            //je.JadeWeight = 10.8;
            //je.GoldWeight = 10.7;
            //je.ProcessFee = 10.6;
            //je.OtherFee = 10.5;
            //je.TotalPrice = 10.4;
            //je.Jade = new ObservableCollection<Jade>();
            //je.Jade.Add(new Jade("水钻", 2, 3, 4));
            //je.Jade.Add(new Jade());
            //je.Gold = new ObservableCollection<Gold>();
            //je.Gold.Add(new Gold(5, 6));
            return je;
        }
        #endregion

    }
}
