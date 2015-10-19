using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Windows.Media.Imaging;
using System.Data.SQLite;

namespace student
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string query = "select name,number from student;";
            SQLiteConnection conn = new SQLiteConnection("Data Source=DB/test.db;");
            conn.Open();
            

            SQLiteCommand cmd = conn.CreateCommand();
            //cmd.CommandText = "INSERT INTO student(name,number) VALUES(@Name,@Number)";
            cmd.CommandText = query;
            SQLiteDataReader dr = cmd.ExecuteReader();

            List<Student> StudentList = new List<Student>();
            if (dr.Read())
            {
                Student stu = new Student(dr["name"].ToString(), dr["number"].ToString());
                StudentList.Add(stu);
            }

            //MySqlConnection myConnection = new MySqlConnection("server=localhost;user=root;password=root;database=xhz1;port=3306;");
            //MySqlCommand myCommand = new MySqlCommand(query, myConnection);
            //myConnection.Open();
            //DataSet ds = MySqlHelper.ExecuteDataset(myConnection, query);
            //MySqlDataReader myDataReader = myCommand.ExecuteReader();

            //获取图片
            //Image img = new Image();

            ////新建一个student集合
            //List<Student> StudentList = new List<Student>();
            //foreach (DataTable dt in ds.Tables)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        byte[] by = (byte[])dr["image"];
            //        System.IO.MemoryStream ms = new System.IO.MemoryStream(by);
            //        //System.IO.Stream ss =  System.IO.Stream;
            //        //ms.CopyTo(ss);
            //        System.Drawing.Image imgg = System.Drawing.Image.FromStream(ms, true);
            //        System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(imgg);
            //        IntPtr hb = bmp.GetHbitmap();
            //        System.Windows.Media.ImageSource wpfbitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hb, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            //        img.Source = wpfbitmap;
            //        System.Windows.Media.ImageSource wpftooltip = wpfbitmap;
            //        Student stu = new Student(dr["name"].ToString(), dr["number"].ToString(), wpfbitmap, wpftooltip);
            //        StudentList.Add(stu);
            //    }
            //}

            //新建一个student集合
            //List<Student> StudentList = null;
            //while (myDataReader.Read() == true)
            //{
            //    Student stu = new Student(myDataReader["name"].ToString(),myDataReader["number"].ToString());
            //    StudentList.Add(stu);
            //}
            //myDataReader.Close();
            //myConnection.Close();

            this.student.ItemsSource = StudentList;


        }
    }

    public class Student
    {
        public Student(string name, string number)
        {
            Name = name;
            Number = number;
        }

        public Student(string name, string number, System.Windows.Media.ImageSource imageSource)
        {
            Name = name;
            Number = number;
            ImageSource = imageSource;
        }

        public Student(string name, string number, System.Windows.Media.ImageSource imageSource, System.Windows.Media.ImageSource toolTip)
        {
            Name = name;
            Number = number;
            ImageSource = imageSource;
            ToolTip = toolTip;
        }

        private string name; //学生姓名
        private string number; //学生编号
        private System.Windows.Media.ImageSource imageSource; //学生头像
        private System.Windows.Media.ImageSource tooltip;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        public System.Windows.Media.ImageSource ImageSource
        {
            get { return imageSource; }
            set { imageSource = value; }
        }
        public System.Windows.Media.ImageSource ToolTip
        {
            get { return tooltip; }
            set { tooltip = value; }
        }
    }
}
