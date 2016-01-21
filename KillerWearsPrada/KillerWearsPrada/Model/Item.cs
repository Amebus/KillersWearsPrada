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

        /// <summary>
        /// Return the list of <see cref="ItemGraficalProperty"/> associated to the <see cref="AbstractItem"/>
        /// </summary>
        public List<ItemGraficalProperty> ItemProperties
        {
            get { return attItemProperties; }
        }

        /// <summary>
        /// Return the numbers of <see cref="ItemGraficalProperty"/> associated to the <see cref="AbstractItem"/>
        /// </summary>
        public int PropertiesCount
        {
            get { return attItemProperties.Count; }
        }

        /// <summary>
        /// Add an <see cref="ItemGraficalProperty"/> to the <see cref="AbstractItem"/>
        /// </summary>
        /// <param name="Property"></param>
        public void AddProperty(ItemGraficalProperty Property)
        {
            attItemProperties.Add(Property);
        }

        /// <summary>
        /// Verify if the <see cref="AbstractItem"/> is equal to another specified <see cref="AbstractItem"/> by checking the equalities of their property
        /// </summary>
        /// <param name="AI">The specified <see cref="AbstractItem"/> to be compared</param>
        /// <returns>True if equals, false otherwise</returns>
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

        /// <summary>
        /// Return true if the <see cref="Item"/> contains a <see cref="ItemGraficalProperty"/> definition for the specified <see cref="E_PropertiesKind"/>
        /// </summary>
        /// <param name="PropertyKind">The <see cref="E_PropertiesKind"/> to check if contained</param>
        /// <returns>True if the <see cref="Item"/> contains the specified property False otherwise</returns>
        public bool CheckPropertyByKind(E_PropertiesKind PropertyKind)
        {
            foreach (ItemGraficalProperty igp in ItemProperties)
            {
                if (igp.PropertyKind == PropertyKind)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public E_PropertiesKind GetPropertyKind(int Index)
        {
            if (Index >= ItemProperties.Count)
                Index = ItemProperties.Count - 1;
            else if (Index < 0)
                Index = 0;

            return ItemProperties[Index].PropertyKind;
        }

        /// <summary>
        /// Return a <see cref="string"/> representing the value of the <see cref="ItemGraficalProperty"/> corresponding to the <see cref="E_PropertiesKind"/> specified.
        /// If the property is not found the <see cref="string.Empty"/> will be returned instead
        /// </summary>
        /// <param name="PropertyKind">The <see cref="E_PropertiesKind"/> object representing the kind of properties to be returned</param>
        /// <returns></returns>
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

        /// <summary>
        /// Return a <see cref="string"/> representing the value of the <see cref="ItemGraficalProperty"/> corresponding to the <see cref="E_PropertiesKind"/> specified.
        /// If the property is not found the <see cref="string.Empty"/> will be returned instead
        /// </summary>
        /// <param name="Index">The <see cref="Int"/> object representing the kind of properties to be returned</param>
        /// <returns></returns>
        public string GetProperty (int Index)
        {
            if (Index >= ItemProperties.Count)
                Index = ItemProperties.Count - 1;
            else if (Index < 0)
                Index = 0;

            ItemGraficalProperty wvProperty = ItemProperties[Index];

            return ConvertProperty(wvProperty);
        }

        /// <summary>
        /// Cenvert a property from an <see cref="object"/> to the <see cref="string"/> representing is content
        /// </summary>
        /// <param name="Property"></param>
        /// <returns></returns>
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
                    return string.Empty;
            }
        }

    }

    [Serializable]
    // this class represents the item in the game
    public class Item : AbstractItem
    {
        private const string BASE = "A witness remembers that the @p1 was @p2!";

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
            
            this.Code = c;
            this.BarCode = bc;
            this.ItemName = name;
            this.Price = p;
            this.Description = descr;
            this.Reparto = rep;
            this.TextureFilename = texture;            
            this.ImageFileName = image;            

            attTrashed = false;
            attInInventory = false;
            attDressed = false;
        }

        #region Properties
        public int Code
        {
            get;
            private set;
        }

        public string BarCode
        {
            get;
            private set;
        }

        public double Price
        {
            get;
            private set;
        }

        public string ItemName
        {
            get;
            private set;
        }

        public string Description
        {
            get;
            private set;
        }

        public string Reparto
        {
            get;
            private set;
        }

        public string TextureFilename
        {
            get;
            private set;
        }       

        public string ImageFileName
        {
            get;
            private set;
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

        public string ItemsImagePath
        {
            get { return Helpers.ResourcesHelper.ItemsImagesPath + ImageFileName; }
        }

        /// <summary>
        /// Return a <see cref="string"/> representing the clue of the item
        /// </summary>
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

        public E_Shape Shape
        {
            get
            {
                E_Shape wvShape = E_Shape._NULL;
                if (CheckPropertyByKind(E_PropertiesKind.SHAPE))
                    wvShape = (E_Shape)Enum.Parse(typeof(E_Shape), GetProperty(E_PropertiesKind.SHAPE));
                else
                {
                    Helpers.DBHelper wvDB = new Helpers.DBHelper();
                    bool wvBoolShape = wvDB.GetShape(this.Code);

                    if (wvBoolShape)
                        wvShape = E_Shape.LONG;
                    else
                        wvShape = E_Shape.SHORT;
                }

                return wvShape;
            }
        }

        public E_ItemType Type { get { return ItemType; } }

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

        public void SetAsInInventory()
        {
            attInInventory = true;
            attTrashed = false;
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
            return Clue;
        }

        internal void SetAsEliminated()
        {
            attInInventory = false;
            attDressed = false;
            attTrashed = false;
        }

        #endregion
    }
}
