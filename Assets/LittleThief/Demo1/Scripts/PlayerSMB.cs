using UnityEngine;
using System.Collections;

namespace LittleThief {

    public class PlayerSMB : StateMachineBehaviour {

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            animator.transform.parent.gameObject.SendMessage("AnimationEnter", stateInfo);
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            animator.transform.parent.gameObject.SendMessage("AnimationExit", stateInfo);
        }

    }

}
