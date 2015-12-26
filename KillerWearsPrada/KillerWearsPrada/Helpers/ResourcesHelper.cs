using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KillerWearsPrada.Helpers
{
    static public class ResourcesHelper
    {
        /// <summary>
        /// Aggiungo \ davanti al path se non presente
        /// 
        /// NB: non passare percorsi assoluti, ma solo relativi
        /// </summary>
        /// <param name="Directory">Path da modificare</param>
        /// <returns></returns>
        private static String CreatePath(String Directory)
        {
            String wvPath = Directory;

            if (Directory[0]!=('\\'))
                wvPath = "\\" + Directory;

            return wvPath;
        }

        /// <summary>
        /// Ottiene la Directory di lavoro corrente del programma come stringa
        /// </summary>
        public static String CurrentDirectory
        {
            get { return Application.Current.Resources["CurrentDirectory"].ToString(); }
        }

        public static String ImagesDirectory
        {
            get { return Application.Current.Resources["ImagesDir"].ToString(); }
        }

        /// <summary>
        /// Salva la directory corrente nella risorsa ["CurrentDirectory"]
        /// </summary>
        public static void SaveCurrentDirectory()
        {
            String wvDir = Directory.GetCurrentDirectory().ToString();
            Application.Current.Resources["CurrentDirectory"] = wvDir;
        }

        /// <summary>
        /// Imposta il Path relativo per 
        /// </summary>
        public static void ModifyMainBackgroundPath ()
        {
            String wvRightPath = CurrentDirectory;
            //String dir = CurrentDirectory;
            /*
            String[] dirs = dir.Split('\\');
            dir_ok = dirs[0];
            for (int i = 1; i < dirs.Length - 2; i++)
                dir_ok = dir_ok + "\\" + dirs[i];
            */
            wvRightPath += CreatePath(Application.Current.Resources["ImagesDir"].ToString());
            wvRightPath += CreatePath(Application.Current.Resources["Application_Start_Image"].ToString());
            Application.Current.Resources["Application_Start_Image"] = wvRightPath;
        }


        public static void ModifyRoomBackgroundPath(String roomImage)
        {
            String wvRightPath = CurrentDirectory;
            //String dir = CurrentDirectory;
            /*
            String[] dirs = dir.Split('\\');
            dir_ok = dirs[0];
            for (int i = 1; i < dirs.Length - 2; i++)
                dir_ok = dir_ok + "\\" + dirs[i];
            */
            wvRightPath += CreatePath(Application.Current.Resources["ImagesDir"].ToString());
            wvRightPath += CreatePath(Application.Current.Resources[roomImage].ToString());
            Application.Current.Resources[roomImage] = wvRightPath;
        }
        

    }
}
