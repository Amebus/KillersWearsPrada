﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Model
{

    //da ricontrollare
    public enum E_Color
    {
        NULL,
        BLACK,
        BLUE,
        BROWN,
        GRAY,
        GREEN,        
        RED,
        PINK,        
        PURPLE,
        YELLOW
    }

    public enum E_Gradiation
    {
        NULL,
        LIGHT,
        DARK
    }

    public enum E_Shape
    {
        NULL,
        SHORT,
        LONG
    }

    public enum E_Texture //can be taken from DB table TipoTexture
    {
        NULL,
        FANTASY,
        POIS,
        STRIPES,
        SCOTTISH,
        PLAINCOLOR
    }

    public enum E_ItemKind // can be taken from DB table TipoCapo
    {
        NULL,
        hat,
        shirt,
        t_shirt,
        trousers,
        skirt,
        cap,
        jumper
        
    }
    
}
