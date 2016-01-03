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
        private String color;
        private bool gradiation;
        private bool shape;
        private String texture;
        private String itemKind;
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

    enum ItemKind // can be taken from DB table TipoCapo
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
