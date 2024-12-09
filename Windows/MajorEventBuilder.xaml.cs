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
    /// Interaction logic for MajorEventBuilder.xaml
    /// </summary>
    public partial class MajorEventBuilder : Window
    {


        public List<StoryPoint> Points = new List<StoryPoint>();

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


        public MajorEventBuilder()
        {
            Loaded += ToolWindow_Loaded;
            InitializeComponent();
        }
        MainWindow _main;
        public MajorEventBuilder(MainWindow main)
        {
            Loaded += ToolWindow_Loaded;
            _main = main;
            InitializeComponent();
        }

        //New Sign
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SignBuilder sg = new SignBuilder(tbMajorEventOverarcing.Text,int.Parse(DayOfoccurenceTxt.Text),listSigns,this);
            sg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            sg.Show();
            this.Hide();
        }

        //Cancel
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _main.Show();
            this.Close();
        }
        //Save
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _main.StoryController.StoryPoints.Add(new StoryPoint(tbNewMajorEventTxt.Text,tbMajorEventOverarcing.Text,int.Parse(DayOfoccurenceTxt.Text)));
            _main.StoryController.StoryPoints.AddRange(Points);
            
            _main.Persistence.SaveCalendar();
            _main.Show();
            this.Close();
        }
    }
}
