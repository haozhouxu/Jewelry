using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ManagementSystem.ViewModels
{
    public class SQLiteService
    {
        public static string connectionFormat = "Data Source=DB/{0}.db;Version=3;Password=123";

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
                        while(dr1.Read())
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
    }
}
