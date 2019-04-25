using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
namespace EitherMouse
{
    /// <summary>
    /// Interakční logika pro App.xaml
    /// </summary>
    public partial class App
    {
        System.Windows.Forms.NotifyIcon nIcon = new System.Windows.Forms.NotifyIcon();
        public App(){
            nIcon.Icon = new Icon(@"icon.ico");
            nIcon.Visible = true;
            nIcon.Click += nIcon_Click;
            nIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            nIcon.ContextMenuStrip.Items.Add("Exit", null, exit);
        }

        private void exit(object sender, EventArgs e)
        {
            SystemParametersInfo(SPI_SETMOUSESPEED, 0, 6, 0);
            SystemParametersInfo(SPI_SETDOUBLECLICKTIME, 500, 0, 0);
            SystemParametersInfo(SPI_SETWHEELSCROLLLINES, 3, 0, 0);
            System.Windows.Application.Current.Shutdown();
        }
        void nIcon_Click(object sender, EventArgs e)
        {
            MainWindow.Visibility = Visibility.Visible;
            MainWindow.WindowState = WindowState.Normal;
        }
        [DllImport("User32.dll")]
        static extern bool SystemParametersInfo(
        uint uiAction,
        uint uiParam,
        uint pvParam,
        uint fWinIni);

        public const uint SPI_GETMOUSESPEED = 0x0070;
        public const uint SPI_GETWHEELSCROLLLINES = 0x0068;
        public const uint SPI_SETDOUBLECLICKTIME = 0x0020;
        public const uint SPI_SETMOUSESPEED = 0x0071;
        public const uint SPI_SETWHEELSCROLLLINES = 0x0069;

        [DllImport("user32.dll")]
        static extern uint GetDoubleClickTime();
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            SystemParametersInfo(SPI_SETMOUSESPEED, 0, 6, 0);
            SystemParametersInfo(SPI_SETDOUBLECLICKTIME, 500, 0, 0);
            SystemParametersInfo(SPI_SETWHEELSCROLLLINES, 3, 0, 0);
        }
    }
}
