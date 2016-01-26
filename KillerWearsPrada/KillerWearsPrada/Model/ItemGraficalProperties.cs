using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Model
{

    [Serializable]
    public class ItemGraficalProperty : ISerializable
    {
        public object Content { get; private set; }


        public E_PropertiesKind PropertyKind { get; private set; }
        

        public ItemGraficalProperty()
        {
            PropertyKind = E_PropertiesKind._NULL;
            Content = null;
        }
        
        public void SetContent (E_PropertiesKind PropertyKind, object ItemProperty)
        {
            this.PropertyKind = PropertyKind;
            Content = ItemProperty;
        }

        public void SetContent (ItemGraficalProperty IGP)
        {
            PropertyKind = IGP.PropertyKind;
            this.Content = IGP.Content;
        }

        internal bool EqualsTo(ItemGraficalProperty AI)
        {
            if (this.PropertyKind != AI.PropertyKind)
                return false;
            if (this.Content != AI.Content)
                return false;

            return true;

        }
    }

    /// <summary>
    /// Kind of properties addable to the items
    /// </summary>
    public enum E_PropertiesKind
    {
        _NULL,
        GRADIATION,
        COLOR,
        SHAPE,
        TEXTURE,
        _END
    }

    public enum E_Color
    {
        _NULL,
        BLACK,
        BLUE,
        BROWN,
        //GRAY,
        GREEN,        
        RED,
        //PINK,        
        PURPLE,
        YELLOW,
        WHITE,
        _END
    }

    public enum E_Gradiation
    {
        _NULL,
        LIGHT,
        DARK,
        _END
    }

    /// <summary>
    /// If associated to hats Short=Cap and Long=Hat
    /// </summary>
    public enum E_Shape
    {
        _NULL,
        SHORT,
        LONG,
        _END
    }

    public enum E_Texture //can be taken from DB table TipoTexture
    {
        _NULL,
        FLOWERS,
        POIS,
        STRIPES,
        SCOTTISH,
        PLAINCOLOR,
        _END
    }

    public enum E_ItemKind // can be taken from DB table TipoCapo
    {
        _NULL,
        TROUSERS,
        HAT,
        T_SHIRT,
        _END
    }
    
}
