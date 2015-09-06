using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

using Cow.Editor.UI;

namespace Cow.Editor.UI
{
    public class EditorItemList : MonoBehaviour
    {
        public GameObject listPrefab;
        public LevelSet items;
        public PrefabIconner iconMaker;
        public EditorManager editor;

        void Start()
        {
            // Populate the list at start-up.
            PopulateList();
            // Notice how I did not put in true or false when I invoked it.
        }
        public void PopulateList(bool append = false)
        {
            // Clear the list if the append variable is False (False by default).
            if(!append)
                ClearList();

            // Add an eraser/empty item.
            AddListItem(null, "Nothing",new UnityAction(delegate { editor.ActiveTile = null; }));

            // Loop through each item in the item set and add it to the list. The
            // icon is created using the PrefabIconner (I should rename it later).
            for (int i = 0; i < items.items.Length; i++)
            {
                int j = i;
                AddListItem(
                    iconMaker.GetIconOf(items.items[i].prefab.transform),
                    items.items[i].name,
                    new UnityAction(delegate
                    {
                        editor.ActiveTile = new Tile(j);
                    }));
            }
        }
        public void ClearList()
        {
            // Loop through each child transform in our transform and delete them.
            foreach (Transform child in transform)
                Destroy(child.gameObject);
        }
        public void AddListItem(Texture2D icon, string title, UnityAction action)
        {
            // Spawn in our list prefab and set it's parent to us. So the layout
            // group component attached to this game object can layout the list.
            GameObject instantiatedPrefab = Instantiate(listPrefab);
            instantiatedPrefab.transform.SetParent(transform);

            // Next we go through and set the title icon and the action that gets
            // invoked when the button is pressed but we only add it if it's not
            // null. I don't think the null check is necessary... 'Just-in-Case' :3
            instantiatedPrefab.GetComponentInChildren<Text>().text = title;
            instantiatedPrefab.GetComponentInChildren<RawImage>().texture = icon;
            if(action != null)
                instantiatedPrefab.GetComponent<Button>().onClick.AddListener(action);
        }
    }
}