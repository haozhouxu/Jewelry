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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using First.Helper;

namespace First.View
{
    /// <summary>
    /// setting.xaml 的交互逻辑
    /// </summary>
    public partial class setting : Page
    {
        public setting()
        {
            InitializeComponent();
        }

        private void Ok_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!CheckStatue())
                {
                    return;
                }
                string OldPwd = this.Oldpw.Password.Trim();
                string NewPwd = this.Newpw1.Password.Trim();
                string User = GlobalBindingHelper.Name;
                if (CheckPwd(User, helper.StringToMd5(OldPwd), "first"))
                {
                    if (UpdataPwd(User, helper.StringToMd5(OldPwd), helper.StringToMd5(NewPwd), "first"))
                    {
                        ThrowMsg("更新成功，下次请使用新密码登陆！", false);
                        this.Oldpw.Clear();
                        this.Newpw1.Clear();
                        this.Newpw2.Clear();
                        this.Oldpw.Focus();
                        return;
                    }
                    else
                    {
                        ThrowMsg("更新失败，请喝管理员联系！", true);
                        return;
                    }
                }
                else
                {
                    ThrowMsg("旧密码不正确，请重新输入！", true);
                    this.Oldpw.Focus();
                    return;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool UpdataPwd(string userName,string oldPassword ,string newpassword, string dbFile)
        {
            try
            {
                bool isSuccess = false;
                //定义temp存储影响行数
                int temp = 0;
                string sql = "update User set password = '"+ newpassword + "' where name='" + userName + "' and password = '" + oldPassword + "'";
                using (SQLiteConnection sc1 = new SQLiteConnection(string.Format(SQLiteService.connectionFormat, dbFile)))
                {
                    SQLiteCommand sCom = new SQLiteCommand(sql, sc1);
                    sc1.Open();
                    temp = sCom.ExecuteNonQuery();
                    sc1.Close();
                }
                if (temp > 0)
                {
                    isSuccess = true;
                }
                return isSuccess;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool CheckPwd(string userName, string password, string dbFile)
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

        bool CheckStatue()
        {
            string _currentOldPwd = this.Oldpw.Password.Trim();
            string _currentPwd1 = this.Newpw1.Password.Trim();
            string _currentPwd2 = this.Newpw2.Password.Trim();
            if (string.IsNullOrEmpty(_currentOldPwd) && string.IsNullOrEmpty(_currentPwd1) && string.IsNullOrEmpty(_currentPwd2))
            {
                ThrowMsg("请先输入旧密码和新密码！", true);
                this.Oldpw.Focus();
                return false;
            }
            else
            {
                if (string.IsNullOrEmpty(_currentOldPwd))
                {
                    ThrowMsg("请先输入旧密码！", true);
                    this.Oldpw.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(_currentPwd1))
                {
                    ThrowMsg("请先输入新密码！", true);
                    this.Newpw1.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(_currentPwd2))
                {
                    ThrowMsg("请再次输入新密码！", true);
                    this.Newpw2.Focus();
                    return false;
                }
                if (!_currentPwd1.Equals(_currentPwd2))
                {
                    ThrowMsg("新密码不一致，请重新输入！", true);
                    this.Newpw1.Focus();
                    return false;
                }
            }
            return true;
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            this.Oldpw.Clear();
            this.Newpw1.Clear();
            this.Newpw2.Clear();
            this.Oldpw.Focus();
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
    }
}
