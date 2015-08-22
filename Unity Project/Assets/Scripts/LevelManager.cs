using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using Cow;

namespace Cow
{
    public class LevelManager : MonoBehaviour
    {
        Level level;
        Dictionary<TileCoord, Transform> spawnedTiles;
        public LevelSet levelSet;
        public Texture2D texture;
        public string fileName = "Test";
        public enum LevelLoadMode { blank, texture, fromFile, fromAsset };
        public LevelLoadMode loadMode;

        public UnityEvent OnChanged;
        public Level Level { get { return level; } }
        void Start()
        {
            // Set up our dictionaries that keep track of the map data
            // and spawned prefabs.
            level = new Level();
            spawnedTiles = new Dictionary<TileCoord, Transform>();

            if (loadMode == LevelLoadMode.texture && texture != null)
                MakeLevelFromTexture(texture);
            else if (loadMode == LevelLoadMode.fromFile && System.IO.File.Exists(string.Format("{0}.level", fileName)))
                LoadFromFile(fileName);
        }
        public void MakeLevelFromTexture(Texture2D texture)
        {
            ClearLevel();

            // This method works by looping through each pixel in the
            // texture. If the pixel is black then create a tile with
            // the prefab value of 0.
            for (int y, x = 0; x < texture.width; x++)
            {
                for (y = 0; y < texture.height; y++)
                {
                    if (texture.GetPixel(x, y) == Color.black)
                        CreateTile(new TileCoord(x, y), new Tile(0));
                }
            }
        }
        public void MakeLevelFromData(Level level)
        {
            ClearLevel();
            foreach (KeyValuePair<TileCoord, Tile> entry in level.data)
            {
                CreateTile(entry.Key, entry.Value);
            }
        }
        public void SaveToFile(string name)
        {
            LevelIO.SaveToFile(level, name);
        }
        public void LoadFromFile(string name)
        {
            ClearLevel();
            MakeLevelFromData(LevelIO.LoadFromFile(name));
        }
        public void ClearLevel()
        {
            foreach (Transform entry in spawnedTiles.Values)
                Destroy(entry.gameObject);

            spawnedTiles.Clear();
            level.data.Clear();
        }
        public void CreateTile(TileCoord pos, Tile tile)
        {
            // Make sure the prefab we want to spawn exists, if it doesn't
            // cry in the console and exit out of this method.
            if (!(tile.prefab >= 0 && tile.prefab < levelSet.items.Length))
            {
                Debug.LogWarning(string.Format("This tile index {0} is not a valid index: Didn't do anything.", tile.prefab));
                return;
            }

            // Check to see if a tile is already here if so, windge about
            // it and return out of this method.
            if (level.data.ContainsKey(pos))
                return;

            if (!spawnedTiles.ContainsKey(pos))
            {
                // Spawn in the prefab.
                Transform inst = (Transform)Instantiate(levelSet.items[tile.prefab].prefab.transform, new Vector3(0.5f + pos.x, 0, 0.5f + pos.y), Quaternion.identity);
                inst.SetParent(transform);
                spawnedTiles.Add(pos, inst);
            }

            level.data.Add(pos, tile);
        }
        public void RemoveTile(TileCoord pos)
        {
            // See if we have spawned a tile at this pos, if so remove it
            // to stop null refernce exceptions.
            if (spawnedTiles.ContainsKey(pos))
                Destroy(spawnedTiles[pos].gameObject);

            // Next remove the dictionay entries
            spawnedTiles.Remove(pos);
            level.data.Remove(pos);
        }
        public void SetTile(TileCoord pos, Tile tile)
        {
            // Do we already have a tile at this pos? If so then remove it.
            if (level.data.ContainsKey(pos))
                RemoveTile(pos);

            //Creat a tile, Duh!
            CreateTile(pos, tile);
        }
        public Tile GetTile(TileCoord pos)
        {
            return level.data[pos];
        }
    }
}