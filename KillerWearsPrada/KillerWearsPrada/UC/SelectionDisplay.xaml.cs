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
    /// Logica di interazione per SelectionDisplay.xaml
    /// </summary>
    public partial class SelectionDisplay : UserControl
    {

        public string ImagePath
        {
            get; set;
        }

        public string ClothName
        {
            get; set;
        }

        public string ItemPrice
        {
            get; set;
        }

        public string ItemClue
        {
            get; set;
        }

        public string ItemDescription
        {
            get; set;
        }

        private string itemId { get; set; }
        
        /// <summary>
        /// Display the information and the clue related to the item chosen 
        /// </summary>
        /// <param name="itemId"></param> is the barcode of the item associated to the button pressed
        public SelectionDisplay(string itemId)
        {
          //  itemId = idItem;
            #region Load information related to the button selected, found by barcode, which is the tag button pressed
            Model.Item it = MainWindow.attGameController.Game.ActualRoom.GetItem(itemId);
            ClothName = it.ItemName;
            ItemDescription = it.Description;

            string[] temp = it.Price.ToString().Split(new char[] { ',' });
            if (temp.Length != 2)
                temp[0] += ",00";
            else
            {
                if (temp[1].Length == 1)
                    temp[1] += "0";
                temp[0] += ("," + temp[1]);
            }
            ItemPrice = temp[0] + " €";

            ItemClue = it.Clue;

            ImagePath = it.ItemsImagePath; //calcolo l'image path se non l'ho già fatto! però con la giusta directory in cui ci saranno tutti i capi

            #endregion


            InitializeComponent();

            //Importantissimo!!!
            //ma mi serve quello della mainwindow??? 
            // devo metterlo dopo InitializeComponent(), se no non si vede nulla
            this.DataContext = this;

        }

        /// <summary>
        /// Called when the OnLoaded storyboard completes.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void OnClosedStoryboardCompleted(object sender, System.EventArgs e)
        {
            Canvas parent = (Canvas)this.Parent;
            parent.Children.Remove(this);
        }

        private void enable_Right_Buttons(ref Canvas p)
        {
            MainWindow i = (MainWindow)Application.Current.Windows[0];
            switch (p.Name.ToString())
            {
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

        // per scroll textbox...
        private void EnterKeyCommand(object sender, MouseButtonEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
