using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using First.Model;

namespace First.ViewModel
{
    public class TypeViewModel : NotifyPropertyBase
    {
        private ObservableCollection<TypeEntity> _ocTypeEntities;
        private int _totalCount, _pageCount, _pageIndex, _pageSize;

        public bool IsBtnPreEnable
        {
            get
            {
                if (PageIndex <= 1)
                    return false;
                return true;
            }
        }
        public bool IsBtnNxtEnable
        {
            get
            {
                if (PageIndex >= PageCount)
                    return false;
                return true;
            }
        }
        public int TotalCount
        {
            get { return _totalCount; }
            set { if (_totalCount != value) { _totalCount = value; NotifyPropertyChanged("TotalCount"); } }
        }
        public int PageCount
        {
            get { return _pageCount; }
            set { if (_pageCount != value) { _pageCount = value; NotifyPropertyChanged("PageCount"); } }
        }
        public int PageIndex
        {
            get { return _pageIndex; }
            set { if (_pageIndex != value) { _pageIndex = value; NotifyPropertyChanged("PageIndex"); } }
        }
        public int PageSize
        {
            get { return _pageSize; }
            set { if (_pageSize != value) { _pageSize = value; NotifyPropertyChanged("PageSize"); } }
        }

        public ObservableCollection<TypeEntity> OcTypeEntities
        {
            get { return _ocTypeEntities; }
            set { _ocTypeEntities = value; NotifyPropertyChanged("OcTypeEntities"); }
        }

        public TypeViewModel()
        {

        }
    }
}
