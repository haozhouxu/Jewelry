using System;
using System.Collections.Generic;
using System.Data.SQLite;
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

using First;
using System.Windows.Threading;
using System.Threading;
using System.Reflection;
using First.Helper;
using System.Management;

namespace First
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        /// <summary>
        /// 确保兼容“记住密码”【易神】
        /// </summary>
        public string MD5Password { get; set; }
        readonly static object _lock = new object();
        private static Semaphore singleInstanceWatcher;
        private static bool createdNew;
        string TempPwd;
        int TempLength = 0;

        //xhz:增加判断是否自动登录和是否记住密码
        private bool isRemember = false;
        private bool isAuth = false;

        public Login()
        {
            //InitializeComponent();
            // 确保不存在程序的其他实例
            singleInstanceWatcher = new Semaphore(
                0, // Initial count.
                1, // Maximum count.
                Assembly.GetExecutingAssembly().GetName().Name, out createdNew);
            if (createdNew)
            {
                //Common.ExistsInternt();
                InitializeComponent();
            }
            else
            {
                MessageBox.Show("程序已在运行中");
                Environment.Exit(-2);
            }
            this.tb_UseName.Focus();
        }

        //~Login()
        //{
        //    Dispose(false);
        //}

        void login_Click(object sender, RoutedEventArgs e)
        {
            UserLogin();
            ////检查是否是指定的电脑
            //ThrowMsg("正在检查硬件...", false);
            //if (CheckUser())
            //{
            //    UserLogin();
            //}
            //else
            //{
            //    ThrowMsg("您的电脑不能登录，请联系管理员！", false);
            //}

            //string user = tb_UseName.Text.Trim();
            //string pwd = pb_Password.Password.Trim();

            //if (user.Equals("admin") && pwd.Equals("123"))
            //{
            //    ThrowMsg("登陆成功,正在加载数据...", false);
            //    System.Threading.Thread.Sleep(1000);
            //    App app = (App)Application.Current;
            //    //MainWindow mw = new MainWindow();
            //    //app.MainWindow = mw;
            //    //mw.Show();
            //    MainShell ms = new MainShell();
            //    app.MainWindow = ms;
            //    ms.Show();
            //    login.Close();
            //}
            //else
            //{
            //    ThrowMsg("登陆失败！请检查用户名和密码", true);
            //}
        }

        private bool CheckUser()
        {
            if (helper.SHA256Encrypt(getCpu()+ helper.SHA256Encrypt(GetDiskVolumeSerialNumber()+ helper.SHA256Encrypt(GetMACAddress()))).Equals(GlobalBindingHelper.UserMachineId))
            {
                return true;
            }
            return false;
        }

        private bool hasclick = false;//用户是否已经请求
        private void UserLogin()
        {
            //检查是否是指定的电脑
            ThrowMsg("正在检查硬件...", false);
            //if (!CheckUser())
            //{
            //    ThrowMsg("您的电脑不能登录，请联系管理员！", true);
            //    return;
            //}
            string UserName = login.tb_UseName.Text.Trim();
            string UserPassword = login.pb_Password.Password.Trim();
            //var sb = login.FindResource("loginClick") as Storyboard;
            if (!CheckStatue() || hasclick)
                return;
            hasclick = true;//用户是否已经请求
            //if (TempPwd != UserPassword)
            //{
            //    TempLength = UserPassword.Length;
            //    this.MD5Password = helper.StringToMd5(UserPassword);
            //}
            TempPwd = UserPassword;
            TempLength = UserPassword.Length;
            this.MD5Password = helper.StringToMd5(UserPassword);

            //SQLiteService server = new SQLiteService();
            //if (!server.CheckUser(UserName))
            //{ ThrowMsg("您本机登陆的账户不能为该账户", true); hasclick = false; return; }

            ThrowMsg("正在登录...", false);
            this.btn_login.IsEnabled = false;
            //InfoPlatformServiceSoapClient client = new InfoPlatformServiceSoapClient();
            //try
            //{
            //    UserEntity entity = client.UserLogin(UserName, this.MD5Password, SQLiteService.GetLocalIP());
            //    // int flag = client.UserLogin(UserName,UsingMD5(UserPassword));
            //    DealWithLogin(entity);
            //}
            //catch (Exception ex) { client.Abort(); }
            if (CheckLogin(UserName,MD5Password,"first"))
            {
                ThrowMsg("登录成功，正在加载数据...", false);
                //加载数据，暂时无用
                GlobalBindingHelper.Name = UserName;

                //加载数据结束，打开主窗口
                App app = (App)Application.Current;
                MainShell ms = new MainShell();
                app.MainWindow = ms;
                ms.Show();
                login.Close();
            }
            else
            {
                ThrowMsg("用户名或者密码错误！", true);
                login.tb_UseName.Focus();
                hasclick = false;//用户是否已经请求
                this.btn_login.IsEnabled = true;
                return;
            }
        }

        private bool CheckLogin(string userName,string password,string dbFile)
        {
            try
            {
                bool isSuccess = false;
                string sql = "select * from User where name='" + userName + "' and password = '" + password + "'";
                using (SQLiteConnection sc1 = new SQLiteConnection(string.Format(SQLiteService.connectionFormat, dbFile)))
                {
                    SQLiteCommand sCom = new SQLiteCommand(sql, sc1);
                    sc1.Open();
                    using (SQLiteDataReader dr1 = sCom.ExecuteReader())
                    {
                        if (dr1.Read())
                        {
                            isSuccess = true;
                        }
                        dr1.Close();
                    }
                    sc1.Close();
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                    
                throw ex;
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

        /// <summary>
        /// 状态检测【易神】
        /// </summary>
        /// <param name="_currentName"></param>
        /// <param name="_currentPwd"></param>
        bool CheckStatue()
        {
            string _currentPwd = this.pb_Password.Password;
            string _currentName = this.tb_UseName.Text.Trim();
            if (string.IsNullOrEmpty(_currentName) && string.IsNullOrEmpty(_currentPwd))
            {
                ThrowMsg("请先输入用户名和密码！", true);
                this.tb_UseName.Focus();
                return false;
            }
            else
            {
                if (string.IsNullOrEmpty(_currentPwd))
                {
                    ThrowMsg("请先输入密码！", true);
                    this.pb_Password.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(_currentName))
                {
                    ThrowMsg("请先输入用户名！", true);
                    this.tb_UseName.Focus();
                    return false;
                }
            }
            return true;
        }

        void pb_Password_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    //CheckExist();
                    if (btn_login.IsEnabled)
                        UserLogin();
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 获得CPU的序列号
        /// </summary>
        /// <returns></returns>
        public string getCpu()
        {
            try
            {
                string strCpu = null;
                ManagementClass myCpu = new ManagementClass("win32_Processor");
                ManagementObjectCollection myCpuConnection = myCpu.GetInstances();
                foreach (ManagementObject myObject in myCpuConnection)
                {
                    strCpu = myObject.Properties["Processorid"].Value.ToString();
                    break;
                }
                return strCpu;
            }
            catch (Exception)
            {
                return "CPU";
                //throw;
            }
        }

        /// <summary>
        /// 取得设备硬盘的卷标号
        /// </summary>
        /// <returns></returns>
        public string GetDiskVolumeSerialNumber()
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
                disk.Get();
                return disk.GetPropertyValue("VolumeSerialNumber").ToString();
            }
            catch (Exception)
            {
                return "DISK";
                //throw;
            }
        }

        public string GetMACAddress()
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();

                string MACAddress = String.Empty;

                foreach (ManagementObject mo in moc)
                {
                    if (MACAddress == String.Empty)
                    { // only return MAC Address from first card
                        if ((bool)mo["IPEnabled"] == true) MACAddress = mo["MacAddress"].ToString();
                    }
                    mo.Dispose();
                }

                return MACAddress;
            }
            catch (Exception)
            {
                return "MAC";
                //throw;
            }
        }
    }
}
