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
        
        public List<string> ListClues { get; set; }
        
        public string ImageFileNameOC { get; set; }

        public int countItems { get; set; }

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

            //metto a true l'attributo has finished
            MainWindow.attGameController.Game.SetAsFinished();

            string message = "GAME FINISHED!!!\r\nThe percentage discount you earned is " + score + "%\r\nGo away from the Kinect and go to the checkout to retrieve your discount!\r\nThank you to have played our game!";

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

       
            //carico elementi da mettere nell'inventario, non indossati e non nel cestino
            // copio tutti i capi nella lista dell'inventario nella mia Observable collection, per mostrarli
            foreach (Item it in MainWindow.attGameController.Game.ItemsInInventory)
            {
                if (it.IsDressed == false)
                    itemInv.Add(it);
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
            }

            LeftListBox.ItemsSource = itemInv;
            change_TrashImage();
        }
        
        
    }
    
}
