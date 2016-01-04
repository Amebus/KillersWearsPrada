using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Model
{
    [Serializable]
    class Clue : ISerializable
    {
        private const string BASE = "Un testimone si ricorda che ";

        private bool attPositive;
        private E_Color attColor;
        private E_Gradiation attGradiation;
        private E_Shape attShape;
        private E_Texture attTexture;
        private E_ItemKind attItemKind;
        //private String color;
        //private bool gradiation;
        //private bool shape;
        //private String texture;
        //private String itemKind;

        public Clue()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Positive"></param>
        /// <param name="Gradiation"></param>
        /// <param name="Shape"></param>
        /// <param name="Color"></param>
        /// <param name="Texture"></param>
        /// <param name="ItemKind"></param>
        public Clue(Boolean Positive, Boolean Gradiation, Boolean Shape, String Color, String Texture, String ItemKind)
        {
            attPositive = Positive;
            attGradiation = (Gradiation) ? E_Gradiation.CHIARO : E_Gradiation.SCURO;
            attShape = (Shape) ? E_Shape.CORTO : E_Shape.LUNGO;

            attColor = (E_Color) Enum.Parse(typeof(E_Color), Color.ToUpper());
            attTexture = (E_Texture)Enum.Parse(typeof(E_Texture), Texture.ToUpper());
            attItemKind = (E_ItemKind)Enum.Parse(typeof(E_ItemKind), ItemKind.ToUpper());
        }

        public override string ToString()
        {
            String wvClue = BASE;

            throw new NotImplementedException("Finire la generazione della stringa dell'indizio");

            return wvClue;
        }
    }
}
