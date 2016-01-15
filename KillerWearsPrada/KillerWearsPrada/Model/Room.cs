using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Model
{
    [Serializable]
    class Room : ISerializable
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

        public Item GetItemByBarCode (String BarCode)
        {
            foreach(Item it in attItems)
            {
                if (it.BarCode == BarCode)
                    return it;
            }

            return null;
        }

        public Item GetItemByCode(Int32 Code)
        {
            foreach (Item it in attItems)
            {
                if (it.Code == Code)
                    return it;
            }

            return null;
        }

    }
}
