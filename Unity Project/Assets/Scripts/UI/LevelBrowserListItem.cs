using UnityEngine;
using System.Collections;

using Cow.UI;
using UnityEngine.UI;

namespace Cow.UI
{
    [RequireComponent(typeof(Button))]
    public class LevelBrowserListItem : MonoBehaviour
    {
        //public Text idLabel;
        public Text titleLabel;
        public Text authorLabel;
        public Text dateLabel;
        public Text playsLabel;
        public Image ratingBar;
        Button button;
        LevelBrowser list;
        public void Create(LevelBrowser list,int index)
        {
            this.list = list;
            button = GetComponent<Button>();
            button.onClick.AddListener(new UnityEngine.Events.UnityAction(delegate { list.ShowLevelDetails(index); }));
            SetContent(null);
        }
        public void SetContent(LevelBrowser.ListElement data)
        {
            if (data == null)                                      //Exit if told to not display anything
            {
                gameObject.SetActive(false);
                return;
            }
            gameObject.SetActive(true);
            //idLabel.text = data.id.ToString();
            titleLabel.text = data.title;
            authorLabel.text = data.author;
            dateLabel.text = data.date;
            playsLabel.text = data.plays.ToString();
            ratingBar.fillAmount = (float)data.likes / (data.likes + data.dislikes);
        }
    }
}