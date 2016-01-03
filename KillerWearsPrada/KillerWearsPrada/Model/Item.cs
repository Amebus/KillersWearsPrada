using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Model
{
    [Serializable]
    // this class represents the item in the game
    class Item : ISerializable
    {
        private Clue attClue;

        private int code;
        private int barcode;
        private String itemName;
        private float price;
        private String description;
        private String reparto;
        private String textureFileName;
        private String maskFileName;

        public Item ()
        {
            attClue = new Clue();
        }

        public Item(int c, int bc, String name, float p, String descr, String rep, String texture, String mask)
        {
            attClue = new Clue();
            code = c;
            barcode = bc;
            itemName = name;
            price = p;
            description = descr;
            reparto = rep;
            textureFileName = texture;
            maskFileName = mask;
        }

        public Clue Clue
        {
            get { return attClue; }
        }

        public int Code
        {
            get { return code; }
        }

        public int BarCode
        {
            get { return barcode; }
        }

        public float Price
        {
            get { return price; }
        }

        public String ItemName
        {
            get { return itemName; }
        }

        public String Description
        {
            get { return description; }
        }

        public String Reparto
        {
            get { return reparto; }
        }

        public String TextureFilename
        {
            get { return textureFileName; }
        }

        public String MaskFileName
        {
            get { return maskFileName; }
        }
    }
}
