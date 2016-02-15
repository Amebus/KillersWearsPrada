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
using System.Collections.Specialized;

namespace KillerWearsPrada.UC
{
    /// <summary>
    /// Logica di interazione per InventoryUC.xaml
    /// </summary>
    public partial class InventoryUC : UserControl
    {
        
        public Trash attTrash;
        
        public ObservableCollection<Item> itemInv { get; set; }
        public ObservableCollection<Item> itemOutfit { get; set; }

        private ObservableCollection<Item> hatsDressed { get; set; }
        private ObservableCollection<Item> shirtsDressed { get; set; }
        private ObservableCollection<Item> trousersDressed { get; set; }
        
        public List<string> ListClues { get; set; }
        
        public string ImageFileNameOC { get; set; }

        public int countItems { get; set; }

        private const string endGameString = "GAME FINISHED!!!\r\nThe percentage discount you earned is @p1%\r\nMove away from the Kinect and go to the checkout to retrieve your discount!\r\nThank you to have played our game!";

        public InventoryUC()
        {
            InitializeComponent();
            
            //setting datacontext to this
            this.DataContext = this;
          
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
            p.Children.Remove(this);
        }

       
        private void trashare_Click(object sender, RoutedEventArgs e)
        {
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
            int score =  MainWindow.attGameController.ComputeScore();

            MainWindow.attGameController.Game.SetAsFinished();
            LeaveGame lg = new LeaveGame(endGameString.Replace("@p1", score.ToString()));
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

      
        #region Manage the addition and the remove of clothes of the killer and the trash
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
                                checkOrder();
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
          
            currentItemIndexLeft = LeftListBox.SelectedIndex;
            currentItemIndexRight = RightListBox.SelectedIndex;
            currentItemTextLeft = (Item)LeftListBox.SelectedValue;
            currentItemTextRight = (Item)RightListBox.SelectedValue;
            
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

                if (currentItemText != null)
                {
                    foreach (Model.Room r in MainWindow.attGameController.Game.Rooms)
                    {
                        foreach (Item ite in r.Items)
                        {
                            if (ite.BarCode == currentItemText.BarCode)
                            {
                                ite.SetAsTrashed();
                                VisualStateManager.GoToState(trash, "trashFull33", true);
                                trash.IsEnabled = true;
                                break;
                            }
                        }

                    }
                if (currentItemIndexRight < 0)
                {
                    itemInv.RemoveAt(currentItemIndex);
                }
                else
                {
                    itemOutfit.RemoveAt(currentItemIndex);
                    checkOrder();
                }
            }
        }
        #endregion

        
        private void EnterKeyCommand(object sender, MouseButtonEventArgs e)
        {

        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            itemInv = null;
            itemOutfit = null;
            itemInv = new ObservableCollection<Item>();
            itemOutfit = new ObservableCollection<Item>();

            foreach (Item it in MainWindow.attGameController.Game.ItemsInInventory)
            {
                if (it.IsDressed == false)
                    itemInv.Add(it);
            }

            foreach (Item it in MainWindow.attGameController.Game.ItemsDressed)
            {
                itemOutfit.Add(it);
            }

            checkOrder();
           

            change_Status_Inventory_Buttons(true);

            change_TrashImage();
            LeftListBox.ItemsSource = itemInv;
            RightListBox.ItemsSource = itemOutfit;

            #region Populate the list of all clues found so far
            cluesList.ItemsSource = MainWindow.attGameController.Game.DisclosedClues;
            #endregion

        }

        private void change_Order_DressedItems(object sender, NotifyCollectionChangedEventArgs e)
        {
            checkOrder();
        }
        
        private void checkOrder()
        {
            if(itemOutfit.Count == 0)
            {
                return;
            }
            if (itemOutfit.ElementAt(0).ItemKind == E_ItemKind.HAT)
                {
                    if (itemOutfit.Count > 1)
                    {
                        if (itemOutfit.ElementAt(1).ItemKind == E_ItemKind.T_SHIRT)
                        {
                            if (itemOutfit.Count > 2)
                            {
                                if (itemOutfit.ElementAt(2).ItemKind == E_ItemKind.TROUSERS)
                                {
                                    return;
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            
            Item hat1 = null;
            Item shirt1 = null;
            Item trouser1 = null;

            foreach(Item i in itemOutfit)
            {
                if(i.ItemKind == E_ItemKind.HAT)
                {
                    hat1 = i;
                }else if(i.ItemKind == E_ItemKind.T_SHIRT)
                {
                    shirt1 = i;
                }
                else
                {
                    trouser1 = i;
                }
            }

            itemOutfit.Clear();
            if(hat1 != null)
                itemOutfit.Add(hat1);
            if(shirt1 != null)
                itemOutfit.Add(shirt1);
            if(trouser1 != null)
                itemOutfit.Add(trouser1);
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
            }

            LeftListBox.ItemsSource = itemInv;
            change_TrashImage();
        }
        
    }
    
}
