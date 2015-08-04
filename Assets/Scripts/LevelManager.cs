using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

public class LevelManager : MonoBehaviour
{
    Dictionary<TileCoord,Tile> data;
    public UnityEvent OnChanged;
    public Texture2D texture;
    public LevelRenderer renderer;
    
    void Start()
    {
        if (texture != null)
        {
            MakeLevelFromTexture(texture);
            renderer.RenderWorld(data);
        }
    }
    public void MakeLevelFromTexture(Texture2D texture)
    {
        data = new Dictionary<TileCoord, Tile>();
        for (int y, x = 0; x < texture.width; x++)
        {
            for (y = 0; y < texture.height; y++)
            {
                if (texture.GetPixel(x, y) == Color.black)
                    CreateTile(new TileCoord(x, y), new Tile(0));
            }
        }
    }

    public void CreateTile(TileCoord pos, Tile tile)
    {
        data.Add(pos,tile);
    }
    public void RemoveTile(TileCoord pos)
    {
        data.Remove(pos);
    }
    public void SetTile(TileCoord pos, Tile tile)
    {
        if (data[pos] == null)
            CreateTile(pos, tile);
        else
            data[pos] = tile;
    }
    public Tile GetTile(TileCoord pos)
    {
        return data[pos];
    }
}
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
        if(obj is TileCoord)
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
}
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