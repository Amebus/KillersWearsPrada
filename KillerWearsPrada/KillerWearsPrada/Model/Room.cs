using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Model
{
    [Serializable]
    class Room : Serializable
    {
        private List<Item> attItems;
        
        public Room(List<Item> Items)
        {
            attItems = Items;
        } 

        public List<Item> Items
        {
            get { return attItems; }
        }

    }
}
