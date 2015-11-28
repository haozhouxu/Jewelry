using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using ManagementSystem;

namespace ManagementSystem.Views
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        void login_Click(object sender, RoutedEventArgs e)
        {
            //UserLogin();
            string user = tb_UseName.Text;
            string pwd = pb_Password.Password.Trim();

            if (user.Equals("admin")&&pwd.Equals("123"))
            {
                MessageBox.Show("登陆成功");
                App app = (App)Application.Current;
                MainWindow mw = new MainWindow();
                app.MainWindow = mw;
                mw.Show();
                login.Close();
            }
            else
            {
                MessageBox.Show("登陆失败，请检查用户和密码！");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("您确定要退出程序吗？", "系统提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                App.Current.Shutdown();
            //this.Close();
        }
    }
}
