using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace ManagementSystem.Models
{
    public class Jewelry:INotifyPropertyChanged
    {
        #region 字段
        private string guid;
        private Image image;
        private float totalWeight;
        private float jadeWeight;
        private float goldWeight;
        private ObservableCollection<Gold> gold;
        private ObservableCollection<Jade> jade;
        private float processFee;
        private float otherFee;
        private float totalPrice;
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
        public float TotalWeight
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
        public float JadeWeight
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
        public float GoldWeight
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
        public float ProcessFee
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
        public float OtherFee
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
        public float TotalPrice
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
        #endregion

        #region 方法
        Jewelry()
        {

        }
        Jewelry(Image im,float tw,ObservableCollection<Gold> go,ObservableCollection<Jade> ja, float pf,float of)
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
