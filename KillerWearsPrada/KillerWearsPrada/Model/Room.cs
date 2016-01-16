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

        private String attRoomName;
        
        public Room(List<Item> Items, List<Clue> Clues, String Name)
        {
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
