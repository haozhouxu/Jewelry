using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Collections.ObjectModel;

using First.Model;
using First.ViewModel;
using System.Collections;

namespace First
{
    public class SQLiteService
    {
        private static int _totalCount;
        public static readonly string borrowtype = "借出";
        public static readonly string saletype = "卖出";
        public static readonly string nosaletype = "未卖";

#if DEBUG
        //测试数据库
        public static string connectionFormat = @"Data Source=C:\xhz\{0};";
#else
        //正式数据库
        //public static string connectionFormat = "Data Source="+ System.IO.Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, @"DB\{0};");
        public static string connectionFormat = @"Data Source=DB\{0};Version=3";
#endif
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
                string sql = string.Format(sqlAll,je.Guid, imageString,je.BuyTime.ToString("s"),je.BuyPrice, je.BuyWho, je.GoldPrice, je.Type, je.Color, je.Mark, je.BuySource, je.OwnWho, je.State, je.BorrowTime.ToString("s"), je.BorrowWho, je.BorrowPirce, je.BorrowReturnTime.ToString("s"), je.SaleTime.ToString("s"), je.SaleWho, je.SalePirce, je.SaleState);
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

        //带分页的查询语句
        public static MainOneViewModel LoadDataAsMainOneViewModel_Paging_Sqlite(string dbFile, string modelGroupString, string sql, Int32 pageSize, Int32 offset, params IDictionary[] conditions)
        {
            try
            {
                //因为每次改变数据源的参数的时候，就会自动刷新数据源，设置了一个参数，来判断是否是参数已经设置完了
                if (modelGroupString.Equals(""))
                {
                    return null;
                }
                string sqlpara = string.Empty;
                List<string> list = GetCommonCondition(conditions);
                if (list.Count > 0)
                    sqlpara = sqlpara + " where " + string.Join(" and ", list);
                sql = string.Format(sql, sqlpara) + string.Format(" limit {0} offset {1}", pageSize, pageSize * (offset - 1));

                _totalCount = 0;
                MainOneViewModel vmNew = LoadDataAsMainOneViewModel(dbFile, sql);

                if (vmNew != null)
                {
                    if (vmNew.OCJ.Count > 0)
                        vmNew.TotalCount = _totalCount;
                    else
                        vmNew.TotalCount = 0;
                    vmNew.PageCount = vmNew.TotalCount % pageSize > 0 ? (vmNew.TotalCount / pageSize + 1) : (vmNew.TotalCount / pageSize);
                    vmNew.PageIndex = offset;
                    vmNew.PageSize = pageSize;
                    //vmNew.ModelGroupString = modelGroupString;
                }
                return vmNew;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //查询条件
        public static List<string> GetCommonCondition(IDictionary[] conditions)
        {
            if (conditions != null && conditions.Count() > 0)
            {
                //var l1 = conditions.Where(d1 => !string.IsNullOrEmpty(Convert.ToString((object)d1["val"]))).ToList();
                //if (l1.Count > 0)
                //{
                //var l2 = from d1 in l1
                //         select string.Format((string)d1["format"], d1["val"]);
                //return l2.ToList();
                List<string> list = new List<string>();

                foreach (var item in conditions)
                {
                    //if (item.Count == 2)
                    //{
                    //    if (item["val"] != null && !string.IsNullOrEmpty(item["val"].ToString()))
                    //    {
                    //        if (item["val"].GetType().Name.ToLower() == "datetime")
                    //            list.Add(string.Format((string)item["format"], ((DateTime)item["val"]).ToString("yyyy-MM-dd")));
                    //        else if (item["val"].GetType().Name.ToLower() == "int32")
                    //            list.Add(string.Format((string)item["format"], ((Int32)item["val"]).ToString()));

                    //        else
                    //            list.Add(string.Format((string)item["format"], string.IsNullOrEmpty(((string)item["val"])) ? "" : ((string)item["val"]).Replace("&lt;", "<").Replace("&gt;", ">")));
                    //    }
                    //}

                    List<string> listCon = new List<string>();
                    int items = item.Count;
                    string sqlpara = string.Empty;
                    for (int i = 0; i < item.Count / 2; i++)
                    {

                        string fFormat = "format";
                        string fVal = "val";
                        if (i > 0)
                        {
                            fFormat = fFormat + i.ToString();
                            fVal = fVal + i.ToString();
                        }
                        if (item[fVal] != null)
                        {
                            if (!string.IsNullOrEmpty(item[fVal].ToString()))
                            {
                                if (item[fVal].GetType().Name.ToLower() == "datetime")
                                    listCon.Add(string.Format((string)item[fFormat], ((DateTime)item[fVal]).ToString("yyyy-MM-dd")));
                                else if (item[fVal].GetType().Name.ToLower() == "int32")
                                    listCon.Add(string.Format((string)item[fFormat], ((Int32)item[fVal]).ToString()));

                                else
                                    listCon.Add(string.Format((string)item[fFormat], string.IsNullOrEmpty(((string)item[fVal])) ? "" : ((string)item[fVal]).Replace("&lt;", "<").Replace("&gt;", ">").Trim()));
                            }
                        }
                    }
                    if (items > 2)
                    {
                        if (listCon.Count > 0)
                        {
                            sqlpara = sqlpara + string.Join(" or ", listCon);
                            sqlpara = "( " + sqlpara + ")";
                            list.Add(sqlpara);
                        }

                    }
                    else
                    {
                        if (listCon.Count > 0)
                            list.Add(listCon.First());
                    }

                }
                return list;
                //}
            }
            return new List<string>();
        }

        public static MainOneViewModel LoadDataAsMainOneViewModel(string dbFile, string sql)
        {
            try
            {
                MainOneViewModel vm = new MainOneViewModel();
                vm.OCJ = new ObservableCollection<Jewelry>();
                //System.Windows.MessageBox.Show(string.Format(SQLiteService.connectionFormat, dbFile));
                using (SQLiteConnection sc1 = new SQLiteConnection(string.Format(SQLiteService.connectionFormat, dbFile)))
                {
                    SQLiteCommand sCom = new SQLiteCommand(sql, sc1);
                    sc1.Open();
                    using (SQLiteDataReader dr1 = sCom.ExecuteReader())
                    {
                        while (dr1.Read())
                        {
                            Jewelry je = new Jewelry();
                            je.Guid = dr1["guid"].ToString();
                            je.Image = helper.Base64ToImage(dr1["image"].ToString());
                            //je.SaleWho = dr1["buytime"].ToString();
                            je.BuyTime = (DateTime)dr1["buytime"];
                            je.BuyPrice = (double)dr1["buyprice"];
                            je.BuyWho = dr1["buywho"].ToString();
                            je.GoldPrice = (double)dr1["goldprice"];
                            je.Type = dr1["type"].ToString();
                            je.Color = dr1["color"].ToString();
                            je.Mark = dr1["mark"].ToString();
                            je.BuySource = dr1["buySource"].ToString();
                            je.OwnWho = dr1["ownwho"].ToString();
                            je.State = dr1["state"].ToString();
                            je.BorrowTime = (DateTime)dr1["borrowtime"];
                            je.BorrowWho = dr1["borrowwho"].ToString();
                            je.BorrowPirce = (double)dr1["borrowprice"];
                            je.BorrowReturnTime = (DateTime)dr1["borrowreturntime"];
                            je.SaleTime = (DateTime)dr1["saletime"];
                            je.SaleWho = dr1["salewho"].ToString();
                            je.SalePirce = (double)dr1["saleprice"];
                            je.SaleState = dr1["salestate"].ToString();

                            _totalCount = int.Parse(dr1["TotalCount"].ToString());

                            vm.OCJ.Add(je);
                            //List<string> l1 = new List<string>();
                            //DataGroup dg1 = new DataGroup();
                            //for (int i = 0; i < dr1.FieldCount; i++)
                            //{
                            //    DataItem dm1 = new DataItem()
                            //    {
                            //        ID = dr1.GetName(i),
                            //        ItemType = TransSqliteType(dr1.GetDataTypeName(i)),
                            //        Value = Convert.ToString(dr1.GetValue(i))
                            //    };
                            //    dg1.Add(dm1);
                            //}
                            //dt1.Add(dg1);
                        }
                        dr1.Close();
                    }
                    sc1.Close();
                }
                return vm;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
