using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace student
{
    /// <summary>
    /// Add.xaml 的交互逻辑
    /// </summary>
    public partial class Add : Window
    {
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
                this.image.Source = new BitmapImage(new Uri(ofd.FileName, UriKind.Absolute));
            }
        }
    }
}
