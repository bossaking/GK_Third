using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Double;
using Color = System.Drawing.Color;
using Point = System.Windows.Point;
using Size = System.Windows.Size;

namespace Third
{
    public partial class MainWindow
    {
        
        private Bitmap _colorPicker;
        private bool _changeFlag;
        
        public MainWindow()
        {
            InitializeComponent();
            CubeSetup();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetImageToBitmap();
            RgbToCmyk();
        }
        
        private void Red_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_changeFlag) return;
            if (!string.IsNullOrWhiteSpace(Red.Text))
            {
                var value = int.Parse(Red.Text);
                if (value > 255)
                {
                    Red.Text = "255";
                }
                else
                {
                    RgbToCmyk();
                }
            }
            else
            {
                Red.Text = "0";
            }
        }

        private void Green_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_changeFlag) return;
            if (!string.IsNullOrWhiteSpace(Green.Text))
            {
                var value = int.Parse(Green.Text);
                if (value > 255)
                {
                    Green.Text = "255";
                }
                else
                {
                    RgbToCmyk();
                }
            }
            else
            {
                Green.Text = "0";
            }
        }

        private void Blue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_changeFlag) return;
            if (!string.IsNullOrWhiteSpace(Blue.Text))
            {
                var value = int.Parse(Blue.Text);
                if (value > 255)
                {
                    Blue.Text = "255";
                }
                else
                {
                    RgbToCmyk();
                }
            }
            else
            {
                Blue.Text = "0";
            }
        }

        private void Cyan_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_changeFlag) return;
            if (!string.IsNullOrWhiteSpace(Cyan.Text))
            {
                var value = int.Parse(Cyan.Text);
                if (value > 100)
                {
                    Cyan.Text = "100";
                }
                else
                {
                    CmykToRgb();
                }
            }
            else
            {
                Cyan.Text = "0";
            }
        }

        private void Magenta_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_changeFlag) return;
            if (!string.IsNullOrWhiteSpace(Magenta.Text))
            {
                var value = int.Parse(Magenta.Text);
                if (value > 100)
                {
                    Magenta.Text = "100";
                }
                else
                {
                    CmykToRgb();
                }
            }
            else
            {
                Magenta.Text = "0";
            }
        }

        private void Yellow_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_changeFlag) return;
            if (!string.IsNullOrWhiteSpace(Yellow.Text))
            {
                var value = int.Parse(Yellow.Text);
                if (value > 100)
                {
                    Yellow.Text = "100";
                }
                else
                {
                    CmykToRgb();
                }
            }
            else
            {
                Yellow.Text = "0";
            }
        }

        private void Black_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_changeFlag) return;
            if (!string.IsNullOrWhiteSpace(Black.Text))
            {
                var value = int.Parse(Black.Text);
                if (value > 100)
                {
                    Black.Text = "100";
                }
                else
                {
                    CmykToRgb();
                }
            }
            else
            {
                Black.Text = "0";
            }
        }

        private void TextBoxNumberTypeValidation(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void RgbToCmyk()
        {
            if (Red == null || Green == null || Blue == null || Black == null || Cyan == null || Magenta == null ||
                Yellow == null) return;
            _changeFlag = false;
            var r = Parse(Red.Text) / 255;
            var g = Parse(Green.Text) / 255;
            var b = Parse(Blue.Text) / 255;

            var k = 1 - Math.Max(Math.Max(r, g), b);

            Cyan.Text = Math.Abs(k - 1) == 0 ? "0" : Math.Round((1 - r - k) / (1 - k) * 100).ToString(CultureInfo.InvariantCulture);
            Magenta.Text = Math.Abs(k - 1) == 0 ? "0" : Math.Round((1 - g - k) / (1 - k) * 100).ToString(CultureInfo.InvariantCulture);
            Yellow.Text = Math.Abs(k - 1) == 0 ? "0" : Math.Round((1 - b - k) / (1 - k) * 100).ToString(CultureInfo.InvariantCulture);
            Black.Text = Math.Round(k * 100).ToString(CultureInfo.InvariantCulture);
            _changeFlag = true;
            ShowSelectedColor();
        }

        private void CmykToRgb()
        {
            if (Red == null || Green == null || Blue == null || Black == null || Cyan == null || Magenta == null ||
                Yellow == null) return;
            _changeFlag = false;
            var c = Parse(Cyan.Text) / 100;
            var m = Parse(Magenta.Text) / 100;
            var y = Parse(Yellow.Text) / 100;
            var k = Parse(Black.Text) / 100;

            Red.Text = Math.Round(255 * (1 - c) * (1 - k)).ToString(CultureInfo.InvariantCulture);
            Green.Text = Math.Round(255 * (1 - m) * (1 - k)).ToString(CultureInfo.InvariantCulture);
            Blue.Text = Math.Round(255 * (1 - y) * (1 - k)).ToString(CultureInfo.InvariantCulture);
            _changeFlag = true;
            ShowSelectedColor();
        }

        private void ShowSelectedColor()
        {
            ActualColor.Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, (byte)int.Parse(Red.Text), (byte)int.Parse(Green.Text), (byte)int.Parse(Blue.Text)));
        }

        private void ColorPicker_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Cross;
        }

        private void ColorPicker_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void ColorPicker_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;
            var position = e.GetPosition(ColorPicker);
            GetColorFromColorPicker(position);
        }

        private void ColorPicker_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;
            var position = e.GetPosition(ColorPicker);
            GetColorFromColorPicker(position);
        }

        private void GetColorFromColorPicker(Point position)
        {
            var actualWidth = ColorPicker.ActualWidth;
            var actualHeight = ColorPicker.ActualHeight;

            if (!(position.X < actualWidth) || !(position.Y < actualHeight)) return;
            double width = _colorPicker.Width;
            double height = _colorPicker.Height;

            var x = Math.Floor(width * position.X / actualWidth);
            var y = Math.Floor(height * position.Y / actualHeight);

            var color = _colorPicker.GetPixel((int)x, (int)y);

            Red.Text = color.R.ToString();
            Green.Text = color.G.ToString();
            Blue.Text = color.B.ToString();
        }

        private void GetImageToBitmap()
        {
            var actualWidth = (int)ColorPicker.ActualWidth;
            var actualHeight = (int)ColorPicker.ActualHeight;

            var rtb = new RenderTargetBitmap(actualWidth, actualHeight, 96, 96, PixelFormats.Pbgra32);

            var size = new Size(actualWidth, actualHeight);

            ColorPicker.Measure(size);
            ColorPicker.Arrange(new Rect(size));

            rtb.Render(ColorPicker);

            var encoder = new PngBitmapEncoder();
            var stream = new MemoryStream();

            encoder.Frames.Add(BitmapFrame.Create(rtb));

            encoder.Save(stream);
            _colorPicker = new Bitmap(stream);
        }

        #region Cube
        
        private void CubeSetup()
        {
            FrontBrush();
            TopBrush();
            RightBrush();
            LeftBrush();
            BackBrush();
            BottomBrush();
        }

        private void FrontBrush()
        {
            var bitmap = new Bitmap(256, 256);
            for (var i = 0; i < 256; i++)
            {
                for (var j = 0; j < 256; j++)
                {
                    bitmap.SetPixel(j, i, Color.FromArgb(255 - i, 255, 0 + j));
                }
            }

            var ms = new MemoryStream();
            (bitmap).Save(ms, ImageFormat.Bmp);
            var image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            var ib = new ImageBrush {ImageSource = image};
            Front.Brush = ib;
        }

        private void TopBrush()
        {
            var bitmap = new Bitmap(256, 256);
            for (var i = 0; i < 256; i++)
            {
                for (var j = 0; j < 256; j++)
                {
                    bitmap.SetPixel(j, i, Color.FromArgb(255, 0 + i, 0 + j));
                }
            }

            var ms = new MemoryStream();
            (bitmap).Save(ms, ImageFormat.Bmp);
            var image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            var ib = new ImageBrush {ImageSource = image};
            Top.Brush = ib;
        }

        private void RightBrush()
        {
            var bitmap = new Bitmap(256, 256);
            for (var i = 0; i < 256; i++)
            {
                for (var j = 0; j < 256; j++)
                {
                    bitmap.SetPixel(j, i, Color.FromArgb(255 - i, 255 - j, 255));
                }
            }

            var ms = new MemoryStream();
            (bitmap).Save(ms, ImageFormat.Bmp);
            var image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            var ib = new ImageBrush {ImageSource = image};
            Right.Brush = ib;
        }

        private void LeftBrush()
        {
            var bitmap = new Bitmap(256, 256);
            for (var i = 0; i < 256; i++)
            {
                for (var j = 0; j < 256; j++)
                {
                    bitmap.SetPixel(j, i, Color.FromArgb(255 - i, 0 + j, 0));
                }
            }

            var ms = new MemoryStream();
            (bitmap).Save(ms, ImageFormat.Bmp);
            var image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            var ib = new ImageBrush {ImageSource = image};
            Left.Brush = ib;
        }

        private void BackBrush()
        {
            var bitmap = new Bitmap(256, 256);
            for (var i = 0; i < 256; i++)
            {
                for (var j = 0; j < 256; j++)
                {
                    bitmap.SetPixel(j, i, Color.FromArgb(255 - i, 0, 255 - j));
                }
            }

            var ms = new MemoryStream();
            (bitmap).Save(ms, ImageFormat.Bmp);
            var image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            var ib = new ImageBrush {ImageSource = image};
            Back.Brush = ib;
        }

        private void BottomBrush()
        {
            var bitmap = new Bitmap(256, 256);
            for (var i = 0; i < 256; i++)
            {
                for (var j = 0; j < 256; j++)
                {
                    bitmap.SetPixel(j, i, Color.FromArgb(0, 255 - i, 0 + j));
                }
            }

            var ms = new MemoryStream();
            (bitmap).Save(ms, ImageFormat.Bmp);
            var image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            var ib = new ImageBrush {ImageSource = image};
            Bottom.Brush = ib;
        }
        
        #endregion
    }
}