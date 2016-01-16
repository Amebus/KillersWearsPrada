using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Model
{
    [Serializable]
    public class Room : ISerializable
    {
        private List<Item> attItems;

        private  List<Clue> attClues;

        private Int32 attNumberOfItemLooked;
        private String attRoomName;
        
        public Room(List<Item> Items, List<Clue> Clues, String Name)
        {
            attNumberOfItemLooked = 0;

            attItems = Items;
            // ogni stanza contnene anche le clues che verranno mostate nelle finestre degli item
            // più una Clue che sarà risolutiva per un'altra stanza
            attClues = Clues;
        } 
        
        public String Name
        {
            get { return Name; }
        }

        public List<Item> Items
        {
            get { return attItems; }
        }

        public Int32 NumberOfItemInInventory
        {
            get
            {
                int count = 0;
                foreach(Item i in attItems)
                {
                    if (i.IsInInventory)
                        count++;
                }

                return count;
            }
        }

        public void AddClue(Clue c)
        {
            attClues.Add(c);
        }
        
            
        public List<Clue> Clues 
        {
            get { return attClues; }
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
