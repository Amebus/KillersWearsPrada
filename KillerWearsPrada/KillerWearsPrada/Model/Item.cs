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
        private string barcode;
        private string itemName;
        private Double price;
        private string description;
        private string reparto;
        private string textureFileName;        
        private string imageFileName;
        private string itemKind;

        private string attClueText;

        private bool attTrashed;
        private bool attInInventory;
        private bool attDressed;

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
        /// <param name="image">string representing the item image file name</param>
        /// <param name="kind">string representing the item kind</param>
        public Item(int c, string bc, string name, Double p, string descr, string rep, string texture, string image, string kind)
        {
            code = c;
            barcode = bc;
            itemName = name;
            price = p;
            description = descr;
            reparto = rep;
            textureFileName = texture;            
            imageFileName = image;
            itemKind = kind;

            attTrashed = false;
            attInInventory = false;
            attDressed = false;
        }

        #region Properties
        public int Code
        {
            get { return code; }
        }

        public string BarCode
        {
            get { return barcode; }
        }

        public Double Price
        {
            get { return price; }
        }

        public string ItemName
        {
            get { return itemName; }
        }

        public string Description
        {
            get { return description; }
        }

        public string Reparto
        {
            get { return reparto; }
        }

        public string TextureFilename
        {
            get { return textureFileName; }
        }

        public string ItemKind
        {
            get { return itemKind; }
        }

        public string ImageFileName
        {
            get { return imageFileName; }
        }

        public bool IsTrashed
        {
            get { return attTrashed; }
        }

        public bool IsInInventory
        {
            get { return attInInventory; }
        }

        public bool IsDressed
        {
            get { return attDressed; }
        }

        public string ClueText
        {
            get { return attClueText; }
        }
        #endregion

        #region Methods
        public void SetClueText (string ClueText)
        {
            attClueText = ClueText;
        }

        public bool SetAsTrashed()
        {
            if (!this.IsInInventory)
                return false;
            attTrashed = true;
            attDressed = false;

            return true;
        }

        public bool RestoreFromTrash()
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

        public bool Dress()
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
        public bool Undress()
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
