using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ManagementSystem.DB
{
    public class Jewelry:INotifyPropertyChanged
    {
        #region 字段
        private float weight;
        private float unitPrice;
        private float totalPrice;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region 属性
        public float Weight
        {
            get
            {
                return weight;
            }
            set
            {
                weight = value;
                NotifyPropertyChanged("Weight");
            }
        }
        public float UnitPrice
        {
            get
            {
                return unitPrice;
            }
            set
            {
                unitPrice = value;
                NotifyPropertyChanged("UnitPrice");
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
        Gold(float we, float un)
        {
            Weight = we;
            UnitPrice = un;
            TotalPrice = we * un;
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
