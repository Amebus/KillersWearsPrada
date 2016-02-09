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
            this.LastClueAlreadyShown = false;
        }

        public Room(E_RoomsName Name, List<Item> Items)
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

        private bool LastClueAlreadyShown { get; set; }

        public bool IsLastClueAlreadyShown
        {
            get
            {
                return LastClueAlreadyShown;
            }
        }

        public bool IsRoomCompleted
        {
            get
            {
                bool wvAAdded = false;
                bool wvBAdded = false;
                foreach (Item i in ItemsInInventory)
                {
                    if (i.Type == E_ItemType.A)
                        wvAAdded = true;
                    else if (i.Type == E_ItemType.B)
                        wvBAdded = true;
                }

                return (wvAAdded && wvBAdded);
            }
        }

        /// <summary>
        /// Return a list of strings representing all the Clues disclosed in the room
        /// </summary>
        public List<string> DisclosedItemsClues
        {
            get
            {
                List<string> wvCLues = new List<string>();
                foreach (Item i in Items)
                {
                    if (i.IsClueDisclosed)
                        if (!wvCLues.Contains(i.Clue))
                            wvCLues.Add(i.Clue);
                }
                return wvCLues;
            }
        }

        public List<Item> ItemsDisclosed
        {
            get
            {
                List<Item> wvItems = new List<Item>();
                foreach (Item i in Items)
                {
                    if (!i.IsClueDisclosed)
                        wvItems.Add(i);
                }
                return wvItems;
            }
        }

        public List<Item> ItemsNotInInventory
        {
            get
            {
                List<Item> wvItems = new List<Item>();
                foreach (Item i in Items)
                {
                    if (!i.IsInInventory)
                        wvItems.Add(i);
                }
                return wvItems;
            }
        }


        public List<Item> ItemsInInventory
        {
            get
            {
                List<Item> wvItems = new List<Item>();
                foreach (Item i in Items)
                {
                    if (i.IsInInventory)
                        wvItems.Add(i);
                }
                return wvItems;
            }
        }

        public List<Item> ItemsTrashed
        {
            get
            {
                List<Item> wvItems = new List<Item>();
                foreach (Item i in Items)
                {
                    if (i.IsTrashed)
                        wvItems.Add(i);
                }
                return wvItems;
            }
        }

        public List<Item> ItemsDressed
        {
            get
            {
                List<Item> wvItems = new List<Item>();
                foreach (Item i in Items)
                {
                    if (i.IsDressed)
                        wvItems.Add(i);
                }
                return wvItems;
            }
        }

        public void DressItem(int Code)
        {

            if (ItemsDressed.Count == 0)
                GetItem(Code).Dress();
            else
                throw new AlredyWearingAnItemException(GetItem(Code).ItemKind);
        }

        public void DressItem(string BarCode)
        {

            if (ItemsDressed.Count == 0)
                GetItem(BarCode).Dress();
            else
                throw new AlredyWearingAnItemException(GetItem(BarCode).ItemKind);
        }

        public string LastClue
        {

            get
            {
                LastClueAlreadyShown = true;
                string wvClue = "There is no LastClue for the " + Name.ToString().Replace('_', ' ');

                if (Name != E_RoomsName.START_ROOM)
                {
                    wvClue = "Thanks to your efforts, a witness remembers that the @p1 was @p2!";

                    Item wvItem = Items[0];

                    wvClue = wvClue.Replace("@p1", wvItem.ItemKind.ToString());
                    wvClue = wvClue.Replace("@p2", wvItem.LastProperty.Content.ToString());
                }


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
                foreach (Item i in this.Items)
                {
                    if (i.IsInInventory)
                        count++;
                }

                return count;
            }
        }

        public Item GetItem(string BarCode)
        {
            foreach (Item it in this.Items)
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

        public void EmptyTrash()
        {
            foreach (Item i in ItemsTrashed)
            {
                i.SetAsEliminated();
            }
        }


        public override string ToString()
        {
            return LastClue;
        }

    }

    public class AlredyWearingAnItemException : Exception
    {

        private const string BASE = "The killer is already wearing a ";

        public AlredyWearingAnItemException(E_ItemKind ItemKind)
        {
            this.ItemKind = ItemKind;
        }

        private E_ItemKind ItemKind { get; set; }

        public override string Message
        {
            get
            {
                return BASE + ItemKind.ToString().Replace('_', '-');
            }
        }
    }
}
