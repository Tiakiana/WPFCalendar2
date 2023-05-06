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
        public SignBuilder()
        {
            InitializeComponent();
        }
        MajorEventBuilder Major;
        string _overarcingReason;
        int _DayofOrigin;
        ListBox listbox;


        public SignBuilder(string overarcingReason, int dayofOriginalEvent,ListBox lb, MajorEventBuilder major)
        {
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
            this.Close();

           
        }
        //cancel
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
