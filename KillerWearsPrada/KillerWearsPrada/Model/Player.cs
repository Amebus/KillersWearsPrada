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
        private String attName;
        private String attID;

        public Player (String ID, String Name)
        {
            attID = ID;
            attName = Name;
        }

        private String Name
        {
            get { return attName; }
        }


        public String ID
        {
            get { return attID; }
        }

    }
}
