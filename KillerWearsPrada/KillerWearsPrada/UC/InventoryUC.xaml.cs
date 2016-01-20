using KillerWearsPrada.Controller;
using KillerWearsPrada.Model;
using Microsoft.Kinect.Toolkit.Input;
using Microsoft.Kinect.Wpf.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        // observable collection construita con tutti gli item nella lista nell'inventario
        public ObservableCollection<Item> itemInv { get; set; }
            // = new ObservableCollection<Item>();

        ObservableCollection<ItemProva> p;

        ObservableCollection<ItemProva> SelectedItems = new ObservableCollection<ItemProva>();
        ObservableCollection<ItemProva> SelectedItems2 = new ObservableCollection<ItemProva>();

        public string ImageFileNameOC { get; set; }

        public int countItems { get; set; }

        public InventoryUC()
        {
            itemInv = new ObservableCollection<Item>();
            zoneList = new ObservableCollection<TimeZoneInfo>();

            // copio tutti i capi nella lista dell'inventario nella mia Observable collection, per mostrarli
          /*     foreach (Item it in MainWindow.attGameController.ItemsInInventory)
               {
                
                    ImageFileNameOC = ImagePath = Helpers.ResourcesHelper.ItemsImagesPath + it.ImageFileName;
                itemInv.Add(it);
               }
              */
            //conto e mostro gli item che ha nell'inventariooo
            // ma devo farlo cambiare runtime!!!
            //countItems = "You have " + zoneList.Count.ToString() + " items in your inventory";

           

            this.DataContext = this;
          //  countItems = zoneList.Count;
            InitializeComponent();

            

            // se non ci sono item nell'inventario, mostrare label 

            #region togliere

            //TODO sarebbe così
            /*
                lvDataBinding.ItemsSource = i.attGameController.ItemsInInventory;

                ListView1.ItemsSource = i.attGameController.ItemsInInventory;
                */

            //        List<Item> items = i.attGameController.ActualRoomItems;
            //       List<Item> s = new List<Item>();

            // devo anche controllare che quelli nell'inventario non siano anche nel cestino???
            /*
                            s.Add(new Item (2, "bc", "name",9.04,"descr", "rep","texture", @"C:\Users\Monica\Documents\polimi\AUI\project\repogithub\KillerWearsPrada\KillerWearsPrada\Images\textures\blueTartan.jpg", "kind" ));
                            s.Add(new Item(2, "bc", "name2", 9.04, "descr", "rep", "texture", @"C:\Users\Monica\Documents\polimi\AUI\project\repogithub\KillerWearsPrada\KillerWearsPrada\Images\hat_1meleOk.png", "kind"));
                            s.Add(new Item(2, "bc", "name3", 9.04, "descr", "rep", "texture", @"C:\Users\Monica\Documents\polimi\AUI\project\repogithub\KillerWearsPrada\KillerWearsPrada\Images\trashEmpty.png", "kind"));
                        */
            /*      items.Add(new ItemProva() { code = 345, description = "eee333", itemName = "ciaone", price = 3.5 });
                  items.Add(new ItemProva() { description = "Jane Doe", code = 39, imageFileName = "jane@doe-family.com", price = 0.888 });
                  items.Add(new ItemProva() { maskFileName = "Sammy Doe", price = 13, reparto = "sammy.doe@gmail.com", code = 1234, description = "capo brutto brutto" });
                  */
            //Bindo nome listView.ItemsSource = Lista tipizzata!!!
            //    lvDataBinding.ItemsSource = s;

            #endregion


            // Come dovrei  per prendere gli item nell'inventario
            //      ListView1.ItemsSource = itemInv;

            #region prova drag drop solo con mouse
            lbOne.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(ListBox_PreviewMouseLeftButtonDown);

            foreach (TimeZoneInfo tzi in TimeZoneInfo.GetSystemTimeZones())
            {
                if (zoneList.Count < 6)
                    zoneList.Add(tzi);
            }
            lbOne.ItemsSource = zoneList;
            #endregion
            
        }

        

        public Int32 ArticleCount
        {
            get
            {
                if (zoneList == null)
                {
                    return 0;
                }
                else
                {
                    return this.zoneList.Count;
                }
            }
        }

        private void OnClosedStoryboardCompleted(object sender, System.EventArgs e)
        {
            // MainWindow.attGameController.ItemsInInventory = (List)zoneList;

            saveItemsinModel();

            Canvas p = (Canvas)this.Parent;

            enable_Right_Buttons(ref p);

            p.Children.Remove(this);

        }

        /// <summary>
        /// salvare le proprietà dressed ininventory e trashed dei vari items nel model alla chiusura dell'inventario, o alla submission
        /// </summary>
        private void saveItemsinModel()
        {
            /*      foreach (Item r in MainWindow.attGameController.ItemsInInventory)
                  {
                      foreach (Item i in itemInv)
                      {
                          if (i.BarCode == r.BarCode)
                          {
                              r.IsInInventory = i.IsInInventory;
                              r.IsDressed = i.IsDressed;
                              r.IsTrashed = i.IsTrashed;
                          }
                      }
                  }*/
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
            saveItemsinModel();

            //controllo se sono giudti gli item in dressed con quelli in solution?
            //prendere da game controller la attGameSolution?
        }


        /// <summary>
        /// quindi lo fa?
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void check_Checked(object sender, RoutedEventArgs e)
        {
            p.RemoveAt(0);
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            p.Remove((ItemProva)listMyItems.SelectedItem);
        }

        private void listMyItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItems.Add((ItemProva)listMyItems.SelectedItem);
        }

        //TODO
        //cose per implementare drag drop listboxes
        public ObservableCollection<TimeZoneInfo> zoneList { get; set; }
            
            //= new ObservableCollection<TimeZoneInfo>();
        ObservableCollection<string> zoneListDest = new ObservableCollection<string>();

        ListBox dragSource = null;
        ListBox dragDestination = null;

        public bool IsPressable
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsManipulatable
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        
        private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            dragSource = parent;
            object data = GetDataFromListBox(dragSource, e.GetPosition(parent));

            if (data != null)
            {
                DragDrop.DoDragDrop(parent, data, DragDropEffects.Move);
            }
        }

        #region GetDataFromListBox(ListBox,Point)
        private static object GetDataFromListBox(ListBox source, Point point)
        {
            UIElement element = source.InputHitTest(point) as UIElement;
            if (element != null)
            {
                object data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    data = source.ItemContainerGenerator.ItemFromContainer(element);
                    if (data == DependencyProperty.UnsetValue)
                    {
                        element = VisualTreeHelper.GetParent(element) as UIElement;
                    }
                    if (element == source)
                    {
                        return null;
                    }
                }
                if (data != DependencyProperty.UnsetValue)
                {
                    return data;
                }
            }
            return null;
        }

        #endregion

        private void ListBox_Drop(object sender, DragEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            object data = e.Data.GetData(typeof(TimeZoneInfo));
            ((IList)dragSource.ItemsSource).Remove(data);
            parent.Items.Add(data.ToString());
        }

        private void Trash_Drop(object sender, DragEventArgs e)
        {
            //   ListBox parent = (ListBox)sender;
            object data = e.Data.GetData(typeof(TimeZoneInfo));
            ((IList)dragSource.ItemsSource).Remove(data);
            //    parent.Items.Add(data.ToString());
        }

        public IKinectController CreateController(IInputModel inputModel, KinectRegion kinectRegion)
        {
            throw new NotImplementedException();
        }
    }



    public class GraphicInventoryItem { 

        public GraphicInventoryItem()
        {
            
        }

        // l'id è il barcode dell'item
        public string id { get; set; }
        public string ImageItem { get; set; }

        public bool isDressed
        {
            get; set;
        }

        public bool isInInventory
        {
            get; set;
        }

        public bool isInTrash
        {
            get; set;
        }

    }

    public class ItemProva
    {
        public ItemProva()
        {

        }

        public string NomeItem { get; set; }
        public bool isDressed
        {
            get; set;
        }


    }


}
