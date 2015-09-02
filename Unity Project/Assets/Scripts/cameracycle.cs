using UnityEngine;
using System.Collections;

using Cow.UI.Transitions;

public class cameracycle : MonoBehaviour
{
    public Transition transition;
    public KeyCode key;

    void Update()
    {
        if (Input.GetKeyDown(key))
            transition.Enter(new UnityEngine.Events.UnityAction(delegate { GameCamera.instance.SetTarget(transform); transition.Exit(null,4f); }),4f);
    }
}

