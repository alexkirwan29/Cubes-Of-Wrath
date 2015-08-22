using UnityEngine;
using UnityEngine.UI;

namespace Cow.UI
{
    [RequireComponent(typeof(Text))]
    public class NumberFormatter : MonoBehaviour
    {
        string startingText;
        Text label;

        void Awake()
        {
            label = GetComponent<Text>();
            startingText = label.text;
            ChangeValue(0);
        }

        public void ChangeValue(float value)
        {
            label.text = string.Format(startingText, value);
        }
    }
}