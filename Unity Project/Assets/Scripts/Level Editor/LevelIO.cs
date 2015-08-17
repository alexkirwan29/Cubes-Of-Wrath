using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LevelIO
{
    public static void SaveToFile(Dictionary<TileCoord,Tile> data, string fileName)
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Format("{0}.level", fileName), FileMode.Create, FileAccess.Write, FileShare.Write);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static Dictionary<TileCoord, Tile> LoadFromFile(string fileName)
    {
        if (File.Exists(string.Format("{0}.level", fileName)))
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Format("{0}.level", fileName), FileMode.Open, FileAccess.Read, FileShare.Read);
            Dictionary<TileCoord, Tile> data = (Dictionary<TileCoord, Tile>)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogWarning(string.Format("Did not Find {0}.level", fileName));
            return null;
        }
    }
    /*public static Dictionary<TileCoord,Tile> LoadFromAsset(TextAsset asset)
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Format("{0}.bytes", fileName), FileMode.Open, FileAccess.Read, FileShare.Read);
        Dictionary<TileCoord, Tile> data = (Dictionary<TileCoord, Tile>)formatter.Deserialize(stream);
        stream.Close();
        return data;
    }*/
}