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
        public SelectionDisplay(string itemId)
        {
            InitializeComponent();
            //Importantissimo!!!
            //ma mi serve quello della mainwindow???
            this.DataContext = this;

            // switch per capire che bottone è e quindi che item con relativa clue devo mostrare... e le cose le devo prendere dal model ovviamente!
            if (itemId == "hat1")
            {
                DisplayedImagePath = Application.Current.Resources[Helpers.ResourcesHelper.E_KitchenImages.Hat1.ToString()].ToString();
                //       DisplayedImagePath=@"C:\Users\Monica\Documents\sketchup projects\JALIS BIANCO.JPG";
                NomeCapo = "Cappello Fedora";
                Prezzo = "33.50 €";
                Descrizione = "Un fantastio cappello da indossare tutti i giorni,bianco come la neve";
                Indizio = "Un testimone si è ricordato che il cappello era un fedora azzurro";
            }

            else {
                //         DisplayedImagePath = @"C:\Users\Monica\Documents\polimi\AUI\project\repogithub\KillerWearsPrada\KillerWearsPrada\Images\pergamena1.png";
                DisplayedImagePath = Application.Current.Resources[Helpers.ResourcesHelper.E_BedroomImages.Shirt3.ToString()].ToString();
                NomeCapo = "Cappello Fedora bordato";
                Prezzo = "33.50 €";
                Descrizione = "Un bellissimo cappello per coprire la pelata che avanza";
                //    Indizio = "Un testimone si è ricordato che il cappello era un fedora con bordo nero";
                Indizio = "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM";
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

        public string DisplayedImagePath
        {
            get; set;
        }

        public string NomeCapo
        {
            get; set;
        }

        public string Prezzo
        {
            get; set;
        }

        public string Indizio
        {
            get; set;
        }

        public string Descrizione
        {
            get; set;
        }
    }
}
