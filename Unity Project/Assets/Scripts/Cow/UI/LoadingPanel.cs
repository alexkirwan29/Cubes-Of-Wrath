using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Cow.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class LoadingPanel : MonoBehaviour
    {
        [SerializeField]
        Text title;
        [SerializeField]
        Text description;
        [SerializeField]
        Image spinner;
        CanvasGroup canvasGroup;
        public void SetStatus(string title, string status, bool spinner = false)
        {
            if (canvasGroup == null)
                canvasGroup = GetComponent<CanvasGroup>();

            this.spinner.gameObject.SetActive(spinner);

            this.title.text = title;
            description.text = status;

            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        public void Hide()
        {
            if (canvasGroup == null)
                canvasGroup = GetComponent<CanvasGroup>();

            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}