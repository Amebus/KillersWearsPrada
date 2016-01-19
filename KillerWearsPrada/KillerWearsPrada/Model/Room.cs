using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Model
{
    [Serializable]
    public class Room : ISerializable
    {
        
        public Room(string Name)
        {
            this.Name = Name;
            this.Items = new List<Item>();
        }

        public Room(List<Item> Items, string Name)
        {
            this.Name = Name;

            this.Items = Items;
            // ogni stanza contnene anche le clues che verranno mostate nelle finestre degli item
            // più una Clue che sarà risolutiva per un'altra stanza
            
        } 
        
        public string Name
        {
            get;
            private set;
        }
        
        public List<Item> Items
        {
            get;
            private set;
        }

        /// <summary>
        /// Represent the number on Item contained in the player's inventory
        /// </summary>
        public int NumberOfItemInInventory
        {
            get
            {
                int count = 0;
                foreach(Item i in this.Items)
                {
                    if (i.IsInInventory)
                        count++;
                }

                return count;
            }
        }

        public Item GetItem (string BarCode)
        {
            foreach(Item it in this.Items)
            {
                if (it.BarCode == BarCode)
                    return it;
            }

            return null;
        }

        public Item GetItem(int Code)
        {
            foreach (Item it in Items)
            {
                if (it.Code == Code)
                    return it;
            }

            return null;
        }

    }
}
