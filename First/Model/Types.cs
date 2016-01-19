using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using First.Helper;

namespace First.Model
{
     public class Types : ObservableCollection<TypeEntity>
    {
        public Types()
        {
            this.Add(new TypeEntity { Type = "耳坠" });
            this.Add(new TypeEntity { Type = "挂件" });
            this.Add(new TypeEntity { Type = "戒指" });
            this.Add(new TypeEntity { Type = "手链" });
            this.Add(new TypeEntity { Type = "手镯" });
            string sql = "select * from Type where category='"+ GlobalBindingHelper.JewelryType +"' order by strftime('%Y-%m-%d %H%M%S',createtime) desc";
            string dbFile = "first";
            using (SQLiteConnection sc1 = new SQLiteConnection(string.Format(SQLiteService.connectionFormat, dbFile)))
            {
                SQLiteCommand sCom = new SQLiteCommand(sql, sc1);
                sc1.Open();
                using (SQLiteDataReader dr1 = sCom.ExecuteReader())
                {
                    while (dr1.Read())
                    {
                        TypeEntity te = new TypeEntity();
                        te.Guid = dr1["guid"].ToString();
                        te.Type = dr1["name"].ToString();
                        this.Add(te);
                    }
                    dr1.Close();
                }
                sc1.Close();
            }
        }
    }
}
