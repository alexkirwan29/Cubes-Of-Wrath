using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Cow
{
    public class ShortcutKeys : MonoBehaviour
    {
        public bool log = true;
        public enum Modifiers { Control, Alt, Shift };
        [System.Serializable]
        public class KeyShortcut
        {
            public string shortcutName;
            public Modifiers modifierKey;
            public KeyCode key;
            int total;
            public UnityEvent onDown;
            public UnityEvent onUp;
        }

        public KeyShortcut[] keyboardShortucts;
        void Update()
        {
            // Check if any key is being held down.
            if(Input.anyKey)
            {
                // Loop through each keyboard shortcut.
                foreach(KeyShortcut kshort in keyboardShortucts)
                {
                    // Check if the key has been pushed down then check if the only modifier key being held down is the one we want.
                    if(Input.GetKeyDown(kshort.key) && GetModifier(kshort.modifierKey))
                    {
                        // If we have been told to log then log.
                        if(log)
                            Debug.Log(string.Format("keyboard shortcut <i>{0}</i> <b>onDown</b> was trigged",kshort.shortcutName));

                        // Call the onDown UnityEvent
                        kshort.onDown.Invoke();
                    }
                    // The same as above but this time for the Key Up event
                    else if (Input.GetKeyUp(kshort.key) && GetModifier(kshort.modifierKey))
                    {
                        if(log)
                            Debug.Log(string.Format("keyboard shortcut <i>{0}</i> <b>onUp</b> was trigged", kshort.shortcutName));

                        kshort.onUp.Invoke();
                    }
                }
            }
        }

        // This checks if the only key being held is the one we want using the Modifier helper.
        public bool GetModifier(Modifiers key)
        {
            switch (key)
            {
                case Modifiers.Control: return Modifier(Modifiers.Control) && !Modifier(Modifiers.Alt) && !Modifier(Modifiers.Shift);
                case Modifiers.Alt: return !Modifier(Modifiers.Control) && Modifier(Modifiers.Alt) && !Modifier(Modifiers.Shift);
                default: return !Modifier(Modifiers.Control) && !Modifier(Modifiers.Alt) && Modifier(Modifiers.Shift);
            }
        }
        // This helper converts the Modifiers enum to a KeyCode then checks if the key is held down.
        bool Modifier(Modifiers key)
        {
            KeyCode LeftKeyCode;
            KeyCode RightKeyCode;
            switch (key)
            {
                case Modifiers.Control: LeftKeyCode = KeyCode.LeftControl; RightKeyCode = KeyCode.RightControl; break;
                case Modifiers.Alt: LeftKeyCode = KeyCode.LeftAlt; RightKeyCode = KeyCode.RightAlt; break;
                default: LeftKeyCode = KeyCode.LeftShift; RightKeyCode = KeyCode.RightShift; break;
            }
            return Input.GetKey(LeftKeyCode) || Input.GetKey(RightKeyCode);
        }
    }
}