using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ManagementSystem.Models
{
    public class Jewelry:INotifyPropertyChanged
    {
        #region 字段
        private string guid;
        private Image image;
        private double totalWeight;
        private double jadeWeight;
        private double goldWeight;
        private ObservableCollection<Gold> gold;
        private ObservableCollection<Jade> jade;
        private double processFee;
        private double otherFee;
        private double totalPrice;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region 属性
        public string Guid
        {
            get
            {
                return guid;
            }
            set
            {
                guid = value;
                NotifyPropertyChanged("Guid");
            }
        }
        public Image Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                NotifyPropertyChanged("Image");
            }
        }
        public double TotalWeight
        {
            get
            {
                return totalWeight;
            }
            set
            {
                totalWeight = value;
                NotifyPropertyChanged("TotalWeight");
            }
        }
        public double JadeWeight
        {
            get
            {
                return jadeWeight;
            }
            set
            {
                jadeWeight = value;
                NotifyPropertyChanged("JadeWeight");
            }
        }
        public double GoldWeight
        {
            get
            {
                return goldWeight;
            }
            set
            {
                goldWeight = value;
                NotifyPropertyChanged("GoldWeight");
            }
        }
        public double ProcessFee
        {
            get
            {
                return processFee;
            }
            set
            {
                processFee = value;
                NotifyPropertyChanged("ProcessFee");
            }
        }
        public double OtherFee
        {
            get
            {
                return otherFee;
            }
            set
            {
                otherFee = value;
                NotifyPropertyChanged("OtherFee");
            }
        }
        public double TotalPrice
        {
            get
            {
                return totalPrice;
            }
            set
            {
                totalPrice = value;
                NotifyPropertyChanged("TotalPrice");
            }
        }
        public ObservableCollection<Jade> Jade
        {
            get
            {
                return jade;
            }
            set
            {
                jade = value;
                NotifyPropertyChanged("Jade");
            }
        }
        public ObservableCollection<Gold> Gold
        {
            get
            {
                return gold;
            }
            set
            {
                gold = value;
                NotifyPropertyChanged("Gold");
            }
        }
        #endregion

        #region 方法
        public Jewelry()
        {
            
        }

        public static Jewelry GetExample()
        {
            Jewelry je= new Jewelry();
            je.Guid = System.Guid.NewGuid().ToString();
            je.Image = new Image();
            je.image.Source = new BitmapImage(new Uri(@"C:\xhz\Koala.jpg", UriKind.Absolute));
            je.TotalWeight = 10.9;
            je.JadeWeight = 10.8;
            je.GoldWeight = 10.7;
            je.ProcessFee = 10.6;
            je.OtherFee = 10.5;
            je.TotalPrice = 10.4;
            je.Jade = new ObservableCollection<Jade>();
            je.Jade.Add(new Jade("水钻", 2, 3, 4));
            je.Jade.Add(new Jade());
            je.Gold = new ObservableCollection<Gold>();
            je.Gold.Add(new Gold(5, 6));
            return je;
        }
        public Jewelry(Image im,double tw,ObservableCollection<Gold> go,ObservableCollection<Jade> ja, double pf,double of)
        {
            this.Image = im;
            this.OtherFee = of;
            this.ProcessFee = pf;
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }
}
