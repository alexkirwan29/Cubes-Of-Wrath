using UnityEngine;
using System.Collections.Generic;

public class LevelRenderer : MonoBehaviour
{
    public GameObject[] prefabs;
    Dictionary<TileCoord, Transform> spawnedTransforms;

    public void RenderWorld(Dictionary<TileCoord,Tile> data)
    {
        foreach (KeyValuePair<TileCoord, Tile> tile in data)
        {
            SpawnObject(tile);
        }
    }
    public void SpawnObject(KeyValuePair<TileCoord, Tile> tile)
    {
        int prefabIndex = tile.Value.prefab;
        if (prefabIndex >= 0 && prefabIndex < prefabs.Length)
        {
            Transform temp = (Transform)Instantiate(prefabs[prefabIndex].transform, new Vector3(tile.Key.x, 0, tile.Key.y), prefabs[prefabIndex].transform.rotation);
            temp.parent = transform;
        }
    }
}