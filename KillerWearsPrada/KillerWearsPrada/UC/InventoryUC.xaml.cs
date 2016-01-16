using KillerWearsPrada.Controller;
using KillerWearsPrada.Model;
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
       
        public InventoryUC()
        {
            MainWindow i = (MainWindow)Application.Current.Windows[0];

            this.DataContext = this;
            InitializeComponent();


            //TODO sarebbe così
        /*
            lvDataBinding.ItemsSource = i.attGameController.ItemsInInventory;

            ListView1.ItemsSource = i.attGameController.ItemsInInventory;
            */

            //        List<Item> items = i.attGameController.ActualRoomItems;
            List<Item> s = new List<Item>();

           // devo anche controllare che quelli nell'inventario non siano anche nel cestino???

                s.Add(new Item (2, "bc", "name",9.04,"descr", "rep","texture", @"C:\Users\Monica\Documents\polimi\AUI\project\repogithub\KillerWearsPrada\KillerWearsPrada\Images\textures\blueTartan.jpg", "kind" ));
                s.Add(new Item(2, "bc", "name2", 9.04, "descr", "rep", "texture", @"C:\Users\Monica\Documents\polimi\AUI\project\repogithub\KillerWearsPrada\KillerWearsPrada\Images\hat_1meleOk.png", "kind"));
                s.Add(new Item(2, "bc", "name3", 9.04, "descr", "rep", "texture", @"C:\Users\Monica\Documents\polimi\AUI\project\repogithub\KillerWearsPrada\KillerWearsPrada\Images\trashEmpty.png", "kind"));
            
      /*      items.Add(new ItemProva() { code = 345, description = "eee333", itemName = "ciaone", price = 3.5 });
            items.Add(new ItemProva() { description = "Jane Doe", code = 39, imageFileName = "jane@doe-family.com", price = 0.888 });
            items.Add(new ItemProva() { maskFileName = "Sammy Doe", price = 13, reparto = "sammy.doe@gmail.com", code = 1234, description = "capo brutto brutto" });
            */
            //Bindo nome listView.ItemsSource = Lista tipizzata!!!
            lvDataBinding.ItemsSource = s;


            ListView1.ItemsSource = s;



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

        private void trashare_Click(object sender, RoutedEventArgs e)
        {

        }

        // TODO
        private void submission_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
