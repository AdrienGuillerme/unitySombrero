using UnityEngine;
using System.Collections;

namespace LittleThief {

    public class EnemyAnimationEvent : MonoBehaviour {

        void Hit() {
            gameObject.transform.parent.SendMessage("HitPlayer");
        }

        void Death_End() {
            gameObject.transform.parent.SendMessage("Hide");
        }

    }

}