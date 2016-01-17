using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Model
{
    [Serializable]
    public class Solution : ISerializable
    {

        private List<Pairer> attPairers;

        public Solution ()
        {
            attPairers = new List<Pairer>();
        }

        public int Count
        {
            get { return attPairers.Count; }
        }

        public List<string> CorrectBarCodes
        {
            get
            {
                List<string> wvList = new List<string>();
                foreach(Pairer p in attPairers)
                {
                    wvList.Add(p.Item.BarCode);
                }
                return wvList;
            }
        }

        public Clue GetClueByBarcode(string BarCode)
        {
            foreach(Pairer p in attPairers)
            {
                if (p.Item.BarCode == BarCode)
                    return p.Clue;
            }
            return null;
        }

        /// <summary>
        /// Check if an ItemKind is already used.
        /// Return true if so, false otherwise.
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public bool CheckItemKind (E_ItemKind Value)
        {
            foreach(Pairer p in attPairers)
            {
                if (p.Item.ItemKind.ToLower() == Value.ToString().ToLower())
                    return true;
            }

            return false;
        }

        //public bool ContainsClue

        public void AddItem (Clue Clue, Item Value)
        {
            attPairers.Add(new Pairer(Clue, Value));
        }

        class Pairer
        {

            Item attItem;
            Clue attClue;

            public Pairer(Clue Clue, Item Item)
            {
                attItem = Item;
                attClue = Clue;
                attItem.SetClueText(Clue.ToString());
            }

            public Item Item
            {
                get { return attItem; }
            }
            
            public Clue Clue
            {
                get { return attClue; }
            }
        }

    }
}
