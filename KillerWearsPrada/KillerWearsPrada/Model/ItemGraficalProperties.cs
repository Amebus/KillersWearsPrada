using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Model
{

    //da ricontrollare
    enum E_Color
    {
        NULL,
        ARANCIONE,
        BLU,
        GIALLO,
        ROSSO,
        VERDE,
        VIOLA
    }

    enum E_Gradiation
    {
        NULL,
        CHIARO,
        SCURO
    }

    enum E_Shape
    {
        NULL,
        CORTO,
        LUNGO
    }

    enum E_Texture //can be taken from DB table TipoTexture
    {
        NULL,
        FANTASIA,
        POIS,
        RIGHE,
        SCOZZESE,
        TINTA_UNITA
    }

    enum E_ItemKind // can be taken from DB table TipoCapo
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
