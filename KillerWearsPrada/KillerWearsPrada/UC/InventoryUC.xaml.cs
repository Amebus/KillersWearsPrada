using KillerWearsPrada.Controller;
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
        #region da eliminare
  /*      private UserControl provenienza;
        private Canvas cprov;

        public InventoryUC(ref Canvas canvas)
        {
            cprov = canvas;
            
            
            prova.Text = cprov.Name;

        }

        public InventoryUC(ref UserControl ucProvenienza)
        {
            
            provenienza = ucProvenienza;
            InitializeComponent();
            //  txtDisplay.Text = provenienza.ToString();
            prova.Text = provenienza.GetType().ToString();

        }*/
        #endregion
        public InventoryUC()
        {
            InitializeComponent();
        }
        
        private void OnClosedStoryboardCompleted(object sender, System.EventArgs e)
        {
            

            Canvas p = (Canvas)this.Parent;

            enable_Right_Buttons(ref p);

            p.Children.Remove(this);
       
        }

        /// <summary>
        /// Enable only the buttons in the room in which inventory was opened
        /// </summary>
        /// <param name="p"></param>
        private void enable_Right_Buttons(ref Canvas p)
        {
            MainWindow i = (MainWindow)Application.Current.Windows[0];
            switch (p.Name.ToString())
            {
                case "room_Canvas":
                    i.StartRoom.change_Buttons_Status(true);
                    break;
                case "Kitchen_Image":
                    {
                        i.Room.change_KitchenButtons_Status(true);
                        i.Room.change_CommonButtons_Status(true);
                    }
                    break;
                case "Livingroom_Image":
                    {
                        i.Room.change_LivingroomButtons_Status(true);
                        i.Room.change_CommonButtons_Status(true);
                    }
                    break;
                default:
                    {
                        i.Room.change_BedroomButtons_Status(true);
                        i.Room.change_CommonButtons_Status(true);
                    }
                    break;
            }
        }
        
    }
}
