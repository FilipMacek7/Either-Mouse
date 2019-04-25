using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
namespace EitherMouse
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
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
        unsafe public MainWindow()
        {
            InitializeComponent();
            int speed;
            SystemParametersInfo(
                SPI_GETMOUSESPEED,
                0,
                (uint)new IntPtr(&speed),
                0);
            speedslider.Value = speed;
            speedtext.Text = "Mouse speed: " + speedslider.Value.ToString();
            doubleclickslider.Value = GetDoubleClickTime();
            doubleclicktext.Text = "Double click speed: " + doubleclickslider.Value.ToString();
            int scroll;
            SystemParametersInfo(
                SPI_GETWHEELSCROLLLINES,
                0,
                (uint)new IntPtr(&scroll),
                0);
            scrolllinesslider.Value = scroll;
            scrolllinestext.Text = "Scroll lines: " + scrolllinesslider.Value.ToString();

        }




        private void SpeedValue_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            speedtext.Text = "Mouse speed: " + speedslider.Value;
            SystemParametersInfo(SPI_SETMOUSESPEED, 0, (uint)speedslider.Value, 0);
        }
        private void SpeedValue_Completed(object sender, DragCompletedEventArgs e)
        {
            SystemParametersInfo(SPI_SETMOUSESPEED, 0, (uint)speedslider.Value, 0);
        }

        private void DoubleClickValue_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            doubleclicktext.Text = "Double click speed: " + doubleclickslider.Value;
            SystemParametersInfo(SPI_SETDOUBLECLICKTIME, (uint)doubleclickslider.Value, 0, 0);
        }
        private void DoubleClickValue_Completed(object sender, DragCompletedEventArgs e)
        {
            SystemParametersInfo(SPI_SETDOUBLECLICKTIME, (uint)doubleclickslider.Value, 0, 0);
        }
        private void ScrollLinesValue_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            scrolllinestext.Text = "Scroll lines: " + scrolllinesslider.Value;
            SystemParametersInfo(SPI_SETWHEELSCROLLLINES, (uint)scrolllinesslider.Value, 0, 0);
        }
        private void ScrollLinesValue_Completed(object sender, DragCompletedEventArgs e)
        {
            SystemParametersInfo(SPI_SETWHEELSCROLLLINES, (uint)scrolllinesslider.Value, 0, 0);
        }

        private void Save_Profile(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile(profilename.Text,(uint)speedslider.Value, (uint)doubleclickslider.Value, (uint)scrolllinesslider.Value);
            File.WriteAllText(@""+profilename.Text+".json", JsonConvert.SerializeObject(profile));
            info.Text = "New profile " + profilename.Text + " has been succefuly saved.";
        }

        private void Load_Profile(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Profile loadedprofile = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(openFileDialog.FileName));
                loadedprofiletext.Text = "Loaded profile:" + loadedprofile.Name;
                speedslider.Value = loadedprofile.Speed;
                speedtext.Text = "Mouse speed: " + speedslider.Value;
                doubleclickslider.Value = loadedprofile.DoubleClick;
                doubleclicktext.Text = "Double click speed: " + doubleclickslider.Value;
                scrolllinesslider.Value = loadedprofile.Scroll;
                scrolllinestext.Text = "Scroll lines: " + scrolllinesslider.Value;
            }
        }
    }
}
