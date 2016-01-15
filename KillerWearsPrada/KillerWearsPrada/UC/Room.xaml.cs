﻿using System;
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
    /// Interaction logic for Room.xaml
    /// </summary>
    public partial class Room : UserControl
    {
        private ImageBrush ib1;
        private ImageBrush ib2;
        private ImageBrush ib3;


        public Room(String roomID)
        {
            // roomID contiene l'ID della stanza che devo caricare
            // in questa prova contiene il nome dell'immagine di sfondo della stanza che devo caricare
            // viene passata nel momento del click sulla relativa porta (e quindi creazione)
            
            InitializeComponent();

       //     setBackgroundCanvas(Application.Current.Resources[roomID].ToString());

            //    setBackgroundCanvas(roomID);


        }

        public Room()
        {
            InitializeComponent();
        }

        

        /// <summary>
        /// Set canvas background acconrdingly to the door selected
        /// </summary>
        /// <param name="roomImagePath"></param>
        public void setBackgroundCanvas(String roomImagePath)
        {
            ib1 = new ImageBrush();
            ib1.ImageSource = new BitmapImage(new Uri(@roomImagePath, UriKind.Absolute));
            room_Canvas.Background = ib1;
        }

        /*
        public void setImageBrush1(String roomImagePath)
        {
            ib1 = new ImageBrush();
            ib1.ImageSource = new BitmapImage(new Uri(@roomImagePath, UriKind.Absolute));
        }

        public void setImageBrush2(String roomImagePath)
        {
            ib1 = new ImageBrush();
            ib1.ImageSource = new BitmapImage(new Uri(@roomImagePath, UriKind.Absolute));
        }

        public void setImageBrush3(String roomImagePath)
        {
            ib1 = new ImageBrush();
            ib1.ImageSource = new BitmapImage(new Uri(@roomImagePath, UriKind.Absolute));
        }

            */

        private void exit_button(object sender, RoutedEventArgs e)
        {
          //  disable_buttons();

            MessageBoxResult result = MessageBox.Show("Do you really want to exit this game?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Windows[0].Close();
            }



        }

        private void back_button(object sender, RoutedEventArgs e)
        {
            // cambio colore ora

            /*         Color c = new Color();
                     c = Color.FromRgb(0, 255, 255);
                     Application.Current.Resources["BlinkColor"] = c; */
            /*
            StartingRoom ucstart = new StartingRoom();
            Window parentWindow = Window.GetWindow(this);
            Grid maingrid = (Grid)parentWindow.FindName("mainGrid");
            maingrid.Children.Remove(this);
            maingrid.Children.Add(ucstart); */

            disable_buttons();

            Livingroom_Image.Visibility = Visibility.Hidden;
            Kitchen_Image.Visibility = Visibility.Hidden;
            Bedroom_Image.Visibility = Visibility.Hidden;
            
            MainWindow yourParentWindow = (MainWindow)Window.GetWindow(this);
            yourParentWindow.Room.Visibility = Visibility.Hidden;

 //           yourParentWindow.StartRoom.UpdateLayout(); // <---------------------------------------------QUI

            yourParentWindow.StartRoom.Visibility = Visibility.Visible;

            // abilito i bottoni delle 3 porte e gli altri
            yourParentWindow.StartRoom.change_Buttons_Status(true);
            

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void hat_btn(object sender, RoutedEventArgs e)
        {
            disable_buttons();

            var myValue = ((Button)sender).Tag;

            SelectionDisplay selectionDisplay = new SelectionDisplay(myValue.ToString());
            Kitchen_Image.Children.Add(selectionDisplay);


            // Selection dialog covers the entire interact-able area, so the current press interaction
            // should be completed. Otherwise hover capture will allow the button to be clicked again within
            // the same interaction (even whilst no longer visible).
            selectionDisplay.Focus();

            
        }

        private void inventory_button(object sender, RoutedEventArgs e)
        {

            InventoryUC inventory;
            inventory = new InventoryUC();
            if (Livingroom_Image.Visibility == Visibility.Visible)
            
                Livingroom_Image.Children.Add(inventory);
            else if(Kitchen_Image.Visibility == Visibility.Visible)
                Kitchen_Image.Children.Add(inventory);
            else
                Bedroom_Image.Children.Add(inventory);
                
            inventory.Focus();
            disable_buttons();
        }

        private void disable_buttons()
        {
            change_BedroomButtons_Status(false);
            change_CommonButtons_Status(false);
            change_KitchenButtons_Status(false);
            change_LivingroomButtons_Status(false);
        }

        public void change_KitchenButtons_Status(Boolean b)
        {
            hat1.IsEnabled = b;
            hat3.IsEnabled = b;
        }

        public void change_LivingroomButtons_Status(Boolean b)
        {
            trousers1.IsEnabled = b;
            trousers3.IsEnabled = b;
        }

        public void change_BedroomButtons_Status(Boolean b)
        {
            shirt3.IsEnabled = b;
            shirt4.IsEnabled = b;
        }

        public void change_CommonButtons_Status(Boolean b)
        {
            inventory_btn.IsEnabled = b;
            exit.IsEnabled = b;
            back.IsEnabled = b;
        }

        private void trousers_btn(object sender, RoutedEventArgs e)
        {
            disable_buttons();
            var myValue = ((Button)sender).Tag;
            SelectionDisplay selectionDisplay = new SelectionDisplay(myValue.ToString());
            Livingroom_Image.Children.Add(selectionDisplay);
            selectionDisplay.Focus();
        }

        private void shirt_btn(object sender, RoutedEventArgs e)
        {
            disable_buttons();
            var myValue = ((Button)sender).Tag;
            SelectionDisplay selectionDisplay = new SelectionDisplay(myValue.ToString());
            Bedroom_Image.Children.Add(selectionDisplay);
            selectionDisplay.Focus();


        }
    }
}
