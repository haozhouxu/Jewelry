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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace First
{
    /// <summary>
    /// WinSecond.xaml 的交互逻辑
    /// </summary>
    public partial class WinSecond : Window
    {
        public object pageContext = null;

        #region 构造函数
        public WinSecond()
        {
            InitializeComponent();
        }

        public WinSecond(object _pageContext)
            : this()
        {
            pageContext = _pageContext;
            this.fra_second.Navigated += fra_second_Navigated;
        }

        #endregion

        #region 放大缩小
        private void OnCloseWindow(object target, ExecutedRoutedEventArgs e)
        {
            //App.Current.Shutdown();
            SystemCommands.CloseWindow(this);
        }

        private void OnMaximizedWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        private void OnMinimizedWindow(object target, ExecutedRoutedEventArgs e)
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

        #region 窗口事件
        void fra_second_Navigated(object sender, NavigationEventArgs e)
        {
            FrameworkElement fe = e.Content as FrameworkElement;
            if (fe != null)
            {
                fe.DataContext = e.ExtraData;
                this.Title = (string)fe.GetValue(Page.TitleProperty);
            }
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CC.ShowDetail(sender, e);
        }

        #endregion
    }
}
