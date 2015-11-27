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
using ManagementSystem.ViewModels;

namespace ManagementSystem.Views
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

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CC.Open_Executed(sender, e);
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Configure the message box to be displayed
            string messageBoxText = "你确定要退出?";
            string caption = "提示框";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;

            // Display message box
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    // User pressed Yes button
                    // ...
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    // User pressed No button
                    // ...
                    break;
                case MessageBoxResult.Cancel:
                    // User pressed Cancel button
                    // ...
                    break;
            }

        }
    }
}
