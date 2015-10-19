using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using MySql.Data.MySqlClient;//添加mysql dll
using System.IO;//添加
using System.Drawing; //tianjia
using System.Collections.Generic;

namespace student
{
    /// <summary>
    /// Add.xaml 的交互逻辑
    /// </summary>
    public partial class Add : Window
    {
        private string imageUrl = "";

        public Add()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            //fbd.RootFolder = Environment.SpecialFolder.Desktop;
            //if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{

            //}
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Filter = "图片文件 (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;|所有文件|*.*";
            ofd.ValidateNames = true;
            ofd.DefaultExt = "*.jpg;*.jpeg;*.png;*.gif;";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                imageUrl = ofd.FileName;
                this.image.Source = new BitmapImage(new Uri(ofd.FileName, UriKind.Absolute));
            }
        }

        private void Save_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            string name = this.name1.Text;
            string number = this.number1.Text;
            //将图片对象image转换成缓冲流imageStream
            MemoryStream imageStream = new MemoryStream();
            BitmapImage bi = image.Source as BitmapImage;
            System.Drawing.Image im = System.Drawing.Image.FromFile(imageUrl);
            im.Save(imageStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] imageByte = imageStream.GetBuffer();
            long len = imageStream.Length;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(number))
            {
                MessageBox.Show("姓名或者编号不能为空！！");
            }

            //连接数据库的操作步骤
            //第一步：创建数据库连接,并打开数据库连接
            string MyConnectionString = "server=localhost;user=root;password=root;database=xhz1;port=3306;";
            MySqlConnection con = new MySqlConnection(MyConnectionString);
            con.Open();

            //第二步：创建执行命令
            string CommandText = "INSERT INTO student(name,number,image) VALUES(@name,@number,@image)";
            //List<MySqlParameter> paras = new List<MySqlParameter>()
            //{
            // new MySqlParameter("@name", MySqlDbType.VarChar),
            // new MySqlParameter(),
            // new MySqlParameter()
            //};
            //MySqlParameter par = new MySqlParameter("@image", MySqlDbType.Blob, (int)len)
            MySqlCommand cmd = new MySqlCommand(CommandText, con);
            cmd.Parameters.Add(new MySqlParameter("@name", MySqlDbType.VarChar)).Value = name;
            cmd.Parameters.Add(new MySqlParameter("@number", MySqlDbType.VarChar)).Value = number;
            cmd.Parameters.Add(new MySqlParameter("@image", MySqlDbType.LongBlob)).Value = imageByte;

            //第三步：执行命令
            cmd.ExecuteNonQuery();

            //第四步：关闭数据库连接
            con.Close();
        }

        private void Open_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {

        }
    }
}
