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
            public string pageVersion;
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

            [JsonProperty("description")]
            public string description;
            [JsonProperty("data url")]
            public string data;
        }

        [Header("Server Varibles")]
        public string CowsButtUrl = "http://203.143.87.76/cowsbutt/";
        public string getList = "get_list.php";
        public string getLevelDetails = "get_level.php";

        [Header("List Varibles")]
        public int pageSize = 100;
        public LevelBrowserListItem listItemPrefab;
        public enum OrderByModes { likes = 0, plays = 1, author = 2, date = 3, title = 4 }
        public OrderByModes orderBy = OrderByModes.date;

        [Header("UI Varibles")]
        public Button nextPage;
        public Button lastPage;
        public Button previousPage;
        public Button fistPage;
        [Space(5)]
        public Text pageLabel;
        public ScrollRect scrollRect;
        public LoadingPanel loading;
        [Space(5)]
        //Debug Varibles
        public Text versionText;
        public Text urlText;
        [Space(10)]
        public LevelBrowserDetails levelDetails;
        public RectTransform listContentContainer;

        private int maxPages = 0;
        private int page = 0;
        private int detailsIndex = 0;

        public PageData pageData;
        LevelBrowserListItem[] levels;

        void Start()
        {
            //Populate the list with empty elements
            levels = new LevelBrowserListItem[pageSize];
            for(int i = 0; i < pageSize;i++)
            {
                GameObject go = Instantiate(listItemPrefab.gameObject);
                go.transform.SetParent(listContentContainer);
                levels[i] = go.GetComponent<LevelBrowserListItem>();
                levels[i].Create(this,i);
            }
            GetPage(0);
        }

        public void NextPage()
        {
            GetPage(++page);
        }
        public void PreviousPage()
        {
            GetPage(--page);
        }

        public void LastPage()
        {
            GetPage(maxPages-1);
        }
        public void FirstPage()
        {
            GetPage(0);
        }
        
        public void ChangeOrderMode(OrderByModes orderBy)
        {
            if (this.orderBy == orderBy)
                return;

            this.orderBy = orderBy;
            GetPage(page);
        }
        public void ChangeOrderMode(int orderBy)
        {
            if (this.orderBy == (OrderByModes)orderBy)
                return;

            this.orderBy = (OrderByModes)orderBy;
            GetPage(page);
        }

        public void DownloadPage_Complete()
        {
            versionText.text = string.Format("Server Version: '<i>{0}</i>'", pageData.pageVersion);
            maxPages = pageData.maxPages;
            pageLabel.text = string.Format("Page {0} of {1}", 1 + page, maxPages);

            nextPage.interactable = page < maxPages - 1;
            lastPage.interactable = page < maxPages - 1;

            previousPage.interactable = page > 0;
            fistPage.interactable = page > 0;


            for (int i = 0; i < levels.Length; i++)
            {
                if (i < pageData.elements.Length)
                    levels[i].SetContent(pageData.elements[i]);
                else
                    levels[i].SetContent(null);
            }
            //Force the canvas to update then tell the scroll rect to snap to the top
            //Canvas.ForceUpdateCanvases();
            //scrollRect.verticalNormalizedPosition = 1;
        }

        public void GetPage(int pageIndex)
        {
            detailsIndex = -1;
            levelDetails.Close();
            /*foreach (LevelBrowserListItem level in levels)
                level.SetContent(null);*/

            page = Mathf.Clamp(pageIndex, 0, maxPages);

            StopAllCoroutines();
            StartCoroutine(DownloadPage(page,orderBy));
        }

        public void ShowLevelDetails(int index)
        {
            StartCoroutine(GetLevelDetails(index));
        }

        IEnumerator DownloadPage(int page,OrderByModes orderBy)
        {
            loading.SetStatus(string.Format("Loading Page {0}", page + 1), "", true);
            WWW www = new WWW(string.Format("{0}{1}?page={2}&size={3}&order={4}", CowsButtUrl, getList, page, pageSize,orderBy.ToString()));
            urlText.text = www.url;
            yield return www;
            if (www.error != null)
            {
                Debug.LogError(www.error);
                loading.SetStatus("Loading Error", string.Format("Loading Page {0} Failed!{2}Error: <color=red>{1}</color>{2}<size=12>URL: {3}</size>",page+1,www.error,System.Environment.NewLine,www.url), false);
            }
            else
            {
                yield return 1;
                pageData = JsonConvert.DeserializeObject<PageData>(www.text);
                yield return 1;
                DownloadPage_Complete();
                loading.Hide();
            }
        }
        IEnumerator GetLevelDetails(int index)
        {
            if (index != detailsIndex)
            {
                detailsIndex = index;
                levelDetails.Open(detailsIndex + 1);
                WWW www = new WWW(string.Format("{0}{1}?id={2}", CowsButtUrl, getLevelDetails, pageData.elements[index].id));
                urlText.text = www.url;
                yield return www;
                if (www.error != null)
                {
                    Debug.LogError(www.error);
                }
                else
                {
                    yield return 1;
                    levelDetails.SetContent(JsonConvert.DeserializeObject<ListElement>(www.text));
                }
            }
        }
    }
}