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
        
        /// <summary>
        /// Display the information and the clue related to the item chosen 
        /// </summary>
        /// <param name="itemId"></param> is the barcode of the item associated to the button pressed
        public SelectionDisplay(string itemId)
        {
            
           
  /*          #region carico le cose da mostrare dell'item giusto. Mi serve sapere la stanza dove sono e il barcode che è nel tag del bottone
            Model.Item it = MainWindow.attGameController.ActualRoom.GetItemByBarCode(itemId);
            ClothName = it.ItemName;
            ItemDescription = it.Description;
            ItemPrice = it.Price.ToString();
            ItemClue = it.ClueText;

            ImagePath = Helpers.ResourcesHelper.ImagesDirectory + it.ImageFileName; //calcolo l'image path se non l'ho già fatto! però con la giusta directory in cui ci saranno tutti i capi
            
            #endregion */

            InitializeComponent();

            //Importantissimo!!!
            //ma mi serve quello della mainwindow??? 
            // devo metterlo dopo InitializeComponent(), se no non si vede nulla
            this.DataContext = this;

            // switch per capire che bottone è e quindi che item con relativa clue devo mostrare... e le cose le devo prendere dal model ovviamente!
            if (itemId == "hat1")
            {
                ImagePath = Application.Current.Resources[Helpers.ResourcesHelper.E_KitchenImages.Hat1.ToString()].ToString();
                //       DisplayedImagePath=@"C:\Users\Monica\Documents\sketchup projects\JALIS BIANCO.JPG";
                ClothName = "Cappello Fedora";
                ItemPrice = "33.50 €";
                ItemDescription = "Un fantastio cappello da indossare tutti i giorni,bianco come la neve";
                ItemClue = "Un testimone si è ricordato che il cappello era un fedora azzurro";
            }

            else {
                //         DisplayedImagePath = @"C:\Users\Monica\Documents\polimi\AUI\project\repogithub\KillerWearsPrada\KillerWearsPrada\Images\pergamena1.png";
                ImagePath = Application.Current.Resources[Helpers.ResourcesHelper.E_BedroomImages.Shirt3.ToString()].ToString();
                ClothName = "Cappello Fedora bordato";
                ItemPrice = "33.50 €";
                ItemDescription = "Un bellissimo cappello per coprire la pelata che avanza";
                //    Indizio = "Un testimone si è ricordato che il cappello era un fedora con bordo nero";
                ItemClue = "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM";
            }

            //     messageTextBlock.Text = itemId.ToString();
        }

        /// <summary>
        /// Called when the OnLoaded storyboard completes.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void OnClosedStoryboardCompleted(object sender, System.EventArgs e)
        {
            Canvas parent = (Canvas)this.Parent;
            enable_Right_Buttons(ref parent);
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

        
    }
}
