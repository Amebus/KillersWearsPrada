using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Model
{
    [Serializable]
    class Item : Serializable
    {
        private Clue attClue;

        public Item ()
        {
            attClue = new Clue();
        }

        public Clue Clue
        {
            get { return attClue; }
        }

    }
}
