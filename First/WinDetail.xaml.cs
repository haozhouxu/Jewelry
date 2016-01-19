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

namespace First
{
    /// <summary>
    /// WinDetail.xaml 的交互逻辑
    /// </summary>
    public partial class WinDetail : Window
    {
        public object _pageContext = null;

        #region 构造函数
        public WinDetail()
        {
            InitializeComponent();
        }

        public WinDetail(object pageContext)
            : this()
        {
            this._pageContext = pageContext;
        }

        public WinDetail(object pageContext, bool isEdit)
            : this(pageContext)
        {
            if (isEdit)
                sp_edit.Visibility = System.Windows.Visibility.Visible;
        }
        public WinDetail(object pageContext, bool isEdit,string title)
            : this(pageContext)
        {
            if (isEdit)
                sp_edit.Visibility = System.Windows.Visibility.Visible;
            if (!string.IsNullOrEmpty(title))
                this.Title = title;
        }
        #endregion

        #region 放大缩小
        private void OnCloseWindow(object target, ExecutedRoutedEventArgs e)
        {
            //App.Current.Shutdown();
            if (MessageBox.Show("您确定要退出该界面吗？", "系统提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                SystemCommands.CloseWindow(this);
        }

        private void OnMaximizeWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        private void OnMinimizeWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void OnRestoreWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);
        }

        private void OnCanResizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ResizeMode == ResizeMode.CanResize || this.ResizeMode == ResizeMode.CanResizeWithGrip;
        }

        private void OnCanMinimizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ResizeMode != ResizeMode.NoResize;
        }
        #endregion

        #region 方法
        public Frame PageFrame
        {
            get { return fra_content; }
        }

        private void bt_ok_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void bt_cancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("您确定要退出该界面吗？", "系统提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.DialogResult = false;
                this.Close();
            }
        }

        private void Close_Page(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void fra_content_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            FrameworkElement fe1 = e.Content as FrameworkElement;
            if (fe1 != null)
            {
                fe1.DataContext = _pageContext;
                //this.Title = (string)fe1.GetValue(Page.TitleProperty);
            }
        }
        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CC.Open_Executed(sender, e);
            //怎么判断新增成功后进行保存
            //this.Close();
        }
        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CC.Save_Executed(sender, e);
        }
        #endregion
    }
}
