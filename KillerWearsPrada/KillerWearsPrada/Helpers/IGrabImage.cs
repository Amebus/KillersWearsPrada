using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Helpers
{
    public abstract class GrabImage
    {


        /// <summary>
        /// Restituisce un'implementazione della classe astratta <see cref="GrabImage"/> a seconda del parametro passato
        /// </summary>
        /// <param name="AsKinect">Se <code>True</code> ritorna implementazione che si interfaccia col kinect, altrimenti ritorna implementazione come test <param>
        /// <returns></returns>
        public static GrabImage GrabImageAsKinect(Boolean AsKinect)
        {
            if (AsKinect)
                return new GrabImageKinect();
            return new GrabImageTest();
        }

    }

    public class GrabImageTest : GrabImage
    {
        public GrabImageTest()
        {

        }
    }

    public class GrabImageKinect : GrabImage
    {
        public GrabImageKinect()
        {

        }
    }
}
