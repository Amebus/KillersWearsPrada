using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Model
{
    [Serializable]
    // this class represents the item in the game
    public class Item : ISerializable
    {

        private int code;
        private String barcode;
        private String itemName;
        private Double price;
        private String description;
        private String reparto;
        private String textureFileName;
        private String maskFileName;
        private String imageFileName;

        private String attClueText;

        private Boolean attTrashed;
        private Boolean attInInventory;
        private Boolean attDressed;

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
        /// <param name="image">string representing the item image file name</param>
        public Item(int c, String bc, String name, Double p, String descr, String rep, String texture, String mask, String image)
        {
            code = c;
            barcode = bc;
            itemName = name;
            price = p;
            description = descr;
            reparto = rep;
            textureFileName = texture;
            maskFileName = mask;
            imageFileName = image;

            attTrashed = false;
            attInInventory = false;
            attDressed = false;
        }

        #region Properties
        public int Code
        {
            get { return code; }
        }

        public String BarCode
        {
            get { return barcode; }
        }

        public Double Price
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

        public String ImageFileName
        {
            get { return imageFileName; }
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

        public String ClueText
        {
            get { return attClueText; }
        }
        #endregion

        #region Methods
        public void SetClueText (String ClueText)
        {
            attClueText = ClueText;
        }
        public Boolean SetAsTrashed()
        {
            if (!this.IsInInventory)
                return false;
            attTrashed = true;
            attDressed = false;

            return true;
        }

        public Boolean RestoreFromTrash()
        {
            if (!this.IsTrashed)
                return false;
            attTrashed = false;
            return true;
        }

        public void SetAsInInventory()
        {
            attInInventory = true;
        }

        public Boolean Dress()
        {
            if (!this.IsInInventory || this.IsTrashed)
                return false;
            bool b = this.Undress();
            attDressed = true;
            return true;
        }

        /// <summary>
        /// It allows to undress an item
        /// </summary>
        /// <returns>true if the it has been undressed, false otherwise</returns>
        public Boolean Undress()
        {
            if (!this.IsDressed)
                return false;

            attDressed = false;
            return true;
        }

        public override string ToString()
        {
            return attClueText;
        }
        #endregion
    }
}
