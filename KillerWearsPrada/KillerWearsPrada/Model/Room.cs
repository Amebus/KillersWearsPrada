using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Model
{
    public enum E_RoomsName
    {
        _NULL,
        START_ROOM,
        LIVINGROOM,
        KITCHEN,
        BEDROOM,
        _END
    }

    [Serializable]
    public class Room : ISerializable
    {
        
        public Room(E_RoomsName Name)
        {
            this.Name = Name;
            this.Items = new List<Item>();
        }

        public Room(List<Item> Items, E_RoomsName Name)
        {
            this.Name = Name;

            this.Items = Items;
            // ogni stanza contnene anche le clues che verranno mostate nelle finestre degli item
            // più una Clue che sarà risolutiva per un'altra stanza
            
        } 
        
        public E_RoomsName Name
        {
            get;
            private set;
        }
        
        public List<Item> Items
        {
            get;
            private set;
        }

        public string LastClue
        {

            get
            {
                string wvClue = "Thanks to your efforts, a witness remembers that the @p1 was @p2!";
                Item wvItem = Items[0];

                wvClue = wvClue.Replace("@p1", wvItem.ItemKind.ToString());
                wvClue = wvClue.Replace("@p2", wvItem.ItemProperties[wvItem.PropertiesCount - 1].Property.ToString());

                return wvClue;
            }


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
