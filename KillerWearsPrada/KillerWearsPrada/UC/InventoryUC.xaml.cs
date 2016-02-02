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

using System.ComponentModel;

namespace KillerWearsPrada.UC
{
    /// <summary>
    /// Logica di interazione per InventoryUC.xaml
    /// </summary>
    public partial class InventoryUC : UserControl
    {
        
        public Trash attTrash;
        
        // observable collection construita con tutti gli item nella lista nell'inventario
        public ObservableCollection<Item> itemInv { get; set; }
        public ObservableCollection<Item> itemOutfit { get; set; }
        // = new ObservableCollection<Item>();

        ObservableCollection<ItemProva> p;



        // string state =  Microsoft.Kinect.HandState.Closed.ToString();

        /// <summary>
        /// INotifyPropertyChangedPropertyChanged event to allow window controls to bind to changeable data
        /// </summary>
        //  public event PropertyChangedEventHandler PropertyChanged;


        public List<string> ListClues { get; set; }
        
        public string ImageFileNameOC { get; set; }

        public int countItems { get; set; }

        public InventoryUC()
        {
          //  ciao = new EventHandler(this.RestoreFromTrash);


            
            InitializeComponent();

        //    attTrash.Unloaded += Captu

         //   attTrash.GetRestoreItem.RestoreEvent += CaptureRestoreItemEvent;//.UnloadGameEvent += CaptureUnloadGameEvent;
        //    attRestoreHandlerDelegate = new RestoreItemHandler(this.RestoreFromTrash);

          //  attRestoreHandlerDelegate += attTrash.Unloaded;

            this.DataContext = this;
          //  trash.IsEnabledChanged
            //   lbOne.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(ListBox_PreviewMouseLeftButtonDown);


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

            #region prova drag drop solo con mouse
            /*         lbOne.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(ListBox_PreviewMouseLeftButtonDown);

                     foreach (TimeZoneInfo tzi in TimeZoneInfo.GetSystemTimeZones())
                     {
                         if (zoneList.Count < 6)
                             zoneList.Add(tzi);
                     }
                     lbOne.ItemsSource = zoneList; */


            //drag drop solo mouse

            /*
                             lbOne.ItemsSource = itemInv;

                             lbTwo.ItemsSource = itemOutfit;
            */
            #endregion



           
        }

        /// <summary>
        /// On inventory closing this method enables the right buttons according to the room in which the player was before 
        /// opening the inventory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClosedStoryboardCompleted(object sender, System.EventArgs e)
        {
            Canvas p = (Canvas)this.Parent;
       //     enable_Right_Buttons(ref p);
            p.Children.Remove(this);
        }

        #region non serve più perchè lo gestisco nei usercontro room e startroom con actualroom name      
        /// <summary>
        /// Enable only the buttons in the room in which inventory was opened
        /// </summary>
        /// <param name="p"></param>
   /*     private void enable_Right_Buttons(ref Canvas p)
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
        }*/

        #endregion

        private void trashare_Click(object sender, RoutedEventArgs e)
        {
            /*   CloseInventory.IsEnabled = false;
               trash.IsEnabled = false;
               attTrash = null;
               attTrash = new Trash();*/
            change_Visibility_CloseButton(Visibility.Hidden);
            change_Status_Inventory_Buttons(false);
            attTrash = null;
            attTrash = new Trash();
            layoutRoot.Children.Add(attTrash);
            attTrash.Unloaded += UpdateContent;
        }

        private void UpdateContent(object sender, RoutedEventArgs e)
        {
            change_Visibility_CloseButton(Visibility.Visible);
            change_Status_Inventory_Buttons(true);
            BindLeftListBox();
        }

        
        private void submission_Click(object sender, RoutedEventArgs e)
        {
            change_Visibility_CloseButton(Visibility.Hidden);
            change_Status_Inventory_Buttons(false);
            //saveItemsinModel();
            int score =  MainWindow.attGameController.ComputeScore();

            //metto a true l'attributo has finished
            MainWindow.attGameController.Game.SetAsFinished();

            string message = "CONGRATULATION!!!\r\n The percentage discount you earned is " + score + "%\r\n\r\nGo to the checkout to retrieve your discount!\r\nThank you to have played our game!\r\nBye!";

            LeaveGame lg = new LeaveGame(message);
            layoutRoot.Children.Add(lg);
            lg.Focus();
        }

        /// <summary>
        /// Change the visibility of the CloseInventory button
        /// </summary>
        /// <param name="v"></param>
        private void change_Visibility_CloseButton(Visibility v)
        {
            CloseInventory.Visibility = v;
        }

        private void change_Status_Inventory_Buttons(bool status)
        {
            CloseInventory.IsEnabled = status;
            submission.IsEnabled = status;
            AddButton.IsEnabled = status;
            RemoveButton.IsEnabled = status;
            LeftListBox.IsEnabled = status;
            RightListBox.IsEnabled = status;
            trash.IsEnabled = status;
            Trash_Button.IsEnabled = status;
        }

        #region da rimuovere
    /*    /// <summary>
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

        #region GetDataFromListBox(ListBox,Point) e drag drop
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

       

        private void ListBox_Drop(object sender, DragEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            object data = e.Data.GetData(typeof(Item));
            ((IList)dragSource.ItemsSource).Remove(data);
            parent.Items.Add(data.ToString());
            itemOutfit.Add((Item)data);

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
        #endregion

            */
        #endregion

        #region Gestione aggiunta rimozione vestiti a Outfit killer e Cestino
        public Item currentItemText;
        public int currentItemIndex;
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            // Find the right item and it's value and index
            currentItemText = (Item)LeftListBox.SelectedValue;
            currentItemIndex = LeftListBox.SelectedIndex;

            if(currentItemIndex < 0)
            {
                return;
            }
            //  RightListBox.Items.Add(currentItemText);
            if (currentItemText != null)
            {

                foreach (Model.Room r in MainWindow.attGameController.Game.Rooms)
                {
                    foreach (Item ite in r.Items)
                    {
                        if (ite.BarCode == currentItemText.BarCode)
                        {
                            //   ite.Dress();
                            try
                            {
                                r.DressItem(ite.BarCode);
                                itemOutfit.Add((Item)currentItemText);
                                itemInv.RemoveAt(currentItemIndex);
                            }
                            catch (AlredyWearingAnItemException ex)
                            {
                                //Show an error message
                                Popup mex = new Popup(ex.Message);
                                layoutRoot.Children.Add(mex);
                                mex.Focus();
                            }
                            
                        }
                    }

                }
            }
        }

       

       
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            // Find the right item and it's value and index
            currentItemText = (Item)RightListBox.SelectedValue;
            currentItemIndex = RightListBox.SelectedIndex;
            // Add RightListBox item to the ArrayList
            if (currentItemText != null)
            {
                foreach (Model.Room r in MainWindow.attGameController.Game.Rooms)
                {
                    foreach (Item ite in r.Items)
                    {
                        if (ite.BarCode == currentItemText.BarCode)
                            ite.Undress();
                    }

                }
                itemInv.Add((Item)currentItemText);
                itemOutfit.RemoveAt(RightListBox.Items.IndexOf(RightListBox.SelectedItem));
            }
        }

        private int currentItemIndexLeft;
        private int currentItemIndexRight;
        private Item currentItemTextLeft;
        private Item currentItemTextRight;

        /// <summary>
        /// This method allow to trash items from both the listboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTrash_Click(object sender, RoutedEventArgs e)
        {
               ListBox l = null;
           //    l = (ListBox)sender;
            // Find the right item and it's value and index
            /*   try
               {
                   l = (ListBox)sender;

               }
               catch (System.InvalidCastException ex)
               {
                   return; //non è una delle 2 listbox, quindi non fa nulla
               }*/


            currentItemIndexLeft = LeftListBox.SelectedIndex;
            currentItemIndexRight = RightListBox.SelectedIndex;
            currentItemTextLeft = (Item)LeftListBox.SelectedValue;
            currentItemTextRight = (Item)RightListBox.SelectedValue;

            //non ho selezionato nessun item dalle 2 liste, quindi concludo
            if (currentItemIndexLeft < 0 && currentItemIndexRight < 0)
            {
                return;
            }

            if(currentItemIndexRight < 0)
            {
                l = LeftListBox;
                currentItemText = currentItemTextLeft;
                currentItemIndex = currentItemIndexLeft;
            }
            else
            {
                l = RightListBox;
                currentItemText = currentItemTextRight;
                currentItemIndex = currentItemIndexRight;
            }

      /*      currentItemText = (Item)l.SelectedValue;
            currentItemIndex = l.SelectedIndex;*/

            //     currentItemText = (Item)LeftListBox.SelectedValue;
            //     currentItemIndex = LeftListBox.SelectedIndex;
        //  RightListBox.Items.Add(currentItemText);
                if (currentItemText != null)
                {
                    foreach (Model.Room r in MainWindow.attGameController.Game.Rooms)
                    {
                        foreach (Item ite in r.Items)
                        {
                            if (ite.BarCode == currentItemText.BarCode)
                            {
                                ite.SetAsTrashed();
                            //finisce la scansione in questa stanza, ma le altre le fa cmq...
                                VisualStateManager.GoToState(trash, "trashFull33", true);
                                trash.IsEnabled = true;
                                break;
                            }
                        }

                    }
                if (currentItemIndexRight < 0)
                {
                    /*    l.Items.RemoveAt(currentItemIndex);
                            itemOutfit.RemoveAt(currentItemIndex); */
                    itemInv.RemoveAt(currentItemIndex);
                }
                else
                {
                    itemOutfit.RemoveAt(currentItemIndex);
                    
                }
            }
            
            // Refresh data binding
            // ApplyDataBinding();
        }
        #endregion



        

        private void EnterKeyCommand(object sender, MouseButtonEventArgs e)
        {

        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            itemInv = new ObservableCollection<Item>();
            itemOutfit = new ObservableCollection<Item>();

        //    zoneList = new ObservableCollection<TimeZoneInfo>();

            //carico tutti gli elementi che ho (prova)
            /*       foreach (Model.Room r in MainWindow.attGameController.Game.Rooms)
                   {
                       foreach (Item ite in r.Items)
                       {
                           ite.SetAsInInventory();
                       }

                   }*/

            //carico elementi da mettere nell'inventario, non indossati e non nel cestino
            // copio tutti i capi nella lista dell'inventario nella mia Observable collection, per mostrarli
            foreach (Item it in MainWindow.attGameController.Game.ItemsInInventory)
            {
                if (it.IsDressed == false)
                    itemInv.Add(it);
                //prova per mostrare la clue!!!
                //   ListClues.Add(it.Clue);
            }
            
            //metto tutti gli elementi dressed nella listbox dei dressed!
            foreach (Item it in MainWindow.attGameController.Game.ItemsDressed)
            {
                itemOutfit.Add(it);
            }
            
            change_Status_Inventory_Buttons(true);

            change_TrashImage();

            // Get data from somewhere and fill in my local ArrayList
            //myDataList = LoadListBoxData();
            // Bind ArrayList with the ListBox
            LeftListBox.ItemsSource = itemInv;
            RightListBox.ItemsSource = itemOutfit;

            #region popolo la lista di tutte le clues trovate fino ad allora
            cluesList.ItemsSource = MainWindow.attGameController.Game.DisclosedClues;
            #endregion

        }

        /// <summary>
        /// This method change the image of Trash according to its state (Empty or with elements)
        /// </summary>
        private void change_TrashImage()
        {
            if (MainWindow.attGameController.Game.ItemsInTrash.Count() > 0)
            {
                VisualStateManager.GoToState(trash, "trashFull33", false);
                trash.IsEnabled = true;
            }
            else
            {
                VisualStateManager.GoToState(trash, "trashempty", false);
                trash.IsEnabled = false;
            }
        }

        private void BindLeftListBox()
        {
            itemInv = null;
            itemInv = new ObservableCollection<Item>();

            foreach (Item it in MainWindow.attGameController.Game.ItemsInInventory)
            {
                if (it.IsDressed == false)
                    itemInv.Add(it);
                //prova per mostrare la clue!!!
                //   ListClues.Add(it.Clue);
            }

            LeftListBox.ItemsSource = itemInv;
            
            change_TrashImage();
        }

       
       
        
    }


    #region da eliminare
    public class GraphicInventoryItem
    {

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
    #endregion

}
