using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Cow;

public class LevelIO
{
    public static void SaveToFile(Level data, string fileName)
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Format("{0}.level", fileName), FileMode.Create, FileAccess.Write, FileShare.Write);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static byte[] GetBytes(Level data)
    {
        IFormatter formatter = new BinaryFormatter();
        MemoryStream stream = new MemoryStream();
        formatter.Serialize(stream, data);
        return stream.ToArray();
    }
    public static Level LoadFromFile (string fileName)
    {
        if (File.Exists(string.Format("{0}.level", fileName)))
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Format("{0}.level", fileName), FileMode.Open, FileAccess.Read, FileShare.Read);
            Level data = (Level)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogWarning(string.Format("Did not Find {0}.level", fileName));
            return null;
        }
    }
}