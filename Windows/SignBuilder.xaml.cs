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

namespace WPFCalendar
{
    /// <summary>
    /// Interaction logic for SignBuilder.xaml
    /// </summary>
    public partial class SignBuilder : Window
    {
        // Prep stuff needed to remove close button on window
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);



        void ToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Code to remove close box from window
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }



        public SignBuilder()
        {
            Loaded += ToolWindow_Loaded;

            InitializeComponent();
        }
        MajorEventBuilder Major;
        string _overarcingReason;
        int _DayofOrigin;
        ListBox listbox;


        public SignBuilder(string overarcingReason, int dayofOriginalEvent,ListBox lb, MajorEventBuilder major)
        {
            Loaded += ToolWindow_Loaded;

            _overarcingReason = overarcingReason;
            _DayofOrigin = dayofOriginalEvent;
            listbox = lb;
            Major = major;
            InitializeComponent();
        }
        //Save
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Major.Points.Add(new StoryPoint(Signtext.Text,_overarcingReason,_DayofOrigin- int.Parse( DayOfoccurenceBeforeTxt.Text)));
            listbox.Items.Add(new StoryPoint(Signtext.Text, _overarcingReason, _DayofOrigin - int.Parse(DayOfoccurenceBeforeTxt.Text)));
            Major.Show();
            this.Close();
           
        }
        //cancel
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Major.Show();
            this.Close();
        }
    }
}
