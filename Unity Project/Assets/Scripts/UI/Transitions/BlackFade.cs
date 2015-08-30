using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Cow.UI.Transitions
{
    [RequireComponent(typeof(Animator))]
    public class BlackFade : Transition
    {
        Animator anim;
        UnityAction finAction;
        public void AnimFinished()
        {
            if (finAction != null)
                finAction.Invoke();
        }
        public override void Enter(UnityAction finished = null, float duration = 1f)
        {
            finAction = finished;

            if (anim == null)
                anim = GetComponent<Animator>();

            anim.SetFloat("Duration", duration);
            anim.SetTrigger("Enter");
        }

        public override void Exit(UnityAction finished = null, float duration = 1f)
        {
            finAction = finished;

            if (anim == null)
                anim = GetComponent<Animator>();

            anim.SetFloat("Duration", duration);
            anim.SetTrigger("Exit");
        }
    }
}