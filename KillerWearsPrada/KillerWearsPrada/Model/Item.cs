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


        private Boolean attTrashed;
        private Boolean attInInventory;
        private Boolean attDressed;

        public Item ()
        {
            attClue = new Clue();
        }

        /// <summary>
        /// constructor which initiates an Item object with parameters
        /// It's the only way to set the attributes
        /// </summary>
        /// <param name="c">integer representig the item code</param>
        /// <param name="bc"> integer representing the barcode</param>
        /// <param name="name">string representing the item name</param>
        /// <param name="p">float representing the price</param>
        /// <param name="descr">string representing the description</param>
        /// <param name="rep">string representing the Reparto</param>
        /// <param name="texture">string representing the texture file name</param>
        /// <param name="mask">string representing the mask file name</param>
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

            attTrashed = false;
            attInInventory = false;
            attDressed = false;
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

        public Boolean IsTrashed
        {
            get { return attTrashed; }
        }

        public Boolean IsInInventory
        {
            get { return attInInventory; }
        }

        public Boolean IsDressed
        {
            get { return attDressed; }
        }
    }
}
