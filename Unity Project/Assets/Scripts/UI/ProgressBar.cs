using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Cow.UI
{
    public class ProgressBar : MonoBehaviour
    {
        public bool smooth = true;
        public float speed;
        float lastValue;
        float t;
        float value;
        float wantedValue;
        public float maxValue = 10;
        [SerializeField]
        Image maskImage;
        [SerializeField]
        Text percentageText;
        public float Value
        {
            get { return value; }
            set
            {
                if (smooth)
                {
                    wantedValue = Mathf.Clamp(value, 0, maxValue);
                    lastValue = this.value;
                    t = 0;
                }
                else
                {
                    maskImage.fillAmount = value / maxValue;
                    percentageText.text = string.Format("{0:##0}%", (value / maxValue) * 100);
                }
            }
        }
        void Update()
        {
            if (smooth)
            {
                if (t < 1f)
                {
                    t += Time.deltaTime * speed;
                    value = Mathf.Lerp(lastValue, wantedValue, t);
                    maskImage.fillAmount = value / maxValue;
                    percentageText.text = string.Format("{0:##0}%", (value / maxValue) * 100);
                }
            }
        }
    }
}