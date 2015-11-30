using ManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace ManagementSystem
{
    public class helper
    {
        public static Jewelry xmlPras(string xml)
        {
            Jewelry je = new Jewelry();

            var root = XElement.Parse(xml);

            var ss = root.Element("goldweight");

            je.GoldWeight = Convert.ToDouble(root.Element("goldweight").Value.ToString());
            je.JadeWeight = Convert.ToDouble(root.Element("jadeweight").Value.ToString());
            je.TotalPrice = Convert.ToDouble(root.Element("totalprice").Value.ToString());
            je.Image = helper.Base64ToImage(root.Element("image").Value.ToString());

            //je.GoldWeight = Convert.ToDouble(ss.Value);

            return je;
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

        public static Image Base64ToImage(string base64String)
        {
            Image im = new Image();

            byte[] fileBytes = Convert.FromBase64String(base64String);

            using (MemoryStream ms = new MemoryStream(fileBytes, 0, fileBytes.Length))
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                im.Source = bitmapImage;
            }

            return im;
        }
    }
}
