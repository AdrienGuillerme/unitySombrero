using UnityEngine;
using System.Collections;

namespace LittleThief {

    public class PlayerSMB2 : StateMachineBehaviour {

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            animator.transform.parent.gameObject.SendMessage("AnimationEnter", stateInfo);
        }

    }

}
