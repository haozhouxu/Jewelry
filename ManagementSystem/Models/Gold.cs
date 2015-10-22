using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Models
{
    public class Gold: INotifyPropertyChanged
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
        public Gold()
        {

        }
        public Gold(float we,float un)
        {
            this.Weight = we;
            this.UnitPrice = un;
            this.TotalPrice = we * un;
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
