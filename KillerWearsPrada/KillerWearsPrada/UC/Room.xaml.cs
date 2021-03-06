﻿using KillerWearsPrada.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static KillerWearsPrada.Helpers.ResourcesHelper;

namespace KillerWearsPrada.UC
{
    /// <summary>
    /// Interaction logic for Room.xaml
    /// </summary>
    public partial class Room : UserControl
    {
         #region da eliminare
        private ImageBrush imageBackground;
    /*    private ImageBrush ib2;
        private ImageBrush ib3;

        public string tr1 { get; set; } */
      #endregion 

        #region indirizzi per binding immagini bottoni
        public string trousers1Image { get; set; }
        public string trousers2Image { get; set; }
        public string trousers3Image { get; set; }
        public string trousers4Image { get; set; }
        public string trousers5Image { get; set; }
        public string trousers6Image { get; set; }
        public string hat1Image { get; set; }
        public string hat2Image { get; set; }
        public string hat3Image { get; set; }
        public string hat4Image { get; set; }
        public string hat5Image { get; set; }
        public string hat6Image { get; set; }
        public string shirt1Image { get; set; }
        public string shirt2Image { get; set; }
        public string shirt3Image { get; set; }
        public string shirt4Image { get; set; }
        public string shirt5Image { get; set; }
        public string shirt6Image { get; set; }
        #endregion

        public List<Button> listOfButtons { get; set; }
        public SelectionDisplay selectionDisplay;

        public Room(string roomID)
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
            // ItemKind ti dà il tipo di capo! hat,  t_shirt,  trousers


            //   tr1 = Application.Current.Resources[Helpers.ResourcesHelper.E_LivingroomImages.Trousers1.ToString()].ToString();

            //svuoto la directory con gli sketches!!!

           
            listOfButtons = null;
            listOfButtons = new List<Button>();

           

            InitializeComponent();

            this.DataContext = this;

            listOfButtons.Add(trousers1);
            listOfButtons.Add(trousers2);
            listOfButtons.Add(trousers3);
            listOfButtons.Add(trousers4);
            listOfButtons.Add(trousers5);
            listOfButtons.Add(trousers6);
            listOfButtons.Add(hat1);
            listOfButtons.Add(hat2);
            listOfButtons.Add(hat3);
            listOfButtons.Add(hat4);
            listOfButtons.Add(hat5);
            listOfButtons.Add(hat6);
            listOfButtons.Add(shirt1);
            listOfButtons.Add(shirt2);
            listOfButtons.Add(shirt3);
            listOfButtons.Add(shirt4);
            listOfButtons.Add(shirt5);
            listOfButtons.Add(shirt6);

            setClothButtons();

        }

        /// <summary>
        /// Get all the items in each room and set button.Tag to item.Barcode and set up the button background to the item sketch created
        /// </summary>
        private void setClothButtons()
        {
            foreach (Model.Room r in MainWindow.attGameController.Game.Rooms)
            {

                switch (r.Name)
                {
                    case Model.E_RoomsName.START_ROOM:
                        break;
                    case Model.E_RoomsName.LIVINGROOM:
                        {
                            trousers1.Tag = r.Items[0].BarCode;
                            trousers1Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_LivingroomImages.Trousers1)), ResourcesHelper.TexturesPath(r.Items[0].TextureFilename), "panta1image.png");
                            trousers2.Tag = r.Items[1].BarCode;
                            trousers2Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_LivingroomImages.Trousers2)), ResourcesHelper.TexturesPath(r.Items[1].TextureFilename), "panta2image.png");
                            trousers3.Tag = r.Items[2].BarCode;
                            trousers3Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_LivingroomImages.Trousers3)), ResourcesHelper.TexturesPath(r.Items[2].TextureFilename), "panta3image.png");
                            trousers4.Tag = r.Items[3].BarCode;
                            trousers4Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_LivingroomImages.Trousers4)), ResourcesHelper.TexturesPath(r.Items[3].TextureFilename), "panta4image.png");
                            trousers5.Tag = r.Items[4].BarCode;
                            trousers5Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_LivingroomImages.Trousers5)), ResourcesHelper.TexturesPath(r.Items[4].TextureFilename), "panta5image.png");
                            trousers6.Tag = r.Items[5].BarCode;
                            trousers6Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_LivingroomImages.Trousers6)), ResourcesHelper.TexturesPath(r.Items[5].TextureFilename), "panta6image.png");
                        }
                        break;
                    case Model.E_RoomsName.KITCHEN:
                        {

                            hat1.Tag = r.Items[0].BarCode;
                            if (r.Items[0].Shape == Model.E_Shape.LONG)
                                hat1Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_KitchenImages.Hat1hat)), ResourcesHelper.TexturesPath(r.Items[0].TextureFilename), "hat1image.png");
                            else
                                hat1Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_KitchenImages.Hat1cap)), ResourcesHelper.TexturesPath(r.Items[0].TextureFilename), "hat1image.png");

                            hat2.Tag = r.Items[1].BarCode;
                            if (r.Items[1].Shape == Model.E_Shape.LONG)
                                hat2Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_KitchenImages.Hat2hat)), ResourcesHelper.TexturesPath(r.Items[1].TextureFilename), "hat2image.png");
                            else
                                hat2Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_KitchenImages.Hat2cap)), ResourcesHelper.TexturesPath(r.Items[1].TextureFilename), "hat2image.png");

                            hat3.Tag = r.Items[2].BarCode;
                            if (r.Items[2].Shape == Model.E_Shape.LONG)
                                hat3Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_KitchenImages.Hat3hat)), ResourcesHelper.TexturesPath(r.Items[2].TextureFilename), "hat3image.png");
                            else
                                hat3Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_KitchenImages.Hat3cap)), ResourcesHelper.TexturesPath(r.Items[2].TextureFilename), "hat3image.png");

                            hat4.Tag = r.Items[3].BarCode;
                            if (r.Items[3].Shape == Model.E_Shape.LONG)
                                hat4Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_KitchenImages.Hat4hat)), ResourcesHelper.TexturesPath(r.Items[3].TextureFilename), "hat4image.png");
                            else
                                hat4Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_KitchenImages.Hat4cap)), ResourcesHelper.TexturesPath(r.Items[3].TextureFilename), "hat4image.png");

                            hat5.Tag = r.Items[4].BarCode;
                            if (r.Items[4].Shape == Model.E_Shape.LONG)
                                hat5Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_KitchenImages.Hat5hat)), ResourcesHelper.TexturesPath(r.Items[4].TextureFilename), "hat5image.png");
                            else
                                hat5Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_KitchenImages.Hat5cap)), ResourcesHelper.TexturesPath(r.Items[4].TextureFilename), "hat5image.png");

                            hat6.Tag = r.Items[5].BarCode;
                            if (r.Items[5].Shape == Model.E_Shape.LONG)
                                hat6Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_KitchenImages.Hat6hat)), ResourcesHelper.TexturesPath(r.Items[5].TextureFilename), "hat6image.png");
                            else
                                hat6Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_KitchenImages.Hat6cap)), ResourcesHelper.TexturesPath(r.Items[5].TextureFilename), "hat6image.png");

                        }
                        break;
                    default:
                        {
                            shirt1.Tag = r.Items[0].BarCode;
                            shirt1Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_BedroomImages.Shirt1)), ResourcesHelper.TexturesPath(r.Items[0].TextureFilename), "shirt1image.png");
                            shirt2.Tag = r.Items[1].BarCode;
                            shirt2Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_BedroomImages.Shirt2)), ResourcesHelper.TexturesPath(r.Items[1].TextureFilename), "shirt2image.png");
                            shirt3.Tag = r.Items[2].BarCode;
                            shirt3Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_BedroomImages.Shirt3)), ResourcesHelper.TexturesPath(r.Items[2].TextureFilename), "shirt3image.png");
                            shirt4.Tag = r.Items[3].BarCode;
                            shirt4Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_BedroomImages.Shirt4)), ResourcesHelper.TexturesPath(r.Items[3].TextureFilename), "shirt4image.png");
                            shirt5.Tag = r.Items[4].BarCode;
                            shirt5Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_BedroomImages.Shirt5)), ResourcesHelper.TexturesPath(r.Items[4].TextureFilename), "shirt5image.png");
                            shirt6.Tag = r.Items[5].BarCode;
                            shirt6Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_BedroomImages.Shirt6)), ResourcesHelper.TexturesPath(r.Items[5].TextureFilename), "shirt6image.png");
                        }
                        break;
                }
            }
        }


        /// <summary>
        /// Set canvas background acconrdingly to the door selected
        /// </summary>
        /// <param name="roomImagePath">Path of the </param>
        public void setBackgroundCanvas(string roomImagePath)
        {
            imageBackground = new ImageBrush();
            imageBackground.ImageSource = new BitmapImage(new Uri(@roomImagePath, UriKind.Absolute));
            room_Canvas.Background = imageBackground;
        }



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
            MainWindow.attGameController.Game.ActualRoomIndex = 0;
            disable_buttons();
            
            Livingroom_Image.Visibility = Visibility.Hidden;
            Kitchen_Image.Visibility = Visibility.Hidden;
            Bedroom_Image.Visibility = Visibility.Hidden;
            
            MainWindow yourParentWindow = (MainWindow)Window.GetWindow(this);
            yourParentWindow.Room.Visibility = Visibility.Hidden;
            yourParentWindow.StartRoom.Visibility = Visibility.Visible;

            yourParentWindow.StartRoom.change_Buttons_Status(true);
            yourParentWindow.changeDoorColor();
        }

        private void hat_btn(object sender, RoutedEventArgs e)
        {
            disable_buttons();
            var myValue = ((Button)sender).Tag;
            selectionDisplay = null;
            selectionDisplay = new SelectionDisplay(myValue.ToString());
            Kitchen_Image.Children.Add(selectionDisplay);
            selectionDisplay.Focus();
            selectionDisplay.Unloaded += UpdateButtons;
        }

        public InventoryUC inventory;

        private void inventory_button(object sender, RoutedEventArgs e)
        {
            
            inventory = null;
            inventory = new InventoryUC();
            if (Livingroom_Image.Visibility == Visibility.Visible)
            
                Livingroom_Image.Children.Add(inventory);
            else if(Kitchen_Image.Visibility == Visibility.Visible)
                Kitchen_Image.Children.Add(inventory);
            else
                Bedroom_Image.Children.Add(inventory);
                
            inventory.Focus();
            disable_buttons();
            inventory.Unloaded += UpdateButtons;
        }

        

        private void disable_buttons()
        {
            change_BedroomButtons_Status(false);
            change_CommonButtons_Status(false);
            change_KitchenButtons_Status(false);
            change_LivingroomButtons_Status(false);
        }

        public void change_KitchenButtons_Status(bool b)
        {
            hat1.IsEnabled = b;
            hat2.IsEnabled = b;
            hat3.IsEnabled = b;
            hat4.IsEnabled = b;
            hat5.IsEnabled = b;
            hat6.IsEnabled = b;
        }

        public void change_LivingroomButtons_Status(bool b)
        {
            trousers1.IsEnabled = b;
            trousers2.IsEnabled = b;
            trousers3.IsEnabled = b;
            trousers4.IsEnabled = b;
            trousers5.IsEnabled = b;
            trousers6.IsEnabled = b;
        }

        public void change_BedroomButtons_Status(bool b)
        {
            shirt1.IsEnabled = b;
            shirt2.IsEnabled = b;
            shirt3.IsEnabled = b;
            shirt4.IsEnabled = b;
            shirt5.IsEnabled = b;
            shirt6.IsEnabled = b;
        }

        public void change_CommonButtons_Status(bool b)
        {
            inventory_btn.IsEnabled = b;
            exit.IsEnabled = b;
            back.IsEnabled = b;
        }

        private void trousers_btn(object sender, RoutedEventArgs e)
        {
            disable_buttons();
            var myValue = ((Button)sender).Tag;
            selectionDisplay = null;
            selectionDisplay = new SelectionDisplay(myValue.ToString());
            Livingroom_Image.Children.Add(selectionDisplay);
            selectionDisplay.Focus();
            selectionDisplay.Unloaded += UpdateButtons;
        }

        private void enable_Right_Buttons()
        {
            switch (MainWindow.attGameController.Game.ActualRoom.Name)
            {
                case Model.E_RoomsName.KITCHEN:
                    {
                        change_KitchenButtons_Status(true);
                        change_CommonButtons_Status(true);
                    }
                    break;
                case Model.E_RoomsName.LIVINGROOM:
                    {
                        change_LivingroomButtons_Status(true);
                        change_CommonButtons_Status(true);
                    }
                    break;
                default:
                    {
                        change_BedroomButtons_Status(true);
                        change_CommonButtons_Status(true);
                    }
                    break;
            }
        }

        private void shirt_btn(object sender, RoutedEventArgs e)
        {
            disable_buttons();
            var myValue = ((Button)sender).Tag;
            selectionDisplay = null;
            selectionDisplay = new SelectionDisplay(myValue.ToString());
            Bedroom_Image.Children.Add(selectionDisplay);
            selectionDisplay.Focus();
            selectionDisplay.Unloaded += UpdateButtons;
        }

        private void UpdateButtons(object sender, RoutedEventArgs e)
        {
            enable_Right_Buttons();
        }


        #region For testing the animations
        private void moving(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(shirt6, "Moving", true);
            
        }
        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            

        }
    }
}
