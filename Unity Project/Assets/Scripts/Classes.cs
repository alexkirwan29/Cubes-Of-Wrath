using UnityEngine;
using System.Collections.Generic;
using Cow;

namespace Cow
{
    [System.Serializable]
    public class Tile
    {
        public int prefab;
        public Tile()
        {
            prefab = 0;
        }
        public Tile(int prefab)
        {
            this.prefab = prefab;
        }
    }

    [System.Serializable]
    public class Level
    {
        public int version;
        public string name;
        public string description;
        public string author;
        public Dictionary<TileCoord, Tile> data;

        public Level()
        {
            version = 1;
            name = "Untiled Level";
            description = "Empty";
            author = "Nobody";

            data = new Dictionary<TileCoord, Tile>();
        }
        public Level(string name, string description, string author)
        {
            this.name = name;
            this.description = description;
            this.author = author;
            data = new Dictionary<TileCoord, Tile>();
        }
    }

    [System.Serializable]
    public struct TileCoord : System.IEquatable<TileCoord>
    {
        public int x;
        public int y;

        public TileCoord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj is TileCoord)
                return base.Equals(obj);
            return false;
        }
        public bool Equals(TileCoord other)
        {
            return x == other.x && y == other.y;
        }
        public override string ToString()
        {
            return string.Format("x:{0} y:{1}", x, y);
        }
        public override int GetHashCode()
        {
            return x.GetHashCode() + y.GetHashCode();
        }

        public static TileCoord FromVector3(Vector3 value)
        {
            return new TileCoord(Mathf.FloorToInt(value.x), Mathf.FloorToInt(value.z));
        }
        public static TileCoord FromVector2(Vector2 value)
        {
            return new TileCoord(Mathf.FloorToInt(value.x), Mathf.FloorToInt(value.y));
        }
    }
}