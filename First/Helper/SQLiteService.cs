using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Collections.ObjectModel;

using First.Model;

namespace First
{
    public class SQLiteService
    {
        public static string connectionFormat = "Data Source=./DB/ms.db;";

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="dbFile"></param>
        public static void SelectAll(string dbFile)
        {
            try
            {
                string connectString = string.Format(connectionFormat, dbFile);
                string sqlAll = "select * from {0}";
                string sql = string.Format(sqlAll, dbFile);

                using (SQLiteConnection sqlcon = new SQLiteConnection(connectString))
                {
                    SQLiteCommand cmd = new SQLiteCommand(sql, sqlcon);
                    sqlcon.Open();
                    using (SQLiteDataReader dr1 = cmd.ExecuteReader())
                    {
                        while (dr1.Read())
                        {

                        }
                        dr1.Close();
                    }
                    sqlcon.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SelectByGuid(string dbFile)
        {
            try
            {
                string connectString = string.Format(connectionFormat, dbFile);
                string sqlAll = "select * from {0}";
                string sql = string.Format(sqlAll, dbFile);

                using (SQLiteConnection sqlcon = new SQLiteConnection(connectString))
                {
                    SQLiteCommand cmd = new SQLiteCommand(sql, sqlcon);
                    sqlcon.Open();
                    using (SQLiteDataReader dr1 = cmd.ExecuteReader())
                    {
                        while (dr1.Read())
                        {

                        }
                        dr1.Close();
                    }
                    sqlcon.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public static int Insert(string dbFile, string sql,)
        public static ObservableCollection<Jewelry> GetAll()
        {
            ObservableCollection<Jewelry> ob = new ObservableCollection<Jewelry>();

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
                        var dd = dr1.GetValue(0);
                        var data = dr1.GetValue(1);
                        var insert = dr1.GetValue(2);
                        var update = dr1.GetValue(3);
                    }
                }

                conn.Close();
            }

            ob.Add(new Jewelry());

            return ob;
        }
    }
}
