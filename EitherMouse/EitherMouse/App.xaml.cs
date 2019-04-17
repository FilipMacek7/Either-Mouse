using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;

namespace EitherMouse
{
    /// <summary>
    /// Interakční logika pro App.xaml
    /// </summary>
    public partial class App : Application
    {
        [DllImport("User32.dll")]
        static extern bool SystemParametersInfo(
        uint uiAction,
        uint uiParam,
        uint pvParam,
        uint fWinIni);

        uint initialSpeed = SPI_SETMOUSESPEED;
        public const uint SPI_SETMOUSESPEED = 0x0071;
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            SystemParametersInfo(SPI_SETMOUSESPEED, 0, initialSpeed, 0);
        }
    }
}
