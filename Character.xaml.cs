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

namespace WPFCalendar
{
    /// <summary>
    /// Interaction logic for Character.xaml
    /// </summary>
    public partial class Character : UserControl
    {
        public Character()
        {
            InitializeComponent();
        }
        public Character(string toplefte, string toprighte, string downlefte, string downrighte)
        {
           
            InitializeComponent(); 
            topleft.Text = toplefte;
            downleft.Text = downlefte;
            topright.Text = toprighte;
        }

    }
}
