using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Newtonsoft.Json;

namespace Cow.UI
{
    [RequireComponent(typeof(Text))]
    public class MotdText : MonoBehaviour
    {
        public string motdJsonUrl = "http://tomp.id.au/dev/operation%20fat%20COW/motd.json";
        public string defaultMotd = "Awww Dangit! it's a four oh four!";
        public float ttlScale = 1f;

        Text motdLabel;
        MotdMsg[] motd;
        int currMotd;

        class MotdMsg
        {
            public int ttl;
            [JsonProperty("motd")]
            public string text;
        }

        void Start()
        {
            motdLabel = GetComponent<Text>();
            motdLabel.text = defaultMotd;
            StartCoroutine(GetMotdData());
        }
        public IEnumerator GetMotdData()
        {
            WWW www = new WWW(motdJsonUrl);
            yield return www;

            if (www.error != null)
            {
                Debug.LogError(www.error);
            }
            else
            {
                motd = JsonConvert.DeserializeObject<MotdMsg[]>(www.text);
                NewMotd();
            }
                
        }

        public void NewMotd()
        {
            if (motd == null || motd.Length == 0)
                return;

            int rand = Random.Range(0, motd.Length);
            if(rand == currMotd)
            {
                rand++;
                if (!(rand < motd.Length))
                    rand = 0;
            }
            motdLabel.text = motd[rand].text;
            Invoke("NewMotd",ttlScale * motd[rand].ttl);
        }
    }
}