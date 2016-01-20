using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using System.Windows.Media.Imaging;
using System.Xml.Linq;

using System.IO;
using System.Windows.Controls;
using System.Data.SQLite;
using System.Windows;
using System.Collections.ObjectModel;

using First.Model;
using System.Windows.Data;
using First.View;

namespace First
{
    public class CC
    {
        public static void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                Dictionary<string, object> paraDic = e.Parameter as Dictionary<string, object>;
                if (paraDic == null)
                {
                    return;
                }
                string type = (string)paraDic["type"];
                switch (type)
                {
                    case "addNew":
                        CC.AddNew(paraDic);
                        break;
                    case "saveLow":
                        CC.SaveLow(paraDic);
                        break;
                    case "selectImage":
                        CC.selectImage(paraDic);
                        break;
                    case "detail":
                        CC.Detail(paraDic);
                        break;
                    case "saveDetails":
                        CC.saveDetails(paraDic);
                        break;
                    case "TableCommonDeleteData":
                        CC.TableCommonDeleteData(paraDic);
                        break;
                    case "ImageView":
                        CC.ImageView(paraDic);
                        break;
                    case "TypeDetail":
                        CC.TypeDetail(paraDic);
                        break;
                    default:
                        string pageID = (string)paraDic["pageID"];
                        bool isEdit = false;
                        if (paraDic.ContainsKey("isEdit"))
                        {
                            isEdit = (bool)paraDic["isEdit"];
                        }
                        string title = "";
                        if (paraDic.ContainsKey("title"))
                        {
                            title = (string)paraDic["title"];
                        }
                        WinDetail wd1 = new WinDetail(paraDic["context1"], isEdit, title);
                        wd1.PageFrame.Navigate(new Uri(Tools.PageTool(pageID), UriKind.RelativeOrAbsolute));
                        bool? res = wd1.ShowDialog();
                        if (res.HasValue && res.Value)
                        {

                        }
                        if (paraDic.ContainsKey("context2"))
                            CommonReflashData((FrameworkElement)paraDic["context2"]);
                        break;
                }

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message.ToString());
            }
        }

        private static void TypeDetail(Dictionary<string, object> paraDic)
        {
            string pageID = (string)paraDic["pageID"];
            bool isEdit = false;
            if (paraDic.ContainsKey("isEdit"))
            {
                isEdit = (bool)paraDic["isEdit"];
            }
            string title = "";
            if (paraDic.ContainsKey("title"))
            {
                title = (string)paraDic["title"];
            }
            WinDetail2 wd1 = new WinDetail2(paraDic["context1"], isEdit, title);
            wd1.PageFrame.Navigate(new Uri(Tools.PageTool(pageID), UriKind.RelativeOrAbsolute));
            bool? res = wd1.ShowDialog();
            if (res.HasValue && res.Value)
            {

            }
            if (paraDic.ContainsKey("context2"))
                CommonReflashData((FrameworkElement)paraDic["context2"]);
        }

        public static void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {

                Dictionary<string, object> paraDic = e.Parameter as Dictionary<string, object>;
                if (paraDic == null)
                {
                    return;
                }
                string type = "";
                if (paraDic.ContainsKey("type"))
                {
                    type = (string)paraDic["type"];
                }
                switch (type)
                {
                    //珠宝类别保存
                    case "JeweleyCategoryType":
                    //珠宝归属人保存
                    case "JeweleyOwnType":
                    //珠宝颜色保存
                    case "JeweleyColorType":
                        Page page = paraDic["context"] as Page;
                        if (page == null)
                        {
                            return;
                        }
                        TypeEntity te = page.DataContext as TypeEntity;
                        if (te != null)
                        {
                            if (SQLiteService.SaveType(te, type))
                            {
                                MessageBox.Show("保存成功");
                            }
                            else
                            {
                                MessageBox.Show("保存失败");
                            }
                        }
                        break;
                    case "":
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private static void ImageView(Dictionary<string, object> paraDic)
        {
            if (paraDic == null)
            {
                return;
            }

            Image im = paraDic["context1"] as Image;
            ImageView iv = new ImageView(im);
            iv.Show();
        }

        private static void saveDetails(Dictionary<string, object> paraDic)
        {
            Frame frame = paraDic["context"] as Frame;
            Jewelry je = (frame.Content as FrameworkElement).DataContext as Jewelry;
            if (je != null)
            {
                if (SQLiteService.Save(je))
                {
                    MessageBox.Show("保存成功");
                }
                else
                {
                    MessageBox.Show("保存失败");
                }
            }
            else
            {

            }
            //string _guid = je.Guid;
            //if (SQLiteService.IsExitItem())
            //{

            //}
        }

        private static void Detail(Dictionary<string, object> paraDic)
        {

        }

        public static void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Dictionary<string, object> dic = e.Parameter as Dictionary<string, object>;
            if (dic == null)//参数是否为空
                return;
            Jewelry je = dic["context1"] as Jewelry;
            ItemsControl ic = dic["context2"] as ItemsControl;
            ObservableCollection<Jewelry> Oje = ic.ItemsSource as ObservableCollection<Jewelry>;

            if (je == null || Oje == null)
            {
                return;
            }

            if (MessageBox.Show("您确定要删除该记录么？", "系统提示", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                return;



            //dg1.Parent.Delete(dg1);
            //dg1.Parent.S.SearchObjectOfDataTuple_OnValueChanged(null, null);//刷新tuple带搜索类型的元素
            //if (dic.ContainsKey("save") && bool.Parse(dic["save"].ToString()))//需要保存
            //{
            //    Document doc = dg1.Parent.Owner as Document;
            //    SQLitehelper.SaveBack(doc, doc.DBFile);
            //}
        }

        internal static void ShowDetail(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void selectImage(Dictionary<string, object> paraDic)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ofd.Filter = "图片文件 (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;|所有文件|*.*";
            ofd.ValidateNames = true;
            ofd.DefaultExt = "*.jpg;*.jpeg;*.png;*.gif;";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string imageUrl = ofd.FileName;
                Jewelry jw = paraDic["context"] as Jewelry;
                if (null != jw)
                {
                    jw.Image.Source = new BitmapImage(new Uri(imageUrl, UriKind.Absolute));

                    //BitmapImage bi = jw.Image.Source as BitmapImage;
                }
            }
        }

        private static void SaveLow(Dictionary<string, object> paraDic)
        {
            try
            {
                //新建一个xml文档类
                XDocument xdoc = new XDocument();
                //可以增加注释
                xdoc.Add(new XComment("增加注释测试"));
                //增加内容
                XStreamingElement rootXE = new XStreamingElement("root");

                Jewelry je = paraDic["context"] as Jewelry;

                //解析内容
                rootXE.Add(new XElement("guid", je.Guid.ToString()));
                rootXE.Add(new XElement("image", helper.ImageToBase64(je.Image)));
                //rootXE.Add(new XElement("totalweight", je.TotalWeight.ToString()));
                //rootXE.Add(new XElement("jadeweight", je.JadeWeight.ToString()));
                //rootXE.Add(new XElement("goldweight", je.GoldWeight.ToString()));
                //rootXE.Add(new XElement("processfee", je.ProcessFee.ToString()));
                //rootXE.Add(new XElement("otherfee", je.OtherFee.ToString()));
                //rootXE.Add(new XElement("totalprice", je.TotalPrice.ToString()));

                xdoc.Add(rootXE);

                string sql = string.Format("insert into {0} Values('{1}','{2}','{3}','{4}')", "detail", je.Guid.ToString(), xdoc.ToString(), System.DateTime.Now, System.DateTime.Now);

                SQLiteConnection conn = new SQLiteConnection(@"Data Source=c:/xhz/ms.db;");
                //SQLiteConnection conn = new SQLiteConnection(@"Data Source=DB/ms.db;");
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);

                int i = cmd.ExecuteNonQuery();

                System.Windows.MessageBox.Show(i.ToString());

                conn.Close();

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message.ToString());
            }
            finally
            {

            }
        }

        public static void AddNew(Dictionary<string, object> paraDic)
        {
            //Add an = new Add();
            //an.Show();
        }

        internal static void CreateNew(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                //获取参数，如果为空，就返回
                Dictionary<string, object> paraDic = e.Parameter as Dictionary<string, object>;
                if (paraDic == null)
                    return;
                //定义参数
                string type = ""; //新增哪个页面
                string pageID = ""; //导航页面
                bool isEdit = false; //是否可以编辑，就是是否出现保存和取消按钮
                string title = ""; //页面的标题
                //获取对应的参数
                if (paraDic.ContainsKey("type"))
                {
                    type = (string)paraDic["type"];
                }
                if (paraDic.ContainsKey("pageID"))
                {
                    pageID = (string)paraDic["pageID"];
                }
                if (paraDic.ContainsKey("isEdit"))
                {
                    isEdit = (bool)paraDic["isEdit"];
                }
                if (paraDic.ContainsKey("title"))
                {
                    title = (string)paraDic["title"];
                }
                switch (type)
                {
                    //珠宝增加
                    case "GoodsType":
                        ObservableCollection<Jewelry> main_ic = paraDic["context1"] as ObservableCollection<Jewelry>;
                        if (main_ic != null)
                        {
                            Jewelry _je = new Jewelry(true);
                            //main_ic.Add(_je);
                            WinDetail wd1 = new WinDetail(_je, isEdit, title);
                            wd1.PageFrame.Navigate(new Uri(Tools.PageTool(pageID), UriKind.RelativeOrAbsolute));
                            bool? res = wd1.ShowDialog();

                            if (res.HasValue && res.Value)
                            {

                            }
                            CommonReflashData((FrameworkElement)paraDic["context2"]);
                        }
                        break;
                    //珠宝类别增加
                    case "JeweleyCategoryType":
                    //珠宝归属人增加
                    case "JeweleyOwnType":
                    //珠宝颜色增加
                    case "JeweleyColorType":
                        //上面三个类别都用同一个操作
                        ObservableCollection<TypeEntity> ObTE = paraDic["context1"] as ObservableCollection<TypeEntity>;
                        if (ObTE != null)
                        {
                            TypeEntity _te = new TypeEntity(true);
                            //main_ic.Add(_je);
                            WinDetail2 wd1 = new WinDetail2(_te, isEdit, title);
                            wd1.PageFrame.Navigate(new Uri(Tools.PageTool(pageID), UriKind.RelativeOrAbsolute));
                            bool? res = wd1.ShowDialog();

                            if (res.HasValue && res.Value)
                            {

                            }
                            CommonReflashData((FrameworkElement)paraDic["context2"]);
                        }
                        break;
                    case "":
                        break;
                    default:
                        break;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                //记录日志，暂未添加

            }
            finally
            {

            }
        }

        //刷新数据
        private static void CommonReflashData(FrameworkElement frame)
        {
            ObjectDataProvider odp = (ObjectDataProvider)frame.DataContext;
            odp.Refresh();
        }

        public static void TurnPageSqlite(System.Windows.Data.ObjectDataProvider odp, string str_offset, int pagesize, int totalcount)
        {
            if (odp != null)
            {
                try
                {
                    //2015.12.20 发现DeferRefresh可以在改变参数的时候，延迟刷行，利用using。注释掉下面三行
                    using (odp.DeferRefresh())
                    {
                        //odp.MethodParameters[1] = "";
                        int offset = int.Parse(str_offset);
                        int pageNum = (int)(odp.MethodParameters[4]) + offset;
                        int index = pagesize * (pageNum - 1);
                        if (index > totalcount)
                        {
                            int maxindex = totalcount % pagesize > 0 ? totalcount / pagesize + 1 : totalcount / pagesize;
                            odp.MethodParameters[4] = maxindex;
                        }
                        if (pageNum > 0 && totalcount > index)
                        {
                            odp.MethodParameters[4] = pageNum;
                            //odp.Refresh();
                        }
                        //odp.MethodParameters[1] = "1";
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public static void TurnPageOriginal(System.Windows.Data.ObjectDataProvider odp, string str_offset, string pagesize)
        {
            if (odp != null)
            {
                try
                {
                    odp.MethodParameters[1] = "";
                    odp.MethodParameters[3] = int.Parse(pagesize);
                    odp.MethodParameters[4] = int.Parse(str_offset);
                    odp.Refresh();
                    odp.MethodParameters[1] = "1";
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        //二维表删除数据
        public static void TableCommonDeleteData(Dictionary<string, object> dic)
        {
            if (MessageBox.Show("您确定要删除这条数据么？", "系统提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                string keyid = (string)dic["context1"];
                string sql_main = (string)dic["context3"];
                string sql_details = "";
                if (dic.Keys.Contains("context4"))
                {
                    sql_details = (string)dic["context4"];
                } 

                string dbfile = (string)dic["dbFile"];
                string connstr = string.Format(SQLiteService.connectionFormat, dbfile);

                int flag = helper.ExecuteNonQuery(connstr, System.Data.CommandType.Text, string.Format(sql_main, keyid), null);//删除主表的数据
                if (dic.Keys.Contains("context4"))
                {
                    helper.ExecuteNonQuery(connstr, System.Data.CommandType.Text, string.Format(sql_details, keyid), null);//删除详细表的数据
                }
                CommonMessageShow("删除", flag == 1);
                CommonReflashData((FrameworkElement)dic["context2"]);//刷新数据  
            }
        }

        //提示语
        private static void CommonMessageShow(string title, bool flag)
        {
            if (flag)
                MessageBox.Show(title + "成功！");
            else
                MessageBox.Show(title + "失败！");
        }
    }
}
