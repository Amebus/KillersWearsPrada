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
        ARANCIONE,
        BLU,
        GIALLO,
        ROSSO,
        VERDE,
        VIOLA
    }

    enum E_Gradiation
    {
        CHIARO,
        SCURO
    }

    enum E_Shape
    {
        CORTO,
        LUNGO
    }

    enum E_Texture //can be taken from DB table TipoTexture
    {
        FANTASIA,
        POIS,
        RIGHE,
        SCOZZESE,
        TINTA_UNITA
    }

    enum E_ItemKind // can be taken from DB table TipoCapo
    {
        CAPPELLO,
        CAMICIA,
        PANTALONI,
        MAGLIETTA,
        MAGLIONE,
        GONNA,
        CAPPELLINO
    }
    
}
