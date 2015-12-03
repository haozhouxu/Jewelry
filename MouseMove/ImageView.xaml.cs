using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MouseMove
{
    /// <summary>
    /// ImageView.xaml 的交互逻辑
    /// </summary>
    public partial class ImageView : Window
    {
        public ImageView()
        {
            InitializeComponent();
            this.Loaded += ImageView_Loaded;
            CreateImage(@"C:/xhz/Koala.jpg");
        }

        public const double BORDER_SIZE = 1;

        public static readonly DependencyProperty ResizeDirectionsProperty =
          DependencyProperty.Register("ResizeDirections", typeof(ResizeDirectionFlags?), typeof(ImageView), new PropertyMetadata(null));


        private ResizeDirectionFlags? storedResizeDirections;

        /// <summary>
        /// Set the Resize Directions
        /// </summary>
        public ResizeDirectionFlags? ResizeDirections
        {
            get { return (ResizeDirectionFlags)GetValue(ResizeDirectionsProperty); }
            set
            {
                var oldValue = (ResizeDirectionFlags?)GetValue(ResizeDirectionsProperty);

                // Compare it to the previous value
                if (oldValue != value)
                {
                    // Set the property
                    SetValue(ResizeDirectionsProperty, value);
                    // ReSharper disable PossibleNullReferenceException
                    //UpdateBorders(value);

                    // Adjust the outer border
                    //var border = (Border)GetTemplateChild("m_edgeBorder");
                    Thickness thickness = new Thickness(
                        (value & ResizeDirectionFlags.SizeW) == ResizeDirectionFlags.SizeW ? BORDER_SIZE : 0.0,
                        (value & ResizeDirectionFlags.SizeN) == ResizeDirectionFlags.SizeN ? BORDER_SIZE : 0.0,
                        (value & ResizeDirectionFlags.SizeE) == ResizeDirectionFlags.SizeE ? BORDER_SIZE : 0.0,
                        (value & ResizeDirectionFlags.SizeS) == ResizeDirectionFlags.SizeS ? BORDER_SIZE : 0.0);
                    //border.Margin = thickness;

                    //// Now invert the inner border to 
                    //border = (Border)GetTemplateChild("INNER_BORDER");
                    //border.Margin = thickness;

                    // ReSharper restore PossibleNullReferenceException
                }
            }
        }



        public ImageView(string imgPath)
        {
            InitializeComponent();
            this.Loaded += ImageView_Loaded;
            CreateImage(imgPath);
            //this.img.Source = ImageHelper.GetTemplatePhotoThumbnail(imgPath, this.img.ActualWidth);
        }

        //public ImageView(ObservableCollection<AttachmentEntity> source, string current)
        //{
        //    InitializeComponent();
        //    this.Loaded += ImageView_Loaded;
        //    this.ic.ItemsSource = source;
        //    CreateImage(current);
        //}

        void ImageView_Loaded(object sender, RoutedEventArgs e)
        {
            if (GetValue(ResizeDirectionsProperty) == null)
                ResizeDirections =
                    ResizeDirectionFlags.SizeN |
                    ResizeDirectionFlags.SizeS |
                    ResizeDirectionFlags.SizeW |
                    ResizeDirectionFlags.SizeE |
                    ResizeDirectionFlags.SizeSE |
                    ResizeDirectionFlags.SizeNW |
                    ResizeDirectionFlags.SizeSW |
                    ResizeDirectionFlags.SizeNE;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            var sb = this.FindResource("leave") as Storyboard;
            sb.Completed += sb_Completed;
            sb.Begin();
        }

        void sb_Completed(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void UpdateBorders(ResizeDirectionFlags? value)
        //{
        //    // ReSharper disable PossibleNullReferenceException
        //    PART_SizeN.IsEnabled = (value & ResizeDirectionFlags.SizeN) == ResizeDirectionFlags.SizeN;
        //    PART_SizeS.IsEnabled = (value & ResizeDirectionFlags.SizeS) == ResizeDirectionFlags.SizeS;
        //    PART_SizeW.IsEnabled = (value & ResizeDirectionFlags.SizeW) == ResizeDirectionFlags.SizeW;
        //    PART_SizeE.IsEnabled = (value & ResizeDirectionFlags.SizeE) == ResizeDirectionFlags.SizeE;
        //    PART_SizeSE.IsEnabled = (value & ResizeDirectionFlags.SizeSE) == ResizeDirectionFlags.SizeSE;
        //    PART_SizeNW.IsEnabled = (value & ResizeDirectionFlags.SizeNW) == ResizeDirectionFlags.SizeNW;
        //    PART_SizeSW.IsEnabled = (value & ResizeDirectionFlags.SizeSW) == ResizeDirectionFlags.SizeSW;
        //    PART_SizeNE.IsEnabled = (value & ResizeDirectionFlags.SizeNE) == ResizeDirectionFlags.SizeNE;
        //    // ReSharper restore PossibleNullReferenceException
        //}

        //private void OnDragDelta(object sender, DragDeltaEventArgs e)
        //{
        //    //ImageView transparentWindow = (ImageView)sender;
        //    ImageView transparentWindow = this;
        //    Thumb thumb = e.OriginalSource as Thumb;
        //    if (thumb != null && transparentWindow.WindowState == WindowState.Normal)
        //    {
        //        Rect windowRect = new Rect(transparentWindow.Left, transparentWindow.Top, transparentWindow.ActualWidth, transparentWindow.ActualHeight);
        //        double maxReducedHeight = Math.Max(0, transparentWindow.ActualHeight - transparentWindow.MinHeight);
        //        double maxReducedWidth = Math.Max(0, transparentWindow.ActualWidth - transparentWindow.MinWidth);
        //        double reducedHeight = e.VerticalChange;
        //        double reducedWidth = e.HorizontalChange;
        //        if (thumb.Name.Equals("PART_SizeSE") && (transparentWindow.ResizeDirections & ResizeDirectionFlags.SizeSE) == ResizeDirectionFlags.SizeSE)
        //        {
        //            reducedHeight = Math.Max(reducedHeight, -maxReducedHeight);
        //            reducedWidth = Math.Max(reducedWidth, -maxReducedWidth);
        //            transparentWindow.Width = Math.Max(transparentWindow.ActualWidth + reducedWidth, transparentWindow.MinWidth);
        //            transparentWindow.Height = Math.Max(transparentWindow.ActualHeight + reducedHeight, transparentWindow.MinHeight);
        //        }
        //        else if (thumb.Name.Equals("PART_SizeNW") && (transparentWindow.ResizeDirections & ResizeDirectionFlags.SizeNW) == ResizeDirectionFlags.SizeNW)
        //        {
        //            reducedWidth = Math.Min(reducedWidth, maxReducedWidth);
        //            reducedHeight = Math.Min(reducedHeight, maxReducedHeight);
        //            windowRect.Y += reducedHeight;
        //            windowRect.X += reducedWidth;
        //            windowRect.Width -= reducedWidth;
        //            windowRect.Height -= reducedHeight;
        //            transparentWindow.SetWindowVisualRect(windowRect);
        //        }
        //        else if (thumb.Name.Equals("PART_SizeSW") && (transparentWindow.ResizeDirections & ResizeDirectionFlags.SizeSW) == ResizeDirectionFlags.SizeSW)
        //        {
        //            reducedHeight = Math.Max(reducedHeight, -maxReducedHeight);
        //            reducedWidth = Math.Min(reducedWidth, maxReducedWidth);
        //            windowRect.X += reducedWidth;
        //            windowRect.Width -= reducedWidth;
        //            windowRect.Height += reducedHeight;
        //            transparentWindow.SetWindowVisualRect(windowRect);
        //        }
        //        else if (thumb.Name.Equals("PART_SizeNE") && (transparentWindow.ResizeDirections & ResizeDirectionFlags.SizeNE) == ResizeDirectionFlags.SizeNE)
        //        {
        //            reducedHeight = Math.Min(reducedHeight, maxReducedHeight);
        //            reducedWidth = Math.Max(reducedWidth, -maxReducedWidth);
        //            windowRect.Y += reducedHeight;
        //            windowRect.Height = transparentWindow.ActualHeight - reducedHeight;
        //            windowRect.Width = transparentWindow.ActualWidth + reducedWidth;
        //            transparentWindow.SetWindowVisualRect(windowRect);
        //        }
        //        else if (thumb.Name.Equals("PART_SizeN") && (transparentWindow.ResizeDirections & ResizeDirectionFlags.SizeN) == ResizeDirectionFlags.SizeN)
        //        {
        //            reducedHeight = Math.Min(reducedHeight, maxReducedHeight);
        //            windowRect.Y += reducedHeight;
        //            windowRect.Height = transparentWindow.ActualHeight - reducedHeight;
        //            transparentWindow.SetWindowVisualRect(windowRect);
        //        }
        //        else if (thumb.Name.Equals("PART_SizeS") && (transparentWindow.ResizeDirections & ResizeDirectionFlags.SizeS) == ResizeDirectionFlags.SizeS)
        //        {
        //            reducedHeight = Math.Max(reducedHeight, -maxReducedHeight);
        //            transparentWindow.Height = transparentWindow.ActualHeight + reducedHeight;
        //        }
        //        else if (thumb.Name.Equals("PART_SizeW") && (transparentWindow.ResizeDirections & ResizeDirectionFlags.SizeW) == ResizeDirectionFlags.SizeW)
        //        {
        //            reducedWidth = Math.Min(reducedWidth, maxReducedWidth);
        //            windowRect.X += reducedWidth;
        //            windowRect.Width = transparentWindow.ActualWidth - reducedWidth;
        //            transparentWindow.SetWindowVisualRect(windowRect);
        //        }
        //        else if (thumb.Name.Equals("PART_SizeE") && (transparentWindow.ResizeDirections & ResizeDirectionFlags.SizeE) == ResizeDirectionFlags.SizeE)
        //        {
        //            reducedWidth = Math.Max(reducedWidth, -maxReducedWidth);
        //            transparentWindow.Width = transparentWindow.ActualWidth + reducedWidth;
        //        }
        //    }
        //}

        [Flags]
        public enum ResizeDirectionFlags
        {
            /// <summary> Not Resizeable </summary>
            None = 0,

            /// <summary> Sizeable to the North </summary>
            SizeN = 1,

            /// <summary> Sizeable to the South </summary>
            SizeS = 2,

            /// <summary> Sizeable to the West </summary>
            SizeW = 4,

            /// <summary> Sizeable to the East </summary>
            SizeE = 8,

            /// <summary> Sizeable to the South/East </summary>
            SizeSE = 16,

            /// <summary> Sizeable to the North/West </summary>
            SizeNW = 32,

            /// <summary> Sizeable to the South/West </summary>
            SizeSW = 64,

            /// <summary> Sizeable to the North/East </summary>
            SizeNE = 128,
        }


        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

        protected virtual void SetWindowVisualRect(Rect rect)
        {
            IntPtr mainWindowPtr = new WindowInteropHelper(this).Handle;
            HwndSource mainWindowSrc = HwndSource.FromHwnd(mainWindowPtr);
            Point deviceTopLeft = mainWindowSrc.CompositionTarget.TransformToDevice.Transform(rect.TopLeft);
            Point deviceBottomRight = mainWindowSrc.CompositionTarget.TransformToDevice.Transform(rect.BottomRight);
            SetWindowPos(mainWindowSrc.Handle,
                         IntPtr.Zero,
                         (int)(deviceTopLeft.X),
                         (int)(deviceTopLeft.Y),
                         (int)(Math.Abs(deviceBottomRight.X - deviceTopLeft.X)),
                         (int)(Math.Abs(deviceBottomRight.Y - deviceTopLeft.Y)),
                         0);
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    var _current = (sender as Button).DataContext as AttachmentEntity;
        //    CreateImage(_current.LocalPath);
        //}

        void CreateImage(string path)
        {
            try
            {
                //var client = new WebClient();
                //client.Proxy = null;
                //client.DownloadDataCompleted += (s, e) =>
                // {
                //     BitmapImage _bmp = new BitmapImage();
                //     _bmp.BeginInit();
                //     _bmp.StreamSource = new MemoryStream(e.Result);
                //     _bmp.EndInit();
                //     if (_bmp.CanFreeze) _bmp.Freeze();
                //     this.img.Source = _bmp;
                //     if (client != null)
                //     {
                //         client.Dispose();
                //         client = null;
                //     }
                // };
                //client.DownloadDataAsync(new Uri(path));
                BitmapImage _bmp = new BitmapImage();
                _bmp.BeginInit();
                _bmp.UriSource = new Uri(path);
                _bmp.EndInit();
                if (_bmp.CanFreeze) _bmp.Freeze();
                this.img.Source = _bmp;
            }
            catch
            {

            }
        }

        private void img_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                stf.CenterX = 500.0;
                stf.CenterY = 500.0;
                if (stf.ScaleX <= 1.5)
                {
                    stf.ScaleX += 0.1;
                    stf.ScaleY += 0.1;
                }
            }
            else
            {
                if (stf.ScaleX >= 0.2)
                {
                    stf.ScaleX -= 0.1;
                    stf.ScaleY -= 0.1;
                }
            }
            this.tbPercent.Text = string.Format("{0}%", ((stf.ScaleX / 1.0) * 100).ToString("f0"));
        }

        private void img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        
            //var sb = this.FindResource("leave") as Storyboard;
            //sb.Completed += sb_Completed;
            //sb.Begin();
        }

        public static BitmapSource GetTemplatePhotoThumbnail(string str_Path, double zoom)
        {
            BitmapSource result;
            using (FileStream fileStream = new FileStream(str_Path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                BitmapFrame bitmapFrame = BitmapFrame.Create(fileStream, BitmapCreateOptions.DelayCreation, BitmapCacheOption.None);
                if (null != bitmapFrame.Thumbnail)
                {
                    result = bitmapFrame.Thumbnail;
                    fileStream.Flush();
                    fileStream.Close();
                    fileStream.Dispose();
                    return result;
                }
            }
            result = GetScaleBitmapSource(str_Path, zoom, zoom);

            return result;
        }

        public static BitmapSource GetScaleBitmapSource(string str_Path, double width, double height)
        {
            BitmapFrame bitmapFrame = null;
            BitmapSource result;
            try
            {
                BitmapImage bitmapImage = new BitmapImage();
                using (FileStream fileStream = new FileStream(str_Path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
                    if (width > height)
                    {
                        bitmapImage.DecodePixelWidth = (int)width;
                    }
                    else
                    {
                        bitmapImage.DecodePixelHeight = (int)height;
                    }
                    bitmapImage.StreamSource = fileStream;
                    bitmapImage.EndInit();
                    bitmapImage.Freeze();
                    fileStream.Flush();
                    fileStream.Close();
                    fileStream.Dispose();
                }
                result = bitmapImage;
                return result;
            }
            catch
            {
            }
            finally
            {
                if (null != bitmapFrame)
                {
                    bitmapFrame = null;
                }
            }
            result = null;
            return result;
        }
    }

    public class MinImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
                return ImageView.GetTemplatePhotoThumbnail(value.ToString(), 50);
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
