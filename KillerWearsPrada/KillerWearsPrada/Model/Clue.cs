using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Model
{
    [Serializable]
    public class Clue : ISerializable
    {
        private const string BASE = "Un testimone si ricorda che il capo ";

        private bool attPositive;
        private E_Color attColor;
        private E_Gradiation attGradiation;
        private E_Shape attShape;
        private E_Texture attTexture;
        // non serve private E_ItemKind attItemKind;
        //private String color;
        //private bool gradiation;
        //private bool shape;
        //private String texture;
        //private String itemKind;

        /// <summary>
        ///  constructor that initiates a Clue object using input parameters
        /// value = NULL fot the unused attributes
        /// </summary>
        /// <param name="Positive"></param>
        /// <param name="Gradiation"></param>
        /// <param name="Shape"></param>
        /// <param name="Color"></param>
        /// <param name="Texture"></param>
        /// <param name="ItemKind"></param>
        /// 
        public Clue(Boolean Positive, E_Gradiation Gradiation, E_Shape Shape, E_Color Color, E_Texture Texture) //, E_ItemKind ItemKind)
        {
            attPositive = Positive;

            attGradiation = Gradiation;
            attShape = Shape;

            attColor = Color;
            attTexture = Texture;
            //attItemKind = ItemKind;
            
            //attColor = (E_Color) Enum.Parse(typeof(E_Color), Color.ToUpper());
            //attTexture = (E_Texture)Enum.Parse(typeof(E_Texture), Texture.ToUpper());
            //attItemKind = (E_ItemKind)Enum.Parse(typeof(E_ItemKind), ItemKind.ToUpper());
        }

        public override string ToString()
        {
            String wvClue = BASE;
            if (!this.attPositive)
            {
                wvClue += "non ";
            }
            wvClue += "è ";
            if (attGradiation != 0)
                wvClue += attGradiation.ToString();
            else if (attShape != 0)
                wvClue += attShape.ToString();
            else if (attColor != 0)
                wvClue += attColor.ToString();
            else if (attTexture != 0)
            {
                wvClue += RemoveUnderScore(attTexture.ToString());
            }
            wvClue += "!";
            return wvClue;
        }

        private string RemoveUnderScore(string v)
        {
            return v.Replace('_', ' ');
        }
    }
}
