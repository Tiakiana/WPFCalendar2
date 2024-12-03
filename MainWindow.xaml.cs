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
        int Day = 8;
        int Month = 0; 
        public StoryPointController StoryController;
        public Persistence Persistence;

        public List<string> Months = new List<string>() {"Resplendant Water", "Descending Water",
            "Ascending Wood", "Resplendant Wood", "Descending Wood",
            "Ascending Fire", "Resplendant Fire", "Descending Fire",
        "Ascending Earth", "Resplendant Earth", "Descending Earth",
        "Calibration",
          "Ascending Air", "Resplendant Air", "Descending Air",
        "Ascending Water"  
        };

        public string GetDate(int day)
        {
            Month = 0;
            for (int i = 1; i < day; i++)
            {
                if (i%28 == 0)
                {
                    Month++;
                }
            }
                
            return (Day - 28 * Month).ToString() +". of " + Months[Month];
        }

        MainWindow me;
        public MainWindow()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            me = this;

            StoryController = new StoryPointController();
            Persistence = new Persistence(StoryController);
            Day = Persistence.LoadDate();
            StoryController.StoryPoints = Persistence.LoadCalendar();
            StoryController.PlayerLocations = Persistence.LoadLocationsOfPlayers();
            tbMainView.Text = StoryController.ConcatenateStorypoints(StoryController.FindCurrentStoryPoints(Day));
            lblpageInformation.Content = Day;
           // tabCharacters.Content = new Characters();

            RefreshPage();
        }
        


        //void timer_Tick(object sender, EventArgs e)
        //{
        //    //label.Content = DateTime.Now.ToLongTimeString();
        //    if (Day == 4)
        //    {
        //        this.Topmost = true;
        //        this.Topmost = false;
        //        MessageBox.Show("Tiden er gået");
        //    }
        //    Day++;
        //    ClosePopUp();
        //}

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            Day--;
            RefreshPage();
            ClosePopUp();
        }
        public void ShowMajorEventCreator()
        {

            MajorEventBuilder cto = new MajorEventBuilder(this);
            cto.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            cto.Show();
            this.Hide();
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

            IsaLocation.Document.Blocks.Clear();
            IsaLocation.Document.Blocks.Add(new Paragraph(new Run(StoryController.PlayerLocations[0])));
            ThomasLocation.Document.Blocks.Clear();
            ThomasLocation.Document.Blocks.Add(new Paragraph(new Run(StoryController.PlayerLocations[1])));
            QuangLocation.Document.Blocks.Clear();
            QuangLocation.Document.Blocks.Add(new Paragraph(new Run(StoryController.PlayerLocations[2])));
            JimmyLocation.Document.Blocks.Clear();
            JimmyLocation.Document.Blocks.Add(new Paragraph(new Run(StoryController.PlayerLocations[3])));
            ChopartLocation.Document.Blocks.Clear();
            ChopartLocation.Document.Blocks.Add(new Paragraph(new Run(StoryController.PlayerLocations[4])));



            lblpageInformation.Content =  Day;
            me.Title = "RPG Calendar - " + GetDate(Day);
           
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
        private void btnCommitLocations(object sender, RoutedEventArgs e)
        {
            string isatext = new TextRange(IsaLocation.Document.ContentStart, IsaLocation.Document.ContentEnd).Text;
            string thomastext = new TextRange(ThomasLocation.Document.ContentStart, ThomasLocation.Document.ContentEnd).Text;
            string quangtext = new TextRange(QuangLocation.Document.ContentStart, QuangLocation.Document.ContentEnd).Text;
            string jimmytext = new TextRange(JimmyLocation.Document.ContentStart, JimmyLocation.Document.ContentEnd).Text;
            string choparttext = new TextRange(ChopartLocation.Document.ContentStart, ChopartLocation.Document.ContentEnd).Text;

            Persistence.SaveLocationsOfPlayers(isatext,thomastext,quangtext,jimmytext,choparttext);
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
        private void gotsMeSomeFocus(object sender, RoutedEventArgs e)
        {
            var textbox = e.OriginalSource as RichTextBox;
            textbox.SelectAll();
        }
        private void gotsMeSomeFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            RichTextBox tb = e.OriginalSource as RichTextBox;

            tb.SelectAll();

            //var textbox = e.OriginalSource as TextBox;
            //textbox.SelectAll();
        }

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

        private void tabTimeLine_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ShowMajorEventCreator();
        }

        private void tabTimeLine_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Persistence.SaveDate(Day);
            btnCommitLocations(null,null);
        }

        private void btnPriviousFive_Click(object sender, RoutedEventArgs e)
        {
            Day--;
            Day--;
            Day--;
            Day--;
            Day--;
            RefreshPage();
            ClosePopUp();
        }

        private void btnNextFive_Click(object sender, RoutedEventArgs e)
        {
            Day++;
            Day++;
            Day++;
            Day++;
            Day++;
            RefreshPage();
            ClosePopUp();
        }
    }
}
