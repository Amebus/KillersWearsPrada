using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Model
{

    
    public class ItemGraficalProperty
    {
        private E_PropertiesKind attPropertyKind;
        public object Property { get; private set; }

        public E_PropertiesKind PropertyKind
        {
            get { return attPropertyKind; }
        }

        public ItemGraficalProperty()
        {
            attPropertyKind = E_PropertiesKind._NULL;
            Property = null;
        }
        
        public void SetProperty (E_PropertiesKind PropertyKind, object ItemProperty)
        {
            attPropertyKind = PropertyKind;
            Property = ItemProperty;
        }

        internal bool EqualsTo(ItemGraficalProperty AI)
        {
            if (this.PropertyKind != AI.PropertyKind)
                return false;
            if (this.Property != AI.Property)
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
        COLOR,
        GRADIATION,
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
        GRAY,
        GREEN,        
        RED,
        PINK,        
        PURPLE,
        YELLOW,
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
        hat,
        t_shirt,
        trousers,
        _END
    }
    
}
