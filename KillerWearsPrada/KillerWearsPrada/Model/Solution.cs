using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Model
{
    [Serializable]
    class Solution : ISerializable
    {

        List<E_ItemKind> attCloathes;

        public E_ItemKind LastItemKind
        {
            get { return attCloathes.Last(); }
        }

        public bool CheckItemKind (E_ItemKind Value)
        {
            return attCloathes.Contains(Value);
        }

        public void AddItemKind (E_ItemKind Value)
        {
            attCloathes.Add(Value);
        }


    }
}
