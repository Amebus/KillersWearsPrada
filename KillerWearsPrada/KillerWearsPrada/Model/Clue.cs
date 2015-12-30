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

    enum Texture
    {
        FANTASIA,
        PUA,
        RIGHE,
        SCACCHI,
        TINTA_UNITA
    }

}
