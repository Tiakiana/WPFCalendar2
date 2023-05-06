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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPFCalendar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public MainWindow()
        //{
        //    InitializeComponent();
        //}
        int Day = 1;

        public StoryPointController StoryController;
        public Persistence Persistence;
        public MainWindow()
        {
            InitializeComponent();


            StoryController = new StoryPointController();
            Persistence = new Persistence(StoryController);
            StoryController.StoryPoints = Persistence.LoadCalendar();

            tbMainView.Text = StoryController.ConcatenateStorypoints(StoryController.FindCurrentStoryPoints(Day));
            lblpageInformation.Content = Day;
            tabCharacters.Content = new Characters();
            RefreshPage();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            //label.Content = DateTime.Now.ToLongTimeString();
            if (Day == 4)
            {
                this.Topmost = true;
                this.Topmost = false;
                MessageBox.Show("Tiden er gået");
            }
            Day++;
            ClosePopUp();
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            Day--;
            RefreshPage();
            ClosePopUp();
        }
        public void ShowMajorEventCreator()
        {

            MajorEventBuilder cto = new MajorEventBuilder(this);
            cto.Show();
        }

        private void RefreshPage()
        {
            //Til popups :D
           // System.Windows.Controls.Primitives.Popup popup = new System.Windows.Controls.Primitives.Popup();
            tbMainView.Text = StoryController.ConcatenateStorypoints(StoryController.FindCurrentStoryPoints(Day));
            List<string> origins = new List<string>();
            List<string> texts = StoryController.ConcatenateReasons(StoryController.FindCurrentStoryPoints(Day), ref origins);
            StackPanel pnl = new StackPanel();
            for (int i = 0; i < texts.Count; i++)
            {
                TextBlock tbk = new TextBlock();
                tbk.FontWeight = FontWeights.Bold;
                tbk.Text = origins[i];
                pnl.Children.Add(tbk);
                tbk = new TextBlock();
                tbk.Text = texts[i];
                pnl.Children.Add(tbk);
                //Dette skal lavestil et popup vindue i stedet, hvis jeg vil have knapper, der kan vælge hvilke info jeg gerne vil edit'e
                //Button btn = new Button();
                //btn.Height = 30;

                //btn.Width = 30;

                //btn.Content = "Click ME";
                //btn.Background = new SolidColorBrush(Colors.White);
                //btn.Foreground = new SolidColorBrush(Colors.Black);
                //btn.Click += (object sender, RoutedEventArgs e) => { MessageBox.Show("Clicked"); };
                //pnl.Children.Add(btn);  

            }

            ToolTip tooltip = new ToolTip();
            tooltip.Content = pnl;
            tbMainView.ToolTip = tooltip;

            lblpageInformation.Content = Day;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            Day++;
            RefreshPage();
            ClosePopUp();
            //tbMainView.Text = StoryController.ConcatenateStorypoints(StoryController.FindCurrentStoryPoints(Day));
            //tbMainView.ToolTip = StoryController.ConcatenateReasons(StoryController.FindCurrentStoryPoints(Day));
            //lblpageInformation.Content = Day;
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int thing = int.Parse(tbNewDay.Text);
                StoryController.StoryPoints.Add(new StoryPoint(tbNewEventTxt.Text, tbNewEventReason.Text, int.Parse(tbNewDay.Text)));
                tbNewDay.Text = "" + (Day + 1);
                tbNewEventReason.Text = "Reason";
                tbNewEventTxt.Text = "Text";
                Persistence.SaveCalendar();
                RefreshPage();
            }
            catch (Exception)
            {
                MessageBox.Show("Day must be an integer");
                throw;
            }

        }

        #region Got focus

        private void tbNewEventTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbNewEventTxt.SelectionLength == 0)
            {
                tbNewEventTxt.SelectAll();
            }
        }

        private void tbNewDay_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.SelectionLength == 0)
            {
                tb.SelectAll();
            }
        }

        private void tbNewEventReason_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (tbNewEventReason.SelectionLength == 0)
            {
                tbNewEventReason.SelectAll();
            }
        }

        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (tbNewEventTxt.SelectionLength == 0)
            {
                tbNewEventTxt.SelectAll();
            }
        }
        private void lblTime_GotFocus(object sender, RoutedEventArgs e)
        {
        }

        #endregion

            System.Windows.Controls.Primitives.Popup popup = null;
        private void tbMainView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            List<string> origins = new List<string>();
            List<string> texts = StoryController.ConcatenateReasons(StoryController.FindCurrentStoryPoints(Day), ref origins);
            if (popup!=null || texts.Count<1)
            {
                return;
            }
            popup = new System.Windows.Controls.Primitives.Popup();

        //    tbMainView.Text = StoryController.ConcatenateStorypoints(StoryController.FindCurrentStoryPoints(Day));
            StackPanel pnl = new StackPanel();
            pnl.Margin = new Thickness(5);
            for (int i = 0; i < texts.Count; i++)
            {
                TextBlock tbk = new TextBlock();
                tbk.FontWeight = FontWeights.Bold;
                tbk.Text = origins[i];
                pnl.Children.Add(tbk);
                tbk = new TextBlock();
                tbk.Text = texts[i];
                pnl.Children.Add(tbk);
                //Dette skal lavestil et popup vindue i stedet, hvis jeg vil have knapper, der kan vælge hvilke info jeg gerne vil edit'e
                Button btn = new Button();
                btn.Height = 30;
                btn.Width = 50;
                btn.Content = "Delete";
                btn.Background = new SolidColorBrush(Colors.White);
                btn.Foreground = new SolidColorBrush(Colors.Black);
                StoryPoint mystorypoint = StoryController.FindCurrentStoryPoints(Day)[i];

                btn.Click += (object senders, RoutedEventArgs ex) => { DeleteStoryPoint(mystorypoint, popup); };
                pnl.Children.Add(btn);

            }
            popup.Placement = System.Windows.Controls.Primitives.PlacementMode.Mouse;
            
            Border b = new Border();
            b.Background = new SolidColorBrush(Colors.LightYellow);
            b.BorderBrush = new SolidColorBrush(Colors.Bisque);
            b.CornerRadius = new CornerRadius(3);
            b.BorderThickness = new Thickness(1);
            popup.Child = b;
            
            b.Child = pnl;
            

            popup.IsOpen = true;
            popup.StaysOpen = false;
            popup.StaysOpen = false;
            popup.MouseLeave += (object senderen, MouseEventArgs exi) => { popup.IsOpen = false; popup = null; };


        }

        void ClosePopUp()
        {
            if (popup != null)
            {
            popup.IsOpen = false;

            popup = null;
            }
        }

        
        void DeleteStoryPoint(StoryPoint point, System.Windows.Controls.Primitives.Popup popup)
        {
            popup.IsOpen = false; 
            popup = null;
            StoryController.StoryPoints.Remove(point);
            RefreshPage();
        }

    }
}
