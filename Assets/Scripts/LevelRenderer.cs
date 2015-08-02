using UnityEngine;
using System.Collections;

[System.Serializable]
public class SpawnableObject
{
    public Transform prefab;
    public Color32 colour;
}
public class LevelRenderer : MonoBehaviour
{
    public float prefabScale = 1;
    public SpawnableObject[] prefabs;
    public Texture2D mapTexture;
    void Start()
    {
        GenerateWorld(mapTexture);
    }
    public void GenerateWorld(Texture2D mapTexture)
    {
        int row = 0;
        int xCount = 0;
        foreach (Color32 colour in mapTexture.GetPixels32())
        {
            SpawnObject(colour,xCount,row);

            xCount++;
            if(xCount == mapTexture.width)
            {
                row++;
                xCount = 0;
            }
        }
    }
    public void SpawnObject(Color32 colour, int x, int y)
    {
        foreach (SpawnableObject prefab in prefabs)
        {
            if(colour.a != 0 && prefab.colour.r == colour.r && prefab.colour.g == colour.g && prefab.colour.b == colour.b)
            {
                Transform temp = (Transform)Instantiate(prefab.prefab, new Vector3(x,0,y), prefab.prefab.rotation);
                temp.parent = transform;
            }
        }
    }
}