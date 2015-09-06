using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class PrefabIconner : MonoBehaviour
{
    [Range(0, 31)]
    public int iconLayer;
    public Camera snapshotCam;

    public Texture2D[] GetIconsFrom(LevelSet levelSet)
    {
        // This loops through each icon in the level and add the icon for that
        // item to an array of Texture2Ds.
        Texture2D[] icons = new Texture2D[levelSet.items.Length];
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i] = GetIconOf(levelSet.items[i].prefab.transform);
        }
        return icons;
    }
    public Texture2D[] GetIconsFrom(Transform[] transforms)
    {
        // This loops through each icon in the level and add the icon for that
        // transform to an array of Textures.
        Texture2D[] icons = new Texture2D[transforms.Length];
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i] = GetIconOf(transforms[i]);
        }
        return icons;
    }
    public Texture2D[] GetIconsFrom(GameObject[] gameObjects)
    {
        // This loops through each icon in the level and add the icon for that
        // transform to an array of Textures.
        Texture2D[] icons = new Texture2D[gameObjects.Length];
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i] = GetIconOf(gameObjects[i].transform);
        }
        return icons;
    }

    public Texture2D GetIconOf(Transform prefab)
    {
        // Spawn us a prefab to look at and set it's layer to the iconLayer so
        // other cameras can't see it.
        Transform tempPrefab = (Transform)Instantiate(prefab, transform.position, Quaternion.identity);
        tempPrefab.gameObject.layer = iconLayer;

        // Force the camera to render because this can be called many times a frame.
        snapshotCam.Render();

        // Set the active render target of the game to be ours.
        RenderTexture.active = snapshotCam.targetTexture;

        // Convert the render texture to a normal usable texture.
        Texture2D newIcon = new Texture2D(snapshotCam.targetTexture.width, snapshotCam.targetTexture.height, TextureFormat.ARGB32, false, true);
        newIcon.ReadPixels(new Rect(0, 0, newIcon.width, newIcon.height), 0, 0, false);
        newIcon.Apply();

        // Set the active render texture to null (this might no be needed).
        RenderTexture.active = null;

        // disable the renderer of the spawned prefab because just removing it is
        // not enough.
        tempPrefab.GetComponent<MeshRenderer>().enabled = false;

        // remove the spawned prefab.
        Destroy(tempPrefab.gameObject);

        // And finally return the new icon texture
        return newIcon;
    }
}