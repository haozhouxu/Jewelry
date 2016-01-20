using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using First.Helper;

namespace First.Model
{
    public class ColorTypes: ObservableCollection<TypeEntity>
    {
        public ColorTypes()
        {
            string sql = "select * from Type where category='" + GlobalBindingHelper.ColorType + "' order by strftime('%Y-%m-%d %H%M%S',createtime) desc";
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
