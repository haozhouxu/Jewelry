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
using System.Data;
using System.Data.SQLite;
using System.Security.Cryptography;

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
            if (im.Source == null)
                return "";
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
            if (string.IsNullOrEmpty(base64String))
            {
                return im;
            }
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

        /// <summary>
        ///使用事例： 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SQLiteParameter[] commandParameters)
        {
            SQLiteCommand cmd = new SQLiteCommand();

            using (SQLiteConnection sqlconn = new SQLiteConnection(connectionString))
            {

                PrepareCommand(cmd, sqlconn, null, cmdType, cmdText, commandParameters);
                SQLiteTransaction trans = sqlconn.BeginTransaction();
                try
                {
                    cmd.Transaction = trans;
                    int val = cmd.ExecuteNonQuery();
                    trans.Commit();
                    //cmd.Parameters.Clear();
                    return val;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    sqlconn.Close();
                    throw ex;
                }
            }
        }

        private static void PrepareCommand(SQLiteCommand sqlcmd, SQLiteConnection sqlconn, SQLiteTransaction trans, CommandType cmdType, string cmdText, params SQLiteParameter[] commandParameters)
        {
            if (sqlconn.State == ConnectionState.Closed)
                sqlconn.Open();

            sqlcmd.Connection = sqlconn;
            sqlcmd.CommandText = cmdText;

            if (trans != null)
                sqlcmd.Transaction = trans;

            sqlcmd.CommandType = cmdType;

            if (commandParameters != null)
            {
                foreach (SQLiteParameter item in commandParameters)
                {
                    sqlcmd.Parameters.Add(item);
                }
            }
        }

        public static string DateToString(DateTime dt)
        {
            if (dt.DayOfYear == 1)
            {
                return null;
            }
            else
            {
                return dt.ToString("yyyy-MM-dd");
            }
        }

        //public static double StringToDouble(string temp)
        //{
        //    return null;
        //}

        public static string StringToMd5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));

            StringBuilder sb = new StringBuilder(16);
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("x"));
            }
            return sb.ToString().ToUpper();
        }
    }
}
