using KillerWearsPrada.Model;
using System;
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
    /// Logica di interazione per Trash.xaml
    /// </summary>
    public partial class Trash : UserControl
    {
     //   public  EventHandler ciao;
        private ObservableCollection<Item> tr { get; set;}
        private RestoreItems attRestoreItem;

        public Trash()
        {
     //       ciao = null;
          //  attRestoreItem = new RestoreItems();
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// The instannce of Class representing the Event UnloadGame
        /// </summary>
     /*   public RestoreItems GetRestoreItem
        {
            get { return attRestoreItem; }
        }
        */

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            tr = new ObservableCollection<Item>();
            foreach(Item i in MainWindow.attGameController.Game.ItemsInTrash)
            {
                tr.Add(i);
            }

            TrashListBox.ItemsSource = tr;
        }

   /*     public event EventHandler ExecuteMethod;

        protected virtual void OnExecuteMethod()
        {
            if (ExecuteMethod != null) ExecuteMethod(this, EventArgs.Empty);
        }

        public void Restore_Click(object sender, EventArgs e)
        {
            OnExecuteMethod();
        }

       */

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            //scatena evento aggiorna tutto!!!
       //     OnRestore(null);
        }

     /*   private void Restore_Click(object sender, RoutedEventArgs e)
        {
         //   attResumeGame.RaiseEvent();
       //     attRestoreItem.RaiseEvent();
            
        }*/

        private void OnClosedStoryboardCompleted(object sender, System.EventArgs e)
        {
            // MainWindow.attGameController.ItemsInInventory = (List)zoneList;

            //     saveItemsinModel();

             // attTrash.GetRestoreItem.RestoreEvent -= CaptureRestoreItemEvent;
   //         attRestoreItem.RaiseEvent();
            Grid p = (Grid)this.Parent;
            
            p.Children.Remove(this);
            
        }

        /*     protected virtual void OnRestore(EventArgs e)
             {
                 if (ciao != null)
                 {
                     ciao(this, e);
                 }
             }*/

        private int currentItemIndex;
        private Item currentItemText;

        private void Restore_Click(object sender, RoutedEventArgs e)
        {
            currentItemText = (Item)TrashListBox.SelectedValue;
            currentItemIndex = TrashListBox.SelectedIndex;
            
            if (currentItemText != null)
            {
                foreach (Model.Room r in MainWindow.attGameController.Game.Rooms)
                {
                    foreach (Item ite in r.Items)
                    {
                        if (ite.BarCode == currentItemText.BarCode)
                            ite.SetAsInInventory();
                    }

                }
                tr.RemoveAt(TrashListBox.Items.IndexOf(TrashListBox.SelectedItem));
            }
        }

        private void EmptyTrash_Click(object sender, RoutedEventArgs e)
        {
            //Are you sure you want to delete all elements in trash?

            MainWindow.attGameController.Game.EmptyTrash();
            tr.Clear();
        }
    }

    /// <summary>
    /// Event that occur when the trash usercontrol is unload
    /// </summary>
    public class RestoreItems
    {
        public event EventHandler<Arguments> RestoreEvent;

        protected virtual void OnRestore(Arguments e)
        {
            if (RestoreEvent != null)
            {
                RestoreEvent(this, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void RaiseEvent()
        {
            //TODO mettere l'ID del giocatore entrato
            Arguments wvParameters = new Arguments();


            OnRestore(wvParameters);
        }

        public class Arguments : EventArgs
        {

        }
    }
}
