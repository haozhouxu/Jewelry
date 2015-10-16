using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MySql.Data;
using MySql.Data.MySqlClient; //引用Mysql.data.dll中的类
using System.Data;
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

            string query = "SELECT * FROM student";
            MySqlConnection myConnection = new MySqlConnection("server=localhost;user=root;password=root;database=xhz1;port=3306;");
            //MySqlCommand myCommand = new MySqlCommand(query, myConnection);
            //myConnection.Open();
            DataSet ds = MySqlHelper.ExecuteDataset(myConnection, query);
            //MySqlDataReader myDataReader = myCommand.ExecuteReader();

            //新建一个student集合
            List<Student> StudentList = new List<Student>();
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Student stu = new Student(dr["name"].ToString(), dr["number"].ToString());
                    StudentList.Add(stu);
                }
            }

            //新建一个student集合
            //List<Student> StudentList = null;
            //while (myDataReader.Read() == true)
            //{
            //    Student stu = new Student(myDataReader["name"].ToString(),myDataReader["number"].ToString());
            //    StudentList.Add(stu);
            //}
            //myDataReader.Close();
            myConnection.Close();
            
            this.student.ItemsSource = StudentList;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Open(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Add openAdd = new Add();
            openAdd.Show();
        }
    }

    public class Student
    {
        public Student(string name,string number)
        {
            Name = name;
            Number = number;
        }

        private string name; //学生姓名
        private string number; //学生编号
        private Image image; //学生头像

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
        public Image Image
        {
            get { return image; }
            set { image = value; }
        }
    }
}
