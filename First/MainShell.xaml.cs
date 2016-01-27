using First.Model;
using First.View;
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
using System.Windows.Threading;
using System.Xml.Linq;
using First.Helper;

namespace First
{
    /// <summary>
    /// MainShell.xaml 的交互逻辑
    /// </summary>
    public partial class MainShell : Window
    {

        #region 公告提醒[易神]
        Page mainPage = null;
        //AlertWindow _alterWindow = null;
        bool hasClosePre = false;
        #endregion

        public MainShell()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                GotoPage(System.Configuration.ConfigurationManager.AppSettings["mainPageID"]);
                //string path = System.IO.Path.Combine(System.Environment.CurrentDirectory, @"Resources\Nav_catalog.xml");
                //xhz:2015-12-01 Nav_catalog.xml 需要设置为始终复制，设置为内容
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, @"Resources\Nav_catalog.xml");
                XElement xe = XElement.Load(path);
                List<SectionTitleCollection> list_catalog = Tools.LoadCatalog(xe.Element("catalog"));
                this.ic_nav.ItemsSource = list_catalog;
                this.fra_view.DataContext = list_catalog;
                //处理首页 直接导航的问题
                //this.fra_view.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
                //{
                //    hasClosePre = true;
                //    mainPage = this.fra_view.Content as Page;
                //    GetLatestNotices(mainPage);
                //}));

                ////新功能提示
                //if (bTip())
                //    CreateFolder();

                ////SectionTitle sect = new SectionTitle();
                ////list_catalog.ForEach(x => sect.Children.Add(x[0]));

                ////InitialdNav(sect.Children);

                //#region  公告提醒[易神]
                //DispatcherTimer _timer = new DispatcherTimer();
                //_timer.Tick += (ss, ee) =>
                //{
                //    GetLatestNotices(mainPage);
                //};
                //_timer.Interval = new TimeSpan(0, 10, 0);
                //_timer.Start();

                //#endregion
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 导航到指定的模块Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GotoPage(object sender, ExecutedRoutedEventArgs e)
        {

            //string id = (string)e.Parameter;
            //GotoPage(id);

            SectionTitle st1 = e.Parameter as SectionTitle;
            if (st1 != null)
            {
                //this.CurrentSectionTitle = st1;//临时变量重置的时候，会跟新界面。如果用GotoPage(st1),则会导致读取2次数据和界面
                GotoPage(st1);
            }
            else if (e.Parameter.GetType().Name.Equals("SectionTitleCollection"))
            {
                SectionTitleCollection stc = e.Parameter as SectionTitleCollection;
                InitalSelected(stc.First());//初始化导航，默认每一个导航的第一个导航选中。解决有首页跳转导致的bug
                GotoPage(stc.First().NavID);
            }
            else
                GotoPage((string)e.Parameter);
        }

        private void GotoPage(string id)
        {
            fra_view.Navigate(new Uri(id, UriKind.Absolute));
            fra_view.Refresh();
            //string mod = SQLiteService.getModule(id);
            //if (!string.IsNullOrEmpty(mod))
            //{
            //    currentModule = id;
            //    if (id.StartsWith("M0003") && GlobalBindingHelper.AppSettings["select_page"] != id)
            //    {
            //        GlobalBindingHelper.AppSettings["select_page"] = id;
            //    }
            //    FrameworkElement fe1 = null;
            //    //if (mod.Contains("ed:Arc"))//解析Arc的圆形控件
            //    //{
            //    //    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(mod);
            //    //    MemoryStream sStream = new MemoryStream(bytes);
            //    //    ParserContext pc = new ParserContext();
            //    //    pc.XamlTypeMapper = new XamlTypeMapper(new string[] { });
            //    //    pc.XamlTypeMapper.AddMappingProcessingInstruction("http://schemas.microsoft.com/expression/2010/drawing", "Microsoft.Expression.Shapes", "Microsoft.Expression.Drawing");
            //    //    pc.XmlnsDictionary.Add("ed", "http://schemas.microsoft.com/expression/2010/drawing");
            //    //    fe1 = System.Windows.Markup.XamlReader.Load(sStream, pc) as FrameworkElement;
            //    //}
            //    //else
            //    fe1 = System.Windows.Markup.XamlReader.Parse(mod) as FrameworkElement;
            //    if (fe1 != null)
            //    {
            //        //rdli 股东分析导航配置
            //        #region 异步添加日志
            //        try
            //        {
            //            string titleName = (string)fe1.GetValue(Page.TitleProperty);
            //            Action action = new Action(() =>
            //            {
            //                SQLiteService.InsertDailyLog(titleName);
            //            });
            //            action.BeginInvoke((res) => { }, null);
            //        }
            //        catch { }
            //        #endregion
            //        fra_view.Navigate(fe1);
            //    }
            //}
        }

        private void GotoPage(SectionTitle st1)
        {
            if (st1 != null && !string.IsNullOrEmpty(st1.NavID))
            {
                GotoPage(st1.NavID);
            }
        }

        private void InitalSelected(SectionTitle sc)
        {
            //主导航的时候，改变全局变量GotoPage
            switch (sc.Title)
            {
                case "全部货物":
                    GlobalBindingHelper.GotoPage = "全部";
                    break;
                case "借出货物":
                    GlobalBindingHelper.GotoPage = "借出";
                    break;
                case "卖出货物":
                    GlobalBindingHelper.GotoPage = "卖出";
                    break;
                case "现存货物":
                    GlobalBindingHelper.GotoPage = "未卖";
                    break;
                default:
                    break;
            }

            sc.Selected = true;
            if (sc.Children.Count == 0)
                return;
            sc.Children.ToList().ForEach(x => x.Selected = false);
            InitalSelected(sc.Children[0]);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbx = sender as ComboBox;
            cbx.SelectedIndex = -1;
        }

        private void fra_view_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //if (e.Content == null)//在法律法规内容  超链接的时候，会导致content为null的导航错误，所以需要屏蔽
            //    e.Cancel = true;
        }

        private void backdb_Selected(object sender, RoutedEventArgs e)
        {

            //string apppath2 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            //string datapath = Path.Combine(apppath2, @"genesysinfo\ican\userdata");

            //System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            //dialog.Description = "选择备份文件的路径";
            //System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            //if (result == System.Windows.Forms.DialogResult.OK)
            //{
            //    string backupPath = dialog.SelectedPath;
            //    string timeNow = System.DateTime.Now.ToShortDateString();
            //    string tempPath = Path.Combine(backupPath, timeNow);
            //    CopyData(datapath, tempPath);
            //    GenesysInfo.GZipCompression.GZipCompress.Compress(tempPath, Path.Combine(backupPath, "上市公司董秘工作平台" + timeNow + ".bak"));
            //    // 获得文件夹数组               
            //    System.IO.Directory.Delete(tempPath, true);
            //    WriteDate(DateTime.Now.ToString("yyyy-MM-dd"));
            //    MessageBox.Show("备份数据成功！请妥善保管好文件", "系统信息");
            //}
        }

        #region 关于界面
        private void about_Selected(object sender, RoutedEventArgs e)
        {
            AboutWindow win = new AboutWindow();
            win.ShowDialog();
        }
        #endregion

        #region 放大缩小
        private void OnCloseWindow(object target, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("您确定要退出程序吗？", "系统提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    //Action action = new Action(() =>
                    //{
                    //    SQLiteService.InsertDailyLog("退出系统");
                    //});
                    //action.BeginInvoke((res) => { }, null);
                    //System.Threading.Thread.Sleep(500);
                    App.Current.Shutdown();
                }
                catch
                {
                    App.Current.Shutdown();
                }
            }
            //SystemCommands.CloseWindow(this);
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

        private void CreateNew(object sender, ExecutedRoutedEventArgs e)
        {
            CC.CreateNew(sender, e);
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CC.Open_Executed(sender, e);
        }

    }
}
