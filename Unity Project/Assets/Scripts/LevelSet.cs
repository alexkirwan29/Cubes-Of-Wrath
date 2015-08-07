using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class LevelSet : ScriptableObject
{
    [System.Serializable]
    public class Item
    {
        public string name;
        public GameObject prefab;
        public bool halfOffset;
        public float random;
        public Item()
        {
            name = "Untitled";
            halfOffset = true;
            random = 0;
        }
    }
    [Header("Level Set")]
    public string levelSetName = "Untitled Level Set";
    [Header("Items For This Level Set")]
    public Item[] items;
}