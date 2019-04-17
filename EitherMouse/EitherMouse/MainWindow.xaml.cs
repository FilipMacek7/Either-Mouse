using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EitherMouse
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const uint SPI_SETMOUSESPEED = 0x0071;
        public MainWindow()
        {
            InitializeComponent();
            mousespeedtext.Text = "Mouse speed: " + SPI_SETMOUSESPEED;
            speedslider.Value = SPI_SETMOUSESPEED;
        }


        [DllImport("User32.dll")]
        static extern bool SystemParametersInfo(
            uint uiAction,
            uint uiParam,
            uint pvParam,
            uint fWinIni);


        private void SpeedValue_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mousespeedtext.Text = "Mouse speed: " + speedslider.Value;
            SystemParametersInfo(SPI_SETMOUSESPEED, 0, (uint)speedslider.Value, 0);
        }
        private void SpeedValue_Completed(object sender, DragCompletedEventArgs e)
        {
            SystemParametersInfo(SPI_SETMOUSESPEED, 0, (uint)speedslider.Value, 0);
        }

    }
}
