using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace First.Model
{
    public class Historys : HistoryEntity
    {
        private Image _image ;

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
    }
}
