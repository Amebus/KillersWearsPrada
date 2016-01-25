using KillerWearsPrada.Helpers;
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
            System.IO.DirectoryInfo di = new DirectoryInfo(SketchesPathsFile());

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
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

            #region foreach per popolare bottoni stanze
            //dovrei prendere dal controller tutti gli items e metterli nelle varie stanze!!!
            foreach (Model.Room r in MainWindow.attGameController.Game.Rooms)
            {

                switch (r.Name)
                {
                    case Model.E_RoomsName.START_ROOM:
                        break;
                    case Model.E_RoomsName.LIVINGROOM:
                        {
                            // devo fare il check per vedere che maschera usare!!!
                            //TODO cambiare ordine caricamento cose
                            //gurado che maschere hanno, e assegno le varie immagini e i vari tag ai vari bottoni
                            trousers1.Tag = r.Items[0].BarCode;
                            //  trousers1.Tag = i.BarCode;
                            //devo creare l'immagine giusta da mostrare nella stanza! mi serve la mask, e la texture
                            trousers1Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_LivingroomImages.Trousers1)), ResourcesHelper.TexturesPath(r.Items[0].TextureFilename), "panta1image.png");

                            trousers3.Tag = r.Items[2].BarCode;
                            trousers3Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_LivingroomImages.Trousers3)), ResourcesHelper.TexturesPath(r.Items[2].TextureFilename), "panta3image.png");

                            //altri bottoni da aggiungere
                                trousers2.Tag = r.Items[1].BarCode;
                                trousers2Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_LivingroomImages.Trousers2)), ResourcesHelper.TexturesPath(r.Items[1].TextureFilename), "panta2image.png");
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
                   /*         string[] hatmasks = new string[6] { "", "", "", "", "", "" };
                            for(int i =0; i<hatmasks.Count(); i++)
                            {
                                hatmasks[i] = ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_KitchenImages.)
                            }
                            */
                            hat1.Tag = r.Items[0].BarCode;
                            if(r.Items[0].Shape == Model.E_Shape.LONG)
                                hat1Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_KitchenImages.Hat1hat)), ResourcesHelper.TexturesPath(r.Items[0].TextureFilename), "hat1image.png");
                            else
                                hat1Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_KitchenImages.Hat1cap)), ResourcesHelper.TexturesPath(r.Items[0].TextureFilename), "hat1image.png");


                            hat3.Tag = r.Items[2].BarCode;
                            if (r.Items[2].Shape == Model.E_Shape.LONG)
                                hat3Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_KitchenImages.Hat3hat)), ResourcesHelper.TexturesPath(r.Items[2].TextureFilename), "hat3image.png");
                            else
                                hat3Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_KitchenImages.Hat3cap)), ResourcesHelper.TexturesPath(r.Items[2].TextureFilename), "hat3image.png");
                            /*    hat6.Tag = r.Items[5].BarCode;
                                hat6Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_KitchenImages.Hat6)), ResourcesHelper.TexturesPath(r.Items[5].TextureFilename), "hat6image.png");*/
                  
                                  hat2.Tag = r.Items[1].BarCode;
                            if (r.Items[1].Shape == Model.E_Shape.LONG)
                                hat2Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_KitchenImages.Hat2hat)), ResourcesHelper.TexturesPath(r.Items[1].TextureFilename), "hat2image.png");
                            else
                                hat2Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_KitchenImages.Hat2cap)), ResourcesHelper.TexturesPath(r.Items[1].TextureFilename), "hat2image.png");

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
                            //TODO cambiare ordine caricamento cose
                            shirt3.Tag = r.Items[2].BarCode;
                            shirt3Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_BedroomImages.Shirt3)), ResourcesHelper.TexturesPath(r.Items[2].TextureFilename), "shirt3image.png");
                            shirt4.Tag = r.Items[3].BarCode;
                            shirt4Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_BedroomImages.Shirt4)), ResourcesHelper.TexturesPath(r.Items[3].TextureFilename), "shirt4image.png");

                            //altri bottoni da aggiungere
                            shirt5.Tag = r.Items[4].BarCode;
                            shirt5Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_BedroomImages.Shirt5)), ResourcesHelper.TexturesPath(r.Items[4].TextureFilename), "shirt5image.png");
                            shirt6.Tag = r.Items[5].BarCode;
                            shirt6Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_BedroomImages.Shirt6)), ResourcesHelper.TexturesPath(r.Items[5].TextureFilename), "shirt6image.png");
                            shirt2.Tag = r.Items[1].BarCode;
                            shirt2Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_BedroomImages.Shirt2)), ResourcesHelper.TexturesPath(r.Items[1].TextureFilename), "shirt2image.png");
                            shirt1.Tag = r.Items[0].BarCode;
                            shirt1Image = SketchHelper.CreateSketchesPath(ResourcesHelper.MasksPaths(ResourcesHelper.GetResource(E_BedroomImages.Shirt1)), ResourcesHelper.TexturesPath(r.Items[0].TextureFilename), "shirt1image.png");
                        }
                        break;
                }
            }
            #endregion
        }



        /// <summary>
        /// Set canvas background acconrdingly to the door selected
        /// </summary>
        /// <param name="roomImagePath"></param>
        public void setBackgroundCanvas(string roomImagePath)
        {
            imageBackground = new ImageBrush();
            imageBackground.ImageSource = new BitmapImage(new Uri(@roomImagePath, UriKind.Absolute));
            room_Canvas.Background = imageBackground;
        }

        /*
        public void setImageBrush1(string roomImagePath)
        {
            ib1 = new ImageBrush();
            ib1.ImageSource = new BitmapImage(new Uri(@roomImagePath, UriKind.Absolute));
        }

        public void setImageBrush2(string roomImagePath)
        {
            ib1 = new ImageBrush();
            ib1.ImageSource = new BitmapImage(new Uri(@roomImagePath, UriKind.Absolute));
        }

        public void setImageBrush3(string roomImagePath)
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

            //imposto l'indice della room in cui sta andando il player
            // 0 credo sia l'entrata
            // MainWindow.attGameController.ActualRoomIndex = 0;
            //funziona
            MainWindow.attGameController.Game.ActualRoomIndex = 0;
            disable_buttons();

            

            Livingroom_Image.Visibility = Visibility.Hidden;
            Kitchen_Image.Visibility = Visibility.Hidden;
            Bedroom_Image.Visibility = Visibility.Hidden;
            
            MainWindow yourParentWindow = (MainWindow)Window.GetWindow(this);
            yourParentWindow.Room.Visibility = Visibility.Hidden;

 //           yourParentWindow.StartRoom.UpdateLayout(); // <---------------------------------------------QUI

            yourParentWindow.StartRoom.Visibility = Visibility.Visible;

            // abilito i bottoni delle 3 porte e gli altri
            yourParentWindow.changeDoorColor();
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

        public void change_KitchenButtons_Status(bool b)
        {
            hat1.IsEnabled = b;
            hat3.IsEnabled = b;
        }

        public void change_LivingroomButtons_Status(bool b)
        {
            trousers1.IsEnabled = b;
            trousers3.IsEnabled = b;
        }

        public void change_BedroomButtons_Status(bool b)
        {
            shirt3.IsEnabled = b;
            shirt4.IsEnabled = b;
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

        private void moving(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(trousers1, "Moving", true);
         /*   Storyboard sb = this.FindResource("moveButton") as Storyboard;
            Storyboard.SetTarget(sb, hat1);
            sb.Begin();*/
        }
    }
}
