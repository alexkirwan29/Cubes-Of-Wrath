using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Cow.UI.Transitions
{
    public abstract class Transition : MonoBehaviour
    {
        public abstract void Enter(UnityAction finished = null, float duration = 1f);
        public abstract void Exit(UnityAction finished = null, float duration = 1f);
    }
}