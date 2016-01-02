using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Model
{
    [Serializable]
    class Clue : ISerializable
    {

    }

    //da ricontrollare
    enum Color
    {
        ARANCIONE,
        BLU,
        GIALLO,
        ROSSO,
        VERDE,
        VIOLA
    }

    enum Gradiation
    {
        CHIARO,
        SCURO
    }

    enum Shape
    {
        CORTO,
        LUNGO
    }

    enum Texture //can be taken from DB table TipoTexture
    {
        FANTASIA,
        POIS,
        RIGHE,
        SCOZZESE,
        TINTA_UNITA
    }

}
