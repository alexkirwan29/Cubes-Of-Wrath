using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class LevelSet : ScriptableObject
{
    [Header("Level Set")]
    public string levelSetName = "Untitled Level Set";
    [Header("Prefabs For This Level Set")]
    public GameObject[] prefabs;
}