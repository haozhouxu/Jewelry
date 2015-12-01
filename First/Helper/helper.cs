using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

using First.Model;

namespace First
{
    public class helper
    {
        public static Jewelry xmlPras(string xml)
        {
            Jewelry je = new Jewelry();

            var root = XElement.Parse(xml);

            je.Type =   root.Element("Type").Value.ToString();
            je.State =  root.Element("State").Value.ToString();
            je.OwnWho = root.Element("OwnWho").Value.ToString();
            je.Image = helper.Base64ToImage(root.Element("Image").Value.ToString());

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
