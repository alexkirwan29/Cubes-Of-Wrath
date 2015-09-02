using UnityEngine;
using System.Collections;
using Cow.UI;
using Cow.UI.Transitions;
using UnityEngine.Events;

namespace Cow.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MenuSequence : MonoBehaviour
    {
        [System.Serializable]
        public class Slide
        {
            public Transition transition;
            public float transitionDurationMull;
            public float slideDuration;
            public bool canSkip;

            public RectTransform slideTransform;
        }

        public Slide[] slides;
        int currSlide = 0;
        CanvasGroup canvasGroup;


        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();

            foreach (Slide slide in slides)
                slide.slideTransform.gameObject.SetActive(false);

            CloseSlide(-1);
        }

        public void NextSlide()
        {
            slides[currSlide].transition.Enter(new UnityAction(delegate { CloseSlide(currSlide); }), slides[currSlide].transitionDurationMull);
        }
        public void CloseSlide(int index)
        {
            if(index >= 0)
                slides[index].slideTransform.gameObject.SetActive(false);

            currSlide = index + 1;
            if (currSlide < slides.Length)
            {
                slides[currSlide].transition.Exit(null, slides[currSlide].transitionDurationMull);
                slides[currSlide].slideTransform.gameObject.SetActive(true);
                Invoke("NextSlide", slides[currSlide].slideDuration);
            }
            else
            {
                slides[slides.Length - 1].transition.Exit(null, slides[slides.Length - 1].transitionDurationMull);

                canvasGroup.blocksRaycasts = false;
            }
        }
    }
}