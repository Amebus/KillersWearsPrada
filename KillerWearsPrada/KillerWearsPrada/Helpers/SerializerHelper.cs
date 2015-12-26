using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Helpers
{
    
    
    class SerializerHelper
    {

        public static void Serialize(String Path, Model.Serializable Obj)
        {
            BinaryFormatter wvSerializer = new BinaryFormatter();
            Stream wvStream = new FileStream(Path, FileMode.Create, FileAccess.Write);
            wvSerializer.Serialize(wvStream, Obj);
            wvStream.Close();
        }

        public static Model.Serializable Deserialize(String Path)
        {
            Model.Serializable wvSerializedObject;
            BinaryFormatter wvSerializer = new BinaryFormatter();
            Stream wvStream = new FileStream(Path, FileMode.Open, FileAccess.Read);
            wvSerializedObject = (Model.Serializable)wvSerializer.Deserialize(wvStream);
            wvStream.Close();
            return wvSerializedObject;
        }
    }
}
