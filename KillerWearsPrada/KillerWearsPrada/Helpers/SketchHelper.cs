using ImageMagick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KillerWearsPrada.Helpers
{
    static public class SketchHelper
    {

        /// <summary>
        /// Sets the directory for Magick
        /// </summary>
        public static void SetDirectories()
        {
            Helpers.ResourcesHelper.ModifyAllMagickPath();
            System.IO.Directory.CreateDirectory(Application.Current.Resources[Helpers.ResourcesHelper.E_Magick_Dirs.CacheDirectoryMagick.ToString()].ToString());
            MagickAnyCPU.CacheDirectory = Application.Current.Resources[Helpers.ResourcesHelper.E_Magick_Dirs.CacheDirectoryMagick.ToString()].ToString();

            System.IO.Directory.CreateDirectory((string)Application.Current.Resources[ResourcesHelper.E_Magick_Dirs.GhostscriptDirectory.ToString()]);
            MagickNET.SetGhostscriptDirectory((string)Application.Current.Resources[ResourcesHelper.E_Magick_Dirs.GhostscriptDirectory.ToString()]);
            // MagickNET.Initialize(@"C:\Users\Monica\Documents\MagickTestFilesVari\MyImageMagickXmlFiles");
            System.IO.Directory.CreateDirectory((string)Application.Current.Resources[ResourcesHelper.E_Magick_Dirs.TempDirectory.ToString()]);
            MagickNET.SetTempDirectory((string)Application.Current.Resources[ResourcesHelper.E_Magick_Dirs.TempDirectory.ToString()]);
            // questi sopra vanno bene
            // Samples.MagickNET.CombiningImagesSamples.MergeMultipleImages();
            System.IO.Directory.CreateDirectory(ResourcesHelper.SketchesPathsFile());
            //CreateSketches(Helpers.ResourcesHelper.MasksPaths("hat_1meleOk.png"), Helpers.ResourcesHelper.TexturesPath("redTartan.jpg"), "ciao.png");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mask"></param> is the path of the Mask image
        /// <param name="texture"></param> is the path of the Texture image
        /// <param name="nameSketch"></param> is the name of the image sketch created, like "ciao.png"
        public static void CreateSketches(string mask, string texture, string nameSketch)
        {

            MagickImage Mask = new MagickImage(mask);

            MagickImage Texture = new MagickImage(texture);

            Texture.Crop(Mask.Width, Mask.Height);

            Texture.Composite(Mask, CompositeOperator.CopyAlpha);
            Mask.Composite(Texture, CompositeOperator.Multiply);
            MagickImage sketch = Mask;
            
            sketch.Write(Helpers.ResourcesHelper.SketchesPath() + nameSketch);

        }

        /// <summary>
        /// ritorna il path dell'oggetto creato        /// </summary>
        /// <param name="mask"></param>
        /// <param name="texture"></param>
        /// <param name="nameSketch"></param>
        public static string CreateSketchesPath(string mask, string texture, string nameSketch)
        {

            MagickImage Mask = new MagickImage(mask);

            MagickImage Texture = new MagickImage(texture); 

            Texture.Crop(Mask.Width, Mask.Height);

            Texture.Composite(Mask, CompositeOperator.CopyAlpha);
            Mask.Composite(Texture, CompositeOperator.Multiply);
            MagickImage sketch = Mask;

            try
            {
                // sketch.Write(Helpers.ResourcesHelper.SketchesPath() + nameSketch);
                string p = Helpers.ResourcesHelper.SketchesPath() + nameSketch;
                System.IO.Stream s = new System.IO.FileStream(p, System.IO.FileMode.Create);
                
                sketch.Write(s);
                s.Close();
            }
            catch (MagickException ex)
            {
                string s= ex.Message;
            }
            catch
            {

            }
            sketch.Dispose();
            
            string path = Helpers.ResourcesHelper.SketchesPath() + nameSketch;
            return path;

        }
    }
}
