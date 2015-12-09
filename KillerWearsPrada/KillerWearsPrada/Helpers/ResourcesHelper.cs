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
            String path = Directory;

            if (Directory[0]!=('\\'))
                path = "\\" + Directory;

            return path;
        }

        /// <summary>
        /// Ottiene la Directory di lavoro corrente del programma come stringa
        /// </summary>
        public static String CurrentDirectory
        {
            get { return Application.Current.Resources["CurrentDirectory"].ToString(); }
        }

        /// <summary>
        /// Salva la directory corrente nella risorsa ["CurrentDirectory"]
        /// </summary>
        public static void SaveCurrentDirectory()
        {
            String dir = Directory.GetCurrentDirectory().ToString();
            Application.Current.Resources["CurrentDirectory"] = dir;
        }

        /// <summary>
        /// Imposta il Path relativo per 
        /// </summary>
        public static void ModifyMainBackgroundPath ()
        {
            String dir_ok = "";
            String dir = CurrentDirectory;

            String[] dirs = dir.Split('\\');
            dir_ok = dirs[0];
            for (int i = 1; i < dirs.Length - 2; i++)
                dir_ok = dir_ok + "\\" + dirs[i];

            dir_ok += CreatePath(Application.Current.Resources["Images"].ToString());
            dir_ok += CreatePath(Application.Current.Resources["Application_Start_Image"].ToString());
            Application.Current.Resources["Application_Start_Image"] = dir_ok;
        }

    }
}
