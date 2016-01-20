using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Model
{
    [Serializable]
    class Player : ISerializable
    {

        public Player (string ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }

        public string Name
        {
            get;
            private set;
        }


        public string ID
        {
            get;
            private set;
        }

    }
}
