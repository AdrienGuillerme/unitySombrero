using UnityEngine;
using System.Collections;

namespace LittleThief {

    public class BoxController : MonoBehaviour {

        public string itemName = "Item1";
        Animator animator;
        GameObject boxCharacter;
        GameObject boxTrigger;

        void Start() {
            boxCharacter = transform.Find("TreasureBox").gameObject;
            boxTrigger = transform.Find("BoxTrigger").gameObject;
            animator = boxCharacter.GetComponent<Animator>();
        }

        public void Reset() {
            boxTrigger.GetComponent<BoxCollider>().enabled = true;
            animator.SetInteger("motionIndex", 2);
        }

        void Open() {
            animator.SetInteger("motionIndex", 1);
        }

        public string GetItemName() {
            return (itemName);
        }

    }

}
