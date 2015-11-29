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
using System.Windows.Threading;

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
                ThrowMsg("登陆成功,正在加载数据...", false);
                System.Threading.Thread.Sleep(1000);
                App app = (App)Application.Current;
                //MainWindow mw = new MainWindow();
                //app.MainWindow = mw;
                //mw.Show();
                MainShell ms = new MainShell();
                app.MainWindow = ms;
                ms.Show();
                login.Close();
            }
            else
            {
                ThrowMsg("登陆失败！请检查用户名和密码", true);
            }
        }

        /// <summary>
        /// 抛出信息【易神】
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="isWarning">是否为警告信息</param>
        void ThrowMsg(string msg, bool isWarning)
        {
            this.tbMsg.Foreground = new SolidColorBrush(isWarning ? Colors.Red : Colors.Gray);
            this.tbMsg.Text = msg;
            DoEvents();
        }

        public void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(delegate (object f)
            {
                ((DispatcherFrame)f).Continue = false;
                return null;
            }), frame);
            Dispatcher.PushFrame(frame);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("您确定要退出程序吗？", "系统提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                App.Current.Shutdown();
            //this.Close();
        }
    }
}
