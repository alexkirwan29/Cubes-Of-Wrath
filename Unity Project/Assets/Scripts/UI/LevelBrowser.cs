using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Newtonsoft.Json;

namespace Cow.UI
{
    public class LevelBrowser : MonoBehaviour
    {
        public class PageData
        {
            [JsonProperty("version")]
            public int pageVersion;
            [JsonProperty("page count")]
            public int maxPages;
            [JsonProperty("total levels")]
            public int levels;
            [JsonProperty("time created")]
            public string pageDate;
            [JsonProperty("level list")]
            public ListElement[] elements;
        }
        public class ListElement
        {
            [JsonProperty("id")]
            public int id = 0;
            [JsonProperty("date")]
            public string date;
            [JsonProperty("title")]
            public string title;
            [JsonProperty("author")]
            public string author;
            [JsonProperty("plays")]
            public int plays;
            [JsonProperty("likes")]
            public int likes;
            [JsonProperty("dislikes")]
            public int dislikes;
        }

        public string pageUrl = "http://madcat/dev/cow%20back/get_list.php";
        public int pageSize = 50;

        private int maxPages = 0;
        private int page = 0;

        levelBrowserListItem[] listElements;
        public RectTransform listContentContainer;
        public levelBrowserListItem listItemPrefab;

        void Start()
        {
            //Populate the list with empty elements
            listElements = new levelBrowserListItem[pageSize];
            for(int i = 0; i < pageSize;i++)
            {
                GameObject go = Instantiate(listItemPrefab.gameObject);
                go.transform.SetParent(listContentContainer);
                listElements[i] = go.GetComponent<levelBrowserListItem>();
                listElements[i].SetContent(null);
            }
            GetPage(0);
        }
        IEnumerator DownloadPage(int page)
        {
            WWW www = new WWW(string.Format("{0}?page={1}",pageUrl,page));
            yield return www;
            if(www.error != null)
            {
                Debug.LogError(www.error);
            }
            else
            {
                PageData downloadedPage = JsonConvert.DeserializeObject<PageData>(www.text);
                maxPages = downloadedPage.maxPages;
                
                for(int i = 0; i < listElements.Length; i++)
                {
                    if (i < downloadedPage.elements.Length)
                        listElements[i].SetContent(downloadedPage.elements[i]);
                    else
                        listElements[i].SetContent(null);
                }
            }
        }

        public void GetPage(int page)
        {
            page = Mathf.Clamp(page, 0, maxPages);
            StopAllCoroutines();
            StartCoroutine(DownloadPage(page));
        }
        public void NextPage()
        {
            page++;
            GetPage(page);
        }
        public void LastPage()
        {
            page--;
            GetPage(page);
        }
    }
}