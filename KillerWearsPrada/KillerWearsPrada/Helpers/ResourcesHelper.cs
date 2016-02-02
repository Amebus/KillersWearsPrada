using System;
using System.IO;
using System.Windows;

namespace KillerWearsPrada.Helpers
{
    static public class ResourcesHelper
    {
        public enum E_Direcetories
        {
            CurrentDirectory,
            ImagesDir,
            SavesDir,
            TexturesDir,
            ItemsImagesDir,
            MasksDir,
            MagickDir,
            SketchesImages
        }

        public enum E_Magick_Dirs
        {
            GhostscriptDirectory,
            CacheDirectoryMagick,
            TempDirectory
        }

        public enum E_GenericImages
        {
            Application_Start_Image,
            Inventory_Background,
            Welcome_Background,

            Start_Image,
            StartOver_Image,
            StartPressed_Image,
            Welcome_Image,

            Selection_Crime,
            Selection_Background,

            Trash_Empty,
            Trash_Full,
            Fumetto,
            Sagoma,
            Pergamena,
            TrashBackground,
            FireworksBackground
        }

        public enum E_RoomsImages
        {
            Doors_Image,
            Bedroom_Image,
            Kitchen_Image,
            Livingroom_Image
        }

        public enum E_DoorsImages
        {
            SXdoor_Image,
            CENTERdoor_Image,
            DXdoor_Image,
            SXdoorDisabled_Image,
            CENTERdoorDisabled_Image,
            DXdoorDisabled_Image
        }


        public enum E_KitchenImages
        {
            Hat1hat,
            Hat2hat,
            Hat3hat,
            Hat4hat,
            Hat5hat,
            Hat6hat,

            Hat1cap,
            Hat2cap,
            Hat3cap,
            Hat4cap,
            Hat5cap,
            Hat6cap
        }

        public enum E_LivingroomImages
        {
            Trousers1,
            Trousers2,
            Trousers3,
            Trousers4,
            Trousers5,
            Trousers6
        }

        public enum E_BedroomImages
        {
            Shirt1,
            Shirt2,
            Shirt3,
            Shirt4,
            Shirt5,
            Shirt6
        }

        /*
        public enum E_WelcomeImages
        {
            Start_Image,
            StartOver_Image,
            StartPressed_Image,
            Welcome_Image
        }
        */

        /// <summary>
        /// Aggiungo \ davanti al path se non presente
        /// 
        /// NB: non passare percorsi assoluti, ma solo relativi
        /// </summary>
        /// <param name="Directory">Path da modificare</param>
        /// <returns></returns>
        private static string CreatePath(string Directory)
        {
            string wvPath = Directory;

            if (Directory[0] != ('\\'))
                wvPath = "\\" + Directory;

            return wvPath;
        }

        #region Resource generic getter and setter
        private static object GetResource(string ResourceName)
        {
            return Application.Current.Resources[ResourceName.ToString()];


        }

        private static void SetResource(string ResourceName, object Value)
        {
            Application.Current.Resources[ResourceName] = Value;
        }
        #endregion

        #region Resource specific getter
        private static string GetResource(E_Direcetories ResourceName)
        {
            return GetResource(ResourceName.ToString()).ToString();
        }

        private static string GetResource(E_GenericImages ResourceName)
        {
            return GetResource(ResourceName.ToString()).ToString();
        }

        private static string GetResource(E_DoorsImages ResourceName)
        {
            return GetResource(ResourceName.ToString()).ToString();
        }

        private static string GetResource(E_RoomsImages ResourceName)
        {
            return GetResource(ResourceName.ToString()).ToString();
        }

        //public because I need them in Room.xaml.cs
        public static string GetResource(E_KitchenImages ResourceName)
        {
            return GetResource(ResourceName.ToString()).ToString();
        }

        public static string GetResource(E_LivingroomImages ResourceName)
        {
            return GetResource(ResourceName.ToString()).ToString();
        }

        public static string GetResource(E_BedroomImages ResourceName)
        {
            return GetResource(ResourceName.ToString()).ToString();
        }


        private static string GetResource(E_Magick_Dirs ResourceName)
        {
            return GetResource(ResourceName.ToString()).ToString();
        }
        #endregion

        #region Resource specifc setters
        private static void SetResource(E_Direcetories ResourceName, string Value)
        {
            SetResource(ResourceName.ToString(), Value);
        }

        private static void SetResource(E_GenericImages ResourceName, string Value)
        {
            SetResource(ResourceName.ToString(), Value);
        }

        private static void SetResource(E_DoorsImages ResourceName, string Value)
        {
            SetResource(ResourceName.ToString(), Value);
        }

        private static void SetResource(E_RoomsImages ResourceName, string Value)
        {
            SetResource(ResourceName.ToString(), Value);
        }

        private static void SetResource(E_LivingroomImages ResourceName, string Value)
        {
            SetResource(ResourceName.ToString(), Value);
        }

        private static void SetResource(E_BedroomImages ResourceName, string Value)
        {
            SetResource(ResourceName.ToString(), Value);
        }

        private static void SetResource(E_KitchenImages ResourceName, string Value)
        {
            SetResource(ResourceName.ToString(), Value);
        }

        private static void SetResource(E_Magick_Dirs ResourceName, string Value)
        {
            SetResource(ResourceName.ToString(), Value);
        }
        #endregion

        /// <summary>
        /// Ottiene la Directory di lavoro corrente del programma come stringa
        /// </summary>
        public static string CurrentDirectory
        {
            get { return GetResource(E_Direcetories.CurrentDirectory); }
        }

        /// <summary>
        /// Return a <see cref="string "/> which represent the absolute path of the <see cref="E_Direcetories.ImagesDir"/> directory
        /// </summary>
        public static string ImagesDirectory
        {
            get
            {
                string wvPath = GetResource(E_Direcetories.CurrentDirectory);
                wvPath += CreatePath(GetResource(E_Direcetories.ImagesDir));
                return wvPath;
            }
        }

        /// <summary>
        /// Return a <see cref="string "/> which represent the absolute path of the <see cref="E_Direcetories.ItemsImagesDir"/> directory, plus "\\" 
        /// </summary>
        public static string ItemsImagesPath
        {
            get
            {
                string wvPath = GetResource(E_Direcetories.CurrentDirectory);
                wvPath += CreatePath(GetResource(E_Direcetories.ImagesDir));
                wvPath += CreatePath(GetResource(E_Direcetories.ItemsImagesDir));
                wvPath += "\\";
                return wvPath;
            }
        }

        #region path per magick directories (da rimuovere credo)
        /*     /// <summary>
             /// 
             /// </summary>
             public static string MagicksDirectory
             {
                 get
                 {
                     string wvPath = GetResource(E_Direcetories.CurrentDirectory);
                     wvPath += CreatePath(GetResource(E_Direcetories.MagickDir));
                     return wvPath;
                 }
             }

             /// <summary>
             /// 
             /// </summary>
             public static string SkecthesDirectory
             {
                 get
                 {
                     string wvPath = GetResource(E_Direcetories.CurrentDirectory);
                     wvPath += CreatePath(GetResource(E_Direcetories.SketchesImages));
                     return wvPath;
                 }
             }

             /// <summary>
             /// Return a <see cref="string "/> which represent the absolute path of the <see cref="E_Direcetories.TexturesDir"/> directory plus \
             /// </summary>
             public static string TexturesDirectory
             {
                 get
                 {
                     string wvPath = GetResource(E_Direcetories.CurrentDirectory);
                     wvPath += CreatePath(GetResource(E_Direcetories.TexturesDir));
                     return wvPath;
                 }
             }

             /// <summary>
             /// Return a <see cref="string "/> which represent the absolute path of the <see cref="E_Direcetories.ItemsImagesDir"/> directory
             /// </summary>
             public static string ItemsImagesDirectory
             {
                 get
                 {
                     string wvPath = GetResource(E_Direcetories.CurrentDirectory);
                     wvPath += CreatePath(GetResource(E_Direcetories.ItemsImagesDir));
                     return wvPath;
                 }
             }

             /// <summary>
             /// Return a <see cref="string "/> which represent the absolute path of the <see cref="E_Direcetories.MasksDir"/> directory
             /// </summary>
             public static string MasksDirectory
             {
                 get
                 {
                     string wvPath = GetResource(E_Direcetories.CurrentDirectory);
                     wvPath += CreatePath(GetResource(E_Direcetories.MasksDir));
                     return wvPath;
                 }
             }

             public static void SaveAllMagickPaths()
             {

                 string wvRightPath = CurrentDirectory;
                 wvRightPath += CreatePath(GetResource(E_Direcetories.ImagesDir));
                 wvRightPath += CreatePath(GetResource(E_GenericImages.Application_Start_Image));
                 SetResource(E_GenericImages.Application_Start_Image, wvRightPath);
             }*/
        #endregion

        /// <summary>
        /// Return a <see cref="string "/> which represent the absolute path of the <see cref="E_Direcetories.SavesDir"/> directory
        /// </summary>
        public static string SavesDirectory
        {
            get
            {
                string wvPath = GetResource(E_Direcetories.CurrentDirectory);
                wvPath += CreatePath(GetResource(E_Direcetories.SavesDir));
                return wvPath;
            }
        }





        /// <summary>
        /// Salva la directory corrente nella risorsa ["CurrentDirectory"]
        /// </summary>
        public static void SaveCurrentDirectory()
        {
            string wvDir = Directory.GetCurrentDirectory().ToString();
            SetResource(E_Direcetories.CurrentDirectory, wvDir);
        }

        /// <summary>
        /// Imposta il Path relativo per 
        /// </summary>
        public static void ModifyMainBackgroundPath()
        {
            string wvRightPath = CurrentDirectory;
            //string dir = CurrentDirectory;    
            /*
            string[] dirs = dir.Split('\\');
            dir_ok = dirs[0];
            for (int i = 1; i < dirs.Length - 2; i++)
                dir_ok = dir_ok + "\\" + dirs[i];
            */
            wvRightPath += CreatePath(GetResource(E_Direcetories.ImagesDir));
            wvRightPath += CreatePath(GetResource(E_GenericImages.Application_Start_Image));
            SetResource(E_GenericImages.Application_Start_Image, wvRightPath);
        }




        public static void ModifyRoomBackgroundPath(E_RoomsImages ResourceName)
        {
            string wvRightPath = CurrentDirectory;
            wvRightPath += CreatePath(GetResource(E_Direcetories.ImagesDir));
            wvRightPath += CreatePath(GetResource(ResourceName));
            SetResource(ResourceName, wvRightPath);
            //     Application.Current.Resources[roomImage] = wvRightPath;
        }

        public static void ModifyDoorsPath(E_DoorsImages ResourceName)
        {
            string wvRightPath = CurrentDirectory;
            wvRightPath += CreatePath(GetResource(E_Direcetories.ImagesDir));
            wvRightPath += CreatePath(GetResource(ResourceName));
            SetResource(ResourceName, wvRightPath);
            //     Application.Current.Resources[roomImage] = wvRightPath;
        }

        public static void ModifyGenericImagesPath(E_GenericImages ResourceName)
        {
            string wvRightPath = CurrentDirectory;
            wvRightPath += CreatePath(GetResource(E_Direcetories.ImagesDir));
            wvRightPath += CreatePath(GetResource(ResourceName));
            SetResource(ResourceName, wvRightPath);
            //     Application.Current.Resources[roomImage] = wvRightPath;
        }


        public static void ModifyKitchenImagesPath(E_KitchenImages ResourceName)
        {
            string wvRightPath = CurrentDirectory;
            wvRightPath += CreatePath(GetResource(E_Direcetories.ImagesDir));
            wvRightPath += CreatePath(GetResource(ResourceName));
            SetResource(ResourceName, wvRightPath);
            //     Application.Current.Resources[roomImage] = wvRightPath;
        }

        public static void ModifyLivingroomImagesPath(E_LivingroomImages ResourceName)
        {
            string wvRightPath = CurrentDirectory;
            wvRightPath += CreatePath(GetResource(E_Direcetories.ImagesDir));
            wvRightPath += CreatePath(GetResource(ResourceName));
            SetResource(ResourceName, wvRightPath);
            //     Application.Current.Resources[roomImage] = wvRightPath;
        }

        public static void ModifyBedroomImagesPath(E_BedroomImages ResourceName)
        {
            string wvRightPath = CurrentDirectory;
            wvRightPath += CreatePath(GetResource(E_Direcetories.ImagesDir));
            wvRightPath += CreatePath(GetResource(ResourceName));
            SetResource(ResourceName, wvRightPath);
            //     Application.Current.Resources[roomImage] = wvRightPath;
        }

        #region altri path in images

        

        public static void ModifyAllMagickPath()
        {
            //setto il giusto path per arrivare a Magick directory
            string wvRightPath = CurrentDirectory;
       /*     wvRightPath += CreatePath(GetResource(E_Direcetories.ImagesDir));
            wvRightPath += CreatePath(GetResource(E_Direcetories.MagickDir));
            SetResource(E_Direcetories.MagickDir, wvRightPath);*/
            //     Application.Current.Resources[roomImage] = wvRightPath;
            ModifyOthersMagickPaths(E_Magick_Dirs.CacheDirectoryMagick);
            ModifyOthersMagickPaths(E_Magick_Dirs.GhostscriptDirectory);
            ModifyOthersMagickPaths(E_Magick_Dirs.TempDirectory);
        }

        public static void ModifyOthersMagickPaths(E_Magick_Dirs m)
        {
            string wvRightPath = CurrentDirectory;
            wvRightPath += CreatePath(GetResource(E_Direcetories.ImagesDir));
            wvRightPath += CreatePath(GetResource(E_Direcetories.MagickDir));
            wvRightPath += CreatePath(GetResource(m));
            SetResource(m, wvRightPath);
        }

        public static string TexturesPath(string name)
        {
            string wvRightPath = CurrentDirectory;
            wvRightPath += CreatePath(GetResource(E_Direcetories.ImagesDir));
            wvRightPath += CreatePath(GetResource(E_Direcetories.TexturesDir));
            wvRightPath += "\\" + name;
            return wvRightPath;
        }

        public static string TexturesPath()
        {
            string wvRightPath = CurrentDirectory;
            wvRightPath += CreatePath(GetResource(E_Direcetories.ImagesDir));
            wvRightPath += CreatePath(GetResource(E_Direcetories.TexturesDir));
            wvRightPath += "\\";
            return wvRightPath;
        }
        /*
                public static string CachePath()
                {
                    string wvRightPath = CurrentDirectory;
                    wvRightPath += CreatePath(GetResource(E_Direcetories.ImagesDir));
                    wvRightPath += CreatePath(GetResource(E_Direcetories.MagickDir));
                    wvRightPath += "\\" + E_Magick_Dirs.CacheDirectoryMagick.ToString();
                    return wvRightPath;
                }*/

        public static string ItemsImagesPaths()
        {
            string wvRightPath = CurrentDirectory;
            wvRightPath += CreatePath(GetResource(E_Direcetories.ImagesDir));
            wvRightPath += CreatePath(GetResource(E_Direcetories.ItemsImagesDir));
            wvRightPath += "\\";
            return wvRightPath;
        }

        public static string ItemsImagesPaths(string name)
        {
            string wvRightPath = CurrentDirectory;
            wvRightPath += CreatePath(GetResource(E_Direcetories.ImagesDir));
            wvRightPath += CreatePath(GetResource(E_Direcetories.ItemsImagesDir));
            wvRightPath += "\\" + name;
            return wvRightPath;
        }

        public static string MasksPaths()
        {
            string wvRightPath = CurrentDirectory;
            wvRightPath += CreatePath(GetResource(E_Direcetories.ImagesDir));
            wvRightPath += CreatePath(GetResource(E_Direcetories.MasksDir));
            wvRightPath += "\\";
            return wvRightPath;
        }

        public static string MasksPaths(string name)
        {
            string wvRightPath = CurrentDirectory;
            wvRightPath += CreatePath(GetResource(E_Direcetories.ImagesDir));
            wvRightPath += CreatePath(GetResource(E_Direcetories.MasksDir));
            wvRightPath += "\\" + name;
            return wvRightPath;
        }

        /// <summary>
        /// This method returns the absolute path to the sketches directory plus the string "\\"
        /// </summary>
        /// <returns></returns>
        public static string SketchesPath()
        {
            string wvRightPath = CurrentDirectory;
            wvRightPath += CreatePath(GetResource(E_Direcetories.ImagesDir));
            wvRightPath += CreatePath(GetResource(E_Direcetories.SketchesImages));
            wvRightPath += "\\";
            return wvRightPath;
        }

        /// <summary>
        /// This method returns the absolute path to the sketches directory
        /// </summary>
        /// <returns></returns>
        public static string SketchesPathsFile()
        {
            string wvRightPath = CurrentDirectory;
            wvRightPath += CreatePath(GetResource(E_Direcetories.ImagesDir));
            wvRightPath += CreatePath(GetResource(E_Direcetories.SketchesImages));
            return wvRightPath;
        }

        /*      public static void ModifyMagickPath()
              {
                  string wvRightPath = CurrentDirectory;
                  wvRightPath += CreatePath(GetResource(E_Direcetories.ImagesDir));
                  wvRightPath += CreatePath(GetResource(E_Direcetories.MagickDir));
                  SetResource(E_Direcetories.MagickDir, wvRightPath);
              }

              public static string GetResourceMagick(E_Magick_Dirs ResourceName)
              {
                  return GetResource(ResourceName.ToString()).ToString();
              }
              */
        #endregion

            // mi sa che non mi serve
        /// <summary>
        /// Method to set the right path to the resources of the sketches in the kitchen room
        /// </summary>
        /// <param name="rightPath"></param>
        /// <param name="numHat"></param>
        public static void SetClothSketchesPath(string rightPath, E_KitchenImages numHat)
        {
            SetResource(numHat, rightPath);
        }
    }
}
