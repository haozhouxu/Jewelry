using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ManagementSystem.Models
{
    public class Jade : INotifyPropertyChanged
    {
        #region 字段
        private string type;
        private int? number;
        private double? weight;
        private double? unitPrice;
        private double? totalPrice;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region 属性
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                NotifyPropertyChanged("Type");
            }
        }
        public int? Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
                NotifyPropertyChanged("Number");
            }
        }
        public double? Weight
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
        public double? UnitPrice
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
        public double? TotalPrice
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
        public Jade()
        {
            this.Type = "";
            this.Number = null;
            this.Weight = null;
            this.UnitPrice = null;
            this.TotalPrice = null;
        }
        public Jade(string ty, int num,double we,double un)
        {
            this.Type = ty;
            this.Number = num;
            this.Weight = we;
            this.UnitPrice = un;
            this.TotalPrice = num * un;
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

    public enum JadeTypeEnum
    {
        CircleType = 1,
        DripType = 2,
        GemType = 3,
        PrincessType = 4,
        HorseEyeType = 5,
        YellowType = 6,
        LadderType = 7,
        JasperType = 8
    }
}
