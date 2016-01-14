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

namespace KillerWearsPrada.UC
{
    /// <summary>
    /// Logica di interazione per InventoryUC.xaml
    /// </summary>
    public partial class InventoryUC : UserControl
    {
        private UserControl provenienza;
        public InventoryUC(UserControl ucProvenienza)
        {
            provenienza = ucProvenienza;
            InitializeComponent();
          //  txtDisplay.Text = provenienza.ToString();

        }

        public InventoryUC()
        {
            InitializeComponent();
        }

        

   

        private void OnClosedStoryboardCompleted(object sender, System.EventArgs e)
        {


            var parentGrid = (Panel)this.Parent;
            Canvas parent = (Canvas)parentGrid.FindName("room_Canvas");


      //      Button bnt1 = (Button)parent.FindName("hat1");
     //       bnt1.IsEnabled = true;
            /*       Button bnt2 = (Button)parent.FindName("hat2");
                   bnt2.IsEnabled = true;
                   Button bnt3 = (Button)parent.FindName("hat3");
                   bnt3.IsEnabled = true;
                   Button bnt4 = (Button)parent.FindName("hat4");
                   bnt4.IsEnabled = true;
                   Button bnt5 = (Button)parent.FindName("hat5");
                   bnt5.IsEnabled = true;
                   Button bnt6 = (Button)parent.FindName("hat6");
                   bnt6.IsEnabled = true;
                   */

            parentGrid.Children.Remove(this);





        }
    }
}
