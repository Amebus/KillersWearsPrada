using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Model
{
    /// <summary>
    /// Indicates the type of object referring to the current paradigm :
    ///  1 - A means the CORRECT item for the solution
    ///  2 -    |   A = | 1 1 1 |
    ///         |   B = | 1 1 0 |
    ///         |   C = | 1 0 1 |
    ///         |   D = | 1 0 0 |
    ///         |   E = | 0 1 1 |
    ///         |   F = | 0 1 0 |
    /// </summary>
    public enum E_ItemType
    {
        A,
        B,
        C,
        D,
        E,
        F
    }

    [Serializable]
    public class AbstractItem : ISerializable
    {
        public E_ItemKind ItemKind { get; private set; }
        protected E_ItemType ItemType { get; private set; }
        private List<ItemGraficalProperty> attItemProperties;
        
        public AbstractItem(E_ItemType ItemType, E_ItemKind ItemKind)
        {
            this.ItemKind = ItemKind;
            this.ItemType = ItemType;
            this.attItemProperties = new List<ItemGraficalProperty>();
        }

        protected AbstractItem(AbstractItem AI)
        {
            this.attItemProperties = AI.attItemProperties;
            this.ItemType = AI.ItemType;
            this.ItemKind = AI.ItemKind;
        }

        public List<ItemGraficalProperty> ItemProperties
        {
            get { return attItemProperties; }
        }

        public int PropertiesCount
        {
            get { return attItemProperties.Count; }
        }

        public void AddProperty(ItemGraficalProperty Property)
        {
            attItemProperties.Add(Property);
        }

        public bool EqualsTo(AbstractItem AI)
        {
            if (this.ItemKind != AI.ItemKind)
                return false;
            if (this.ItemType != AI.ItemType)
                return false;

            for(int i =0; i<this.PropertiesCount; i++)
            {
                if (!this.ItemProperties[i].EqualsTo(AI.ItemProperties[i]))
                    return false;
            }
            
            return true;
        }

        public bool CheckPropertyByKind(E_PropertiesKind PropertyKind)
        {
            foreach (ItemGraficalProperty igp in ItemProperties)
            {
                if (igp.PropertyKind == PropertyKind)
                    return true;
            }

            return false;
        }

        public E_PropertiesKind GetPropertyKind(int Index)
        {
            if (Index >= ItemProperties.Count)
                Index = ItemProperties.Count - 1;
            else if (Index < 0)
                Index = 0;

            return ItemProperties[Index].PropertyKind;
        }

        public string GetProperty(E_PropertiesKind PropertyKind)
        {
            ItemGraficalProperty wvProperty = null;

            foreach(ItemGraficalProperty igp in ItemProperties)
            {
                if (igp.PropertyKind == PropertyKind)
                    wvProperty = igp;
            }

            if (wvProperty == null)
                return null;

            return ConvertProperty(wvProperty);

        }

        public string GetProperty (int Index)
        {
            if (Index >= ItemProperties.Count)
                Index = ItemProperties.Count - 1;
            else if (Index < 0)
                Index = 0;

            ItemGraficalProperty wvProperty = ItemProperties[Index];

            return ConvertProperty(wvProperty);
        }

        private string ConvertProperty(ItemGraficalProperty Property)
        {
            switch (Property.PropertyKind)
            {
                case E_PropertiesKind.COLOR:
                    return ((E_Color)Property.Property).ToString();
                case E_PropertiesKind.GRADIATION:
                    return ((E_Gradiation)Property.Property).ToString();
                case E_PropertiesKind.SHAPE:
                    return ((E_Shape)Property.Property).ToString();
                case E_PropertiesKind.TEXTURE:
                    return ((E_Texture)Property.Property).ToString();
                default:
                    return null;
            }
        }

    }

    [Serializable]
    // this class represents the item in the game
    public class Item : AbstractItem
    {
        private const string BASE = "A witness remembers that the @p1 was @p2!";
        private int code;
        private string barcode;
        private string itemName;
        private double price;
        private string description;
        private string reparto;
        private string textureFileName;        
        private string imageFileName;

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
        /// <param name="AItem">abstract item related to the real one (contains propreties)</param>
        public Item(int c, string bc, string name, double p, string descr, string rep, string texture, string image, AbstractItem AItem) : base(AItem)
        {
            
            code = c;
            barcode = bc;
            itemName = name;
            price = p;
            description = descr;
            reparto = rep;
            textureFileName = texture;            
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

        public string BarCode
        {
            get { return barcode; }
        }

        public double Price
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

        public string Clue
        {
            get
            {
                string wvClue = BASE.Replace("@p1", ItemKind.ToString().Replace('_', ' '));
                string wvTemp;
                if (ItemType == E_ItemType.A || ItemType == E_ItemType.C || ItemType == E_ItemType.D)
                    wvTemp = this.ItemProperties[0].Property.ToString();
                else
                    wvTemp = this.ItemProperties[1].Property.ToString();
                wvClue = wvClue.Replace("@p2", wvTemp);

                return wvClue;
            }
        }
        #endregion

        #region Methods
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
        
        #endregion
    }
}
