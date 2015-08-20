using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine.UI;
using Newtonsoft.Json;

namespace UI
{
    public class JsonTests : MonoBehaviour
    {
        [System.Serializable]
        public class ListData
        {
            [JsonProperty("level list")]
            public ListElement[] TestObjects { get; set; }
            [JsonProperty("page count")]
            public int totalPages;
            [JsonProperty("item count")]
            public int totalItems;
        }
        [System.Serializable]
        public class ListElement
        {
            [JsonProperty("title")]
            public string title;
            [JsonProperty("description")]
            public string description;
            [JsonProperty("image url")]
            public string imageURL;
            [JsonProperty("author")]
            public string author;
            [JsonProperty("stars")]
            public int rating;
            [JsonProperty("play count")]
            public int playCount;
        }
        TestListItem[] listItems;
        public RectTransform targetParent;
        public GameObject listItemPrefab;
        public Text pageText;
        public Button nextPage;
        public Button lastPage;
        public string jsonFile;
        int page = 0;
        int maxPages = 0;
        // Use this for initialization
        void Start()
        {
            listItems = new TestListItem[15];
            for(int i = 0; i < listItems.Length; i ++)
            {
                GameObject go = Instantiate(listItemPrefab);
                go.transform.SetParent(targetParent);
                go.transform.localScale = Vector3.one;
                listItems[i] = go.GetComponent<TestListItem>();
                go.SetActive(false);
            }
            StartCoroutine(GetPage(page));
        }
        public void NextPage() { SetPage(++page); }
        public void LastPage() { SetPage(--page); }
        public void SetPage(int page)
        {
            if (page < 0)
                page = 0;
            this.page = page;
            StartCoroutine(GetPage(this.page));
        }
        IEnumerator GetPage(int page)
        {
            WWW www = new WWW(string.Format("{0}?page={1}",jsonFile,page));
            yield return www;
            if (www.error != null)
            {
                Debug.LogError(www.error);
            }
            else
            {
                ListData downloadedData = JsonConvert.DeserializeObject<ListData>(www.text);
                maxPages = downloadedData.totalPages;
                pageText.text = string.Format("Page {0} of {1}", page, maxPages);

                nextPage.interactable = page <= maxPages - 2;
                lastPage.interactable = page >= 1;

                for (int i = 0; i < listItems.Length; i++)
                {
                    if (i < downloadedData.TestObjects.Length)
                    {
                        listItems[i].gameObject.SetActive(true);
                        listItems[i].FillContents(downloadedData.TestObjects[i]);
                    }
                    else
                    {
                        listItems[i].gameObject.SetActive(false);
                    }
                }
            }
            
        }
    }
}