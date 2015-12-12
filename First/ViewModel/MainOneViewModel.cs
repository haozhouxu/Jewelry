using First.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First.ViewModel
{
    public class MainOneViewModel : NotifyPropertyBase
    {
        private ObservableCollection<Jewelry> _OCJ; /*= new ObservableCollection<Jewelry>();*/
        private int _totalCount, _pageCount, _pageIndex, _pageSize;
        
        public ObservableCollection<Jewelry> OCJ
        {
            get { return _OCJ; }
            set { _OCJ = value; NotifyPropertyChanged("OCJ"); }
        }

        public int TotalCount
        {
            get { return _totalCount; }
            set {if (_totalCount != value) { _totalCount = value; NotifyPropertyChanged("TotalCount"); }}
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


        public MainOneViewModel()
        {
            //OCJ = new ObservableCollection<Jewelry>();
            //TotalCount = 0;
            //PageCount = 0;
            //PageIndex = 0;
            //PageSize = 0;
            //SQLiteConnection conn = new SQLiteConnection(@"Data Source=c:/xhz/first;");
        }

        //public ObservableCollection<Jewelry> GetAll()
        //{
        //    using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=c:/xhz/first;"))
        //    //using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=DB/ms.db;"))
        //    {
        //        conn.Open();

        //        string sql = string.Format("select * from Data");

        //        SQLiteCommand cmd = new SQLiteCommand(sql, conn);

        //        using (SQLiteDataReader dr1 = cmd.ExecuteReader())
        //        {
        //            while (dr1.Read())
        //            {
        //                var d = dr1;
        //                //var guid = dr1.GetValue(0);
        //                //var data = dr1.GetValue(1);
        //                //var insert = dr1.GetValue(2);
        //                //var update = dr1.GetValue(3);

        //                //没有把helper推送到服务器
        //                //Jewelry je1 = helper.xmlPras(data.ToString());
        //                //_OCJ.Add(je1);
        //            }
        //        }

        //        conn.Close();
        //    }
        //    OCJ.Add(Jewelry.GetExample());
        //    return OCJ;
        //}

        //public ObservableCollection<Jewelry> GetExample()
        //{
        //    ObservableCollection<Jewelry> oje = new ObservableCollection<Jewelry>();

        //    //using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=c:/xhz/first;"))
        //    //{
        //    //    conn.Open();

        //    //    string sql = string.Format("select * from Data");

        //    //    SQLiteCommand cmd = new SQLiteCommand(sql, conn);

        //    //    using (SQLiteDataReader dr1 = cmd.ExecuteReader())
        //    //    {
        //    //        while (dr1.Read())
        //    //        {
        //    //            var d = dr1;
        //    //        }
        //    //    }

        //    //    conn.Close();
        //    //}

        //    Jewelry je = Jewelry.GetExample();
        //    OCJ.Add(je);

        //    return OCJ;
        //}
    }
}
