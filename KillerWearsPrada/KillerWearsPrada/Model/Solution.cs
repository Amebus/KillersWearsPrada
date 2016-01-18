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

        private List<AbstractItem> attSolutionItems;

        public Solution ()
        {
            attSolutionItems = new List<AbstractItem>();
        }

        public int Count
        {
            get { return attSolutionItems.Count; }
        }

        public void AddItem (AbstractItem Item)
        {
            attSolutionItems.Add(Item);
        }

        public bool CheckInSolution(AbstractItem Item)
        {
            foreach(AbstractItem ai in attSolutionItems)
            {

            }


        }

    }
}
