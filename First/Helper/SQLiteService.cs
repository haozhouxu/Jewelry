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
        public static string connectionFormat = "Data Source=./DB/{0};";

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

        public static string IsExitItem(string guid)
        {
            try
            {
                string _state = "";
                string connectString = string.Format(connectionFormat, "first");
                string sqlAll = "select state from {0} where guid = '{1}'";
                string sql = string.Format(sqlAll, "Data", guid);
                using (SQLiteConnection sqlcon = new SQLiteConnection(connectString))
                {
                    SQLiteCommand cmd = new SQLiteCommand(sql, sqlcon);
                    sqlcon.Open();
                    using (SQLiteDataReader dr1 = cmd.ExecuteReader())
                    {
                        while (dr1.Read())
                        {
                            _state = (string)dr1["state"];
                        }
                        dr1.Close();
                    }
                    sqlcon.Close();
                }
                if (!string.IsNullOrEmpty(_state))
                {
                    return _state;
                }
                return "";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static bool Save(Jewelry je)
        {
            try
            {
                //定义temp存储影响行数
                int temp = 0;

                //先进行判断是否是状态改变，如果是的话，往History表插入一条数据
                string _state = IsExitItem(je.Guid);
                if (!string.IsNullOrEmpty(_state) && !(_state.Equals(je.State)))
                {
                    //往历史表插入一条记录（未完成）

                }

                //更新数据，不管是编辑，还是新增
                string connectString = string.Format(connectionFormat, "first");
                string sqlAll = "replace into Data (guid,image,buytime,buyprice,buywho,goldprice,type,color,mark,buySource,ownwho,state,borrowtime,borrowwho,borrowprice,borrowreturntime,saletime,salewho,saleprice,salestate) Values('{0}','{1}','{2}',{3},'{4}',{5},'{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}',{14},'{15}','{16}','{17}',{18},'{19}')";
                //把图片转换成base64编码（未完成）
                string imageString = helper.ImageToBase64(je.Image);
                string sql = string.Format(sqlAll,je.Guid, imageString,je.BuyTime,je.BuyPrice, je.BuyWho, je.GoldPrice, je.Type, je.Color, je.Mark, je.BuySource, je.OwnWho, je.State, je.BorrowTime, je.BorrowWho, je.BorrowPirce, je.BorrowReturnTime, je.SaleTime, je.SaleWho, je.SalePirce, je.SaleState);
                using (SQLiteConnection sqlcon = new SQLiteConnection(connectString))
                {
                    SQLiteCommand cmd = new SQLiteCommand(sql, sqlcon);
                    sqlcon.Open();
                    temp = cmd.ExecuteNonQuery();
                    //using (SQLiteDataReader dr1 = cmd.ExecuteReader())
                    //{
                    //    while (dr1.Read())
                    //    {

                    //    }
                    //    dr1.Close();
                    //}
                    sqlcon.Close();
                }

                //返回更新成功或者失败的标志
                if (temp > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
