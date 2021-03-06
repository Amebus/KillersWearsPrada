﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Helpers
{
    
    
    public class SerializerHelper
    {

        public static void Serialize(string Path, Model.ISerializable Obj)
        {
            BinaryFormatter wvSerializer = new BinaryFormatter();
            Stream wvStream = new FileStream(Path, FileMode.Create, FileAccess.Write);
            wvSerializer.Serialize(wvStream, Obj);
            wvStream.Close();
        }

        public static Model.Game Deserialize(string Path)
        {
            Model.Game wvSerializedGame;
            BinaryFormatter wvSerializer = new BinaryFormatter();
            Stream wvStream = new FileStream(Path, FileMode.Open, FileAccess.Read);
            wvSerializedGame = (Model.Game)wvSerializer.Deserialize(wvStream);
            wvStream.Close();
            return wvSerializedGame;
        }
    }
}
