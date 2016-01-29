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
            if (dt.Year == 1)
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

        #region 加密
        //MD5不区分大小写的
        //type 类型，16位还是32位，16位就是取32位的第8到16位
        public static string DoMd5Encode(string pwd, string type)
        {
            byte[] result = Encoding.Default.GetBytes(pwd);
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            if (type == "16")
                return BitConverter.ToString(output).Replace("-", "").ToLower().Substring(8, 16);
            else
                return BitConverter.ToString(output).Replace("-", "").ToLower();

        }


        /// <summary>
        /// 对字符串进行SHA1加密
        /// </summary>
        /// <param name="strIN">需要加密的字符串</param>
        /// <returns>密文</returns>
        public static string SHA1_Encrypt(string Source_String)
        {
            byte[] StrRes = Encoding.Default.GetBytes(Source_String);
            System.Security.Cryptography.HashAlgorithm iSHA = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            StrRes = iSHA.ComputeHash(StrRes);
            StringBuilder EnText = new StringBuilder();
            foreach (byte iByte in StrRes)
            {
                EnText.AppendFormat("{0:x2}", iByte);
            }
            return EnText.ToString();
        }

        /// <summary>
        /// SHA256加密，不可逆转
        /// </summary>
        /// <param name="str">string str:被加密的字符串</param>
        /// <returns>返回加密后的字符串</returns>
        public static string SHA256Encrypt(string str)
        {
            System.Security.Cryptography.SHA256 s256 = new System.Security.Cryptography.SHA256Managed();
            byte[] byte1;
            byte1 = s256.ComputeHash(Encoding.Default.GetBytes(str));
            s256.Clear();
            return Convert.ToBase64String(byte1);
        }

        /// <summary>
        /// SHA384加密，不可逆转
        /// </summary>
        /// <param name="str">string str:被加密的字符串</param>
        /// <returns>返回加密后的字符串</returns>
        public static string SHA384Encrypt(string str)
        {
            System.Security.Cryptography.SHA384 s384 = new System.Security.Cryptography.SHA384Managed();
            byte[] byte1;
            byte1 = s384.ComputeHash(Encoding.Default.GetBytes(str));
            s384.Clear();
            return Convert.ToBase64String(byte1);
        }



        /// <summary>
        /// SHA512加密，不可逆转
        /// </summary>
        /// <param name="str">string str:被加密的字符串</param>
        /// <returns>返回加密后的字符串</returns>
        public static string SHA512Encrypt(string str)
        {
            System.Security.Cryptography.SHA512 s512 = new System.Security.Cryptography.SHA512Managed();
            byte[] byte1;
            byte1 = s512.ComputeHash(Encoding.Default.GetBytes(str));
            s512.Clear();
            return Convert.ToBase64String(byte1);
        }
        #endregion

    }
}
