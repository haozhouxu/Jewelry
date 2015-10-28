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
                }
            }
        }

        private static void SaveLow(Dictionary<string, object> paraDic)
        {
            throw new NotImplementedException();
        }

        public static void AddNew(Dictionary<string ,object > paraDic)
        {
            Add an = new Add();
            an.Show();
        }
    }
}
