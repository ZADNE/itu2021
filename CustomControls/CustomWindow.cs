using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace CustomControls
{
    /// <summary>
    /// Interaction logic for CustomWindow.xaml
    /// </summary>
    public partial class CustomWindow : Window {

        protected void MinimizeClick(object sender, RoutedEventArgs e) {
            WindowState = WindowState.Minimized;
        }

        protected void RestoreClick(object sender, RoutedEventArgs e) {
            Button restoreButton = GetTemplateChild("restoreButton") as Button;
            if (WindowState == WindowState.Normal) {
                WindowState = WindowState.Maximized;
                restoreButton.Content = "2";
            } else {
                WindowState = WindowState.Normal;
                restoreButton.Content = "1";
            }
        }

        protected void CloseClick(object sender, RoutedEventArgs e) {
            Close();
        }

        public override void OnApplyTemplate() {
            Button minimizeButton = GetTemplateChild("minimizeButton") as Button;
            minimizeButton.Click += MinimizeClick;

            Button restoreButton = GetTemplateChild("restoreButton") as Button;
            restoreButton.Click += RestoreClick;

            Button closeButton = GetTemplateChild("closeButton") as Button;
            closeButton.Click += CloseClick;

            Rectangle moveRect = GetTemplateChild("moveRectangle") as Rectangle;
            moveRect.PreviewMouseDown += MoveRectangle_PreviewMouseDown;

            Grid resizeGrid = GetTemplateChild("resizeGrid") as Grid;
            foreach (UIElement element in resizeGrid.Children) {
                Rectangle resizeRectangle = element as Rectangle;
                resizeRectangle.PreviewMouseDown += ResizeRectangle_PreviewMouseDown;
                resizeRectangle.MouseMove += ResizeRectangle_MouseMove;
            }

            base.OnApplyTemplate();
        }

        private void MoveRectangle_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ClickCount == 2) {
                RestoreClick(sender, e);
            }
            if (Mouse.LeftButton == MouseButtonState.Pressed) {
                DragMove();
            }
        }

        protected void ResizeRectangle_MouseMove(Object sender, MouseEventArgs e) {
            Rectangle rectangle = sender as Rectangle;
            switch (rectangle.Name) {
                case "top":
                    Cursor = Cursors.SizeNS;
                    break;
                case "bottom":
                    Cursor = Cursors.SizeNS;
                    break;
                case "left":
                    Cursor = Cursors.SizeWE;
                    break;
                case "right":
                    Cursor = Cursors.SizeWE;
                    break;
                case "topLeft":
                    Cursor = Cursors.SizeNWSE;
                    break;
                case "topRight":
                    Cursor = Cursors.SizeNESW;
                    break;
                case "bottomLeft":
                    Cursor = Cursors.SizeNESW;
                    break;
                case "bottomRight":
                    Cursor = Cursors.SizeNWSE;
                    break;
                default:
                    break;
            }
        }
        public CustomWindow() : base() {
            PreviewMouseMove += OnPreviewMouseMove;
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomWindow),
                new FrameworkPropertyMetadata(typeof(CustomWindow)));
        }

        protected void OnPreviewMouseMove(object sender, MouseEventArgs e) {
            if (Mouse.LeftButton != MouseButtonState.Pressed)
                Cursor = Cursors.Arrow;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint wMsg,
                                UIntPtr wParam, IntPtr lParam);

        protected void ResizeRectangle_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            Rectangle rectangle = sender as Rectangle;
            switch (rectangle.Name) {
                case "top":
                    Cursor = Cursors.SizeNS;
                    ResizeWindow(ResizeDirection.Top);
                    break;
                case "bottom":
                    Cursor = Cursors.SizeNS;
                    ResizeWindow(ResizeDirection.Bottom);
                    break;
                case "left":
                    Cursor = Cursors.SizeWE;
                    ResizeWindow(ResizeDirection.Left);
                    break;
                case "right":
                    Cursor = Cursors.SizeWE;
                    ResizeWindow(ResizeDirection.Right);
                    break;
                case "topLeft":
                    Cursor = Cursors.SizeNWSE;
                    ResizeWindow(ResizeDirection.TopLeft);
                    break;
                case "topRight":
                    Cursor = Cursors.SizeNESW;
                    ResizeWindow(ResizeDirection.TopRight);
                    break;
                case "bottomLeft":
                    Cursor = Cursors.SizeNESW;
                    ResizeWindow(ResizeDirection.BottomLeft);
                    break;
                case "bottomRight":
                    Cursor = Cursors.SizeNWSE;
                    ResizeWindow(ResizeDirection.BottomRight);
                    break;
                default:
                    break;
            }
        }

        private void ResizeWindow(ResizeDirection direction) {
            SendMessage(_hwndSource.Handle, 0x112, (UIntPtr)(61440 + direction), IntPtr.Zero);
        }

        private enum ResizeDirection {
            Left = 1,
            Right = 2,
            Top = 3,
            TopLeft = 4,
            TopRight = 5,
            Bottom = 6,
            BottomLeft = 7,
            BottomRight = 8,
        }

        private HwndSource _hwndSource;

        protected override void OnInitialized(EventArgs e) {
            SourceInitialized += OnSourceInitialized;
            base.OnInitialized(e);
        }

        private void OnSourceInitialized(object sender, EventArgs e) {
            _hwndSource = (HwndSource)PresentationSource.FromVisual(this);
        }
    }

}
