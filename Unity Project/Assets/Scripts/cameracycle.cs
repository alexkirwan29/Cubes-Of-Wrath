using UnityEngine;
using System.Collections;

using Cow.UI.Transitions;

public class CameraCycle : MonoBehaviour
{
    public Transition transition;
    public KeyCode key;
    public bool onStart = false;

    void Start()
    {
        if (onStart)
        {
            if (GameCamera.instance.Target == null)
                GameCamera.instance.Target = transform;
            else
                Debug.LogWarning("The GameCamera has already had it's target set!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(key) && GameCamera.instance.Target != transform)
            transition.Enter(new UnityEngine.Events.UnityAction(delegate {
                GameCamera.instance.Target = transform;
                transition.Exit(null,4f); }
            ),4f);
    }
}

