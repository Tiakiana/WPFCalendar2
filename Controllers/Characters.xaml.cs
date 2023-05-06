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
    /// Interaction logic for Characters.xaml
    /// </summary>
    public partial class Characters : UserControl
    {
        public Characters()
        {
            InitializeComponent();
            spCharacters.Children.Add(new Character());
            //spCharacters.Children.Add(new Character("Jakob","Jakob","Jakob","Jakob"));
            //spCharacters.Children.Add(new Character("Jakob1","Jakob1","Jakob1","Jakob1"));
            //spCharacters.Children.Add(new Character("Jakob2","Jakob2","Jakob2","Jakob2"));
            //spCharacters.Children.Add(new Character("Jakob3","Jakob3","Jakob3","Jakob3"));
        }


    }
}
