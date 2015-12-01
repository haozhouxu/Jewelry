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
        private ObservableCollection<Jewelry> _OCJ = new ObservableCollection<Jewelry>();
        public ObservableCollection<Jewelry> OCJ
        {
            get { return _OCJ; }
            set { _OCJ = value; NotifyPropertyChanged("OCJ"); }
        }

        public ObservableCollection<Jewelry> GetAll()
        {
            using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=c:/xhz/ms.db;"))
            //using (SQLiteConnection conn = new SQLiteConnection(@"Data Source=DB/ms.db;"))
            {
                conn.Open();

                string sql = string.Format("select * from detail");

                SQLiteCommand cmd = new SQLiteCommand(sql, conn);

                using (SQLiteDataReader dr1 = cmd.ExecuteReader())
                {
                    while (dr1.Read())
                    {
                        var d = dr1;
                        var guid = dr1.GetValue(0);
                        var data = dr1.GetValue(1);
                        //var insert = dr1.GetValue(2);
                        //var update = dr1.GetValue(3);

                        //没有把helper推送到服务器
                        Jewelry je1 = helper.xmlPras(data.ToString());
                        _OCJ.Add(je1);
                    }
                }

                conn.Close();
            }
            OCJ.Add(Jewelry.GetExample());
            return OCJ;
        }
    }
}
