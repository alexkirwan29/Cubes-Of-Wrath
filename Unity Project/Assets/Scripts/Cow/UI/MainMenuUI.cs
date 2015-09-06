using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Cow.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public void LoadLevel(int scene)
        {
            Application.LoadLevel(scene);
        }
        public void LoadLevel(string name)
        {
            Application.LoadLevel(name);
        }
    }
}