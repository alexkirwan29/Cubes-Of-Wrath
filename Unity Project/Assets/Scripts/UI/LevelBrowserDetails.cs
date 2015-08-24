using UnityEngine;
using System.Collections;

using Cow.UI;
using UnityEngine.UI;

namespace Cow.UI
{
    [RequireComponent(typeof(Animator))]
    public class LevelBrowserDetails : MonoBehaviour
    {
        Animator anim;
        public Text descriptionLabel;

        //ALEX REMOVE THESE LINES
        public Text animDebugText;
        //WHEN THE ANIMATIONS ARE DONE

        public void Open(int index)
        {
            if (anim == null)
                anim = GetComponent<Animator>();

            transform.SetSiblingIndex(index);
            descriptionLabel.text = null;
            anim.SetTrigger("Open");
            foreach (Button button in GetComponentsInChildren<Button>())
                button.interactable = false;

            //ALEX REMOVE THESE LINES
            gameObject.SetActive(true);
            animDebugText.text = "Trigger: Open";
            //WHEN THE ANIMATIONS ARE DONE
        }
        public void Close()
        {
            if (anim == null)
                anim = GetComponent<Animator>();
            anim.SetTrigger("Close");

            //ALEX REMOVE THESE LINES
            gameObject.SetActive(false);
            animDebugText.text = "Trigger: Close";
            //WHEN THE ANIMATIONS ARE DONE
        }
        public void SetContent(LevelBrowser.ListElement data)
        {
            if (anim == null)
                anim = GetComponent<Animator>();

            foreach (Button button in GetComponentsInChildren<Button>())
                button.interactable = true;

            anim.SetTrigger("Loaded");
            descriptionLabel.text = data.description;

            //ALEX REMOVE THESE LINES
            animDebugText.text = "Trigger: Loaded";
            //WHEN THE ANIMATIONS ARE DONE
        }
    }
}