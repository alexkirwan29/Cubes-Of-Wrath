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

        #region Static Defaults
        static TileCoord m_zero = new TileCoord();
        public static TileCoord Zero { get { return m_zero; } }
        static TileCoord m_up = new TileCoord(0, 1);
        public static TileCoord Up { get { return m_up; } }
        static TileCoord m_down = new TileCoord(0, -1);
        public static TileCoord Down { get { return m_down; } }
        static TileCoord m_left = new TileCoord(-1, 0);
        public static TileCoord Left { get { return m_left; } }
        static TileCoord m_right = new TileCoord(1, 0);
        public static TileCoord Right { get { return m_right; } }
        #endregion

        #region Overrides
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
        #endregion

        #region Converters
        public static TileCoord FromVector3(Vector3 value)
        {
            return new TileCoord(Mathf.FloorToInt(value.x), Mathf.FloorToInt(value.z));
        }
        public static TileCoord FromVector2(Vector2 value)
        {
            return new TileCoord(Mathf.FloorToInt(value.x), Mathf.FloorToInt(value.y));
        }

        public Vector3 ToVector3(float y)
        {
            return new Vector3(x, y, this.y);
        }

        public static implicit operator Vector3(TileCoord v)
        {
            return new Vector3(v.x, 0, v.y);
        }
        public static implicit operator TileCoord(Vector3 v)
        {
            return new TileCoord(Mathf.FloorToInt(v.x),Mathf.FloorToInt(v.z));
        }
        #endregion

        #region Operators
        public static bool operator == (TileCoord a, TileCoord b)
        {
            return a.x == b.x && a.y == b.y;
        }
        public static bool operator != (TileCoord a, TileCoord b)
        {
            return !(a == b);
        }
        #endregion
    }
}