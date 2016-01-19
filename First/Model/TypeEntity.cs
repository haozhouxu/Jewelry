using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace First.Model
{
    public class TypeEntity : NotifyPropertyBase
    {
        private string _guid;
        private string _type;

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

        public TypeEntity()
        {

        }

        public TypeEntity(bool isNew)
        {
            if (isNew)
            {
                Guid = System.Guid.NewGuid().ToString();
            }
        }
    }
}
