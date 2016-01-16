using System;
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
        ARANCIONE,
        BLU,
        GIALLO,
        ROSSO,
        VERDE,
        VIOLA
    }

    public enum E_Gradiation
    {
        NULL,
        CHIARO,
        SCURO
    }

    public enum E_Shape
    {
        NULL,
        CORTO,
        LUNGO
    }

    public enum E_Texture //can be taken from DB table TipoTexture
    {
        NULL,
        FANTASIA,
        POIS,
        RIGHE,
        SCOZZESE,
        TINTA_UNITA
    }

    public enum E_ItemKind // can be taken from DB table TipoCapo
    {
        NULL,
        Cappello,
        Cappelino,
        Camicia,
        Pantaloni,
        Maglietta,
        Maglione,
        Gonna
        
    }
    
}
