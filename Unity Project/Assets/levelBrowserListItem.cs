using UnityEngine;
using System.Collections;

using Cow.UI;
using UnityEngine.UI;

public class levelBrowserListItem : MonoBehaviour
{
    public Text idLabel;
    public Text titleLabel;
    public Text authorLabel;
    public Text dateLabel;
    public Text playsLabel;
    public Image ratingBar;

    public void SetContent(LevelBrowser.ListElement data)
    {
        if(data == null)                                      //Exit if told to no display anything
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
        idLabel.text = data.id.ToString();
        titleLabel.text = data.title;
        authorLabel.text = data.author;
        dateLabel.text = data.date;
        playsLabel.text = data.plays.ToString();
        ratingBar.fillAmount = (float)data.likes / (data.likes + data.dislikes);
    }
}