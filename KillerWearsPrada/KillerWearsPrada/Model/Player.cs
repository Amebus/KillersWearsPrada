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
        private string attName;
        private string attID;

        public Player (string ID, string Name)
        {
            attID = ID;
            attName = Name;
        }

        public string Name
        {
            get { return attName; }
        }


        public string ID
        {
            get { return attID; }
        }

    }
}
