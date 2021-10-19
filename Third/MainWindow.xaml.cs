using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using Microsoft.Win32;
using static System.String;
using Color = System.Drawing.Color;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using Point = System.Windows.Point;

namespace Third
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.KeyDown += OnKeyDown;

            FrontBrush();
            TopBrush();
            RightBrush();
            LeftBrush();
            BackBrush();
            BottomBrush();

            // DoubleAnimation rotate = new DoubleAnimation();
            // rotate.From = 0;
            // rotate.To = 360;
            // rotate.Duration = TimeSpan.FromSeconds(6);
            // // rotate.RepeatBehavior=new RepeatBehavior(2.5);
            // rotate.RepeatBehavior = RepeatBehavior.Forever;
            // rotate3D.Axis = new Vector3D(0,1,1);
            // rotate3D.Angle = 60;
            // rotate3D.BeginAnimation(AxisAngleRotation3D.AngleProperty, rotate);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    rot_x.Angle -= 10;
                    break;
                case Key.Right:
                    rot_x.Angle += 10;
                    break;
                case Key.Up:
                    rot_z.Angle += 10;
                    break;
                case Key.Down:
                    rot_z.Angle -= 10;
                    break;
            }
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
    }
}