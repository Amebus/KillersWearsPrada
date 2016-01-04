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
}
