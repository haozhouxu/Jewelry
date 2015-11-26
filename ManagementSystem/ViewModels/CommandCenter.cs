using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ManagementSystem.Views;
using System.Windows.Media.Imaging;
using ManagementSystem.ViewModels;
using ManagementSystem.Models;
using System.Xml.Linq;

using System.IO;
using System.Windows.Controls;
using System.Data.SQLite;

namespace ManagementSystem.ViewModels
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
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message.ToString());
            }
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

                    BitmapImage bi = jw.Image.Source as BitmapImage;
                }
            }
        }

        private static void SaveLow(Dictionary<string, object> paraDic)
        {
            //新建一个xml文档类
            XDocument xdoc = new XDocument();
            //可以增加注释
            xdoc.Add(new XComment("增加注释测试"));
            //增加内容
            XStreamingElement rootXE = new XStreamingElement("root");

            Jewelry je = paraDic["context"] as Jewelry;

            //解析内容
            rootXE.Add(new XElement("guid",je.Guid.ToString()));
            rootXE.Add(new XElement("image", ImageToBase64(je.Image)));
            rootXE.Add(new XElement("totalweight", je.TotalWeight.ToString()));
            rootXE.Add(new XElement("jadeweight", je.JadeWeight.ToString()));
            rootXE.Add(new XElement("goldweight", je.GoldWeight.ToString()));
            rootXE.Add(new XElement("processfee", je.ProcessFee.ToString()));
            rootXE.Add(new XElement("otherfee", je.OtherFee.ToString()));
            rootXE.Add(new XElement("totalprice", je.TotalPrice.ToString()));

            xdoc.Add(rootXE);

            string sql = string.Format("insert into {0} Values('{1}','{2}','{3}','{4}')","detail", je.Guid.ToString(), xdoc.ToString(),System.DateTime.Now, System.DateTime.Now);

            //SQLiteConnection conn = new SQLiteConnection(@"Data Source=E:/vs_code/ms.db;");
            SQLiteConnection conn = new SQLiteConnection(@"Data Source=DB/ms.db;");
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            
            int i = cmd.ExecuteNonQuery();

            System.Windows.MessageBox.Show(i.ToString());

            conn.Close();
            
        }

        public static void AddNew(Dictionary<string ,object > paraDic)
        {
            Add an = new Add();
            an.Show();
        }

        public static string ImageToBase64(Image im)
        {
            BitmapImage bi = im.Source as BitmapImage;
            MemoryStream ms = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bi));
            encoder.Save(ms);
            byte[] by = null;
            by = ms.ToArray();
            return Convert.ToBase64String(by);
        }
    }
}
