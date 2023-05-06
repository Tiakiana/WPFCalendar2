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

        public MajorEventBuilder()
        {
            InitializeComponent();
        }
        MainWindow _main;
        public MajorEventBuilder(MainWindow main)
        {
            _main = main;
            InitializeComponent();
        }

        //New Sign
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SignBuilder sg = new SignBuilder(tbMajorEventOverarcing.Text,int.Parse(DayOfoccurenceTxt.Text),listSigns,this);
            sg.Show();
        }

        //Cancel
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //Save
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _main.StoryController.StoryPoints.Add(new StoryPoint(tbNewMajorEventTxt.Text,tbMajorEventOverarcing.Text,int.Parse(DayOfoccurenceTxt.Text)));
            _main.StoryController.StoryPoints.AddRange(Points);
            _main.Persistence.SaveCalendar();
            this.Close();
        }
    }
}
