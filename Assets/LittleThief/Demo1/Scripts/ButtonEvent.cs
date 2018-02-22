using UnityEngine;
using System.Collections;

namespace LittleThief {

    public class ButtonEvent : MonoBehaviour {

        public void Reset() {
            GameObject[] target = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject t in target) {
                t.GetComponent<EnemyController>().Respawn();
            }

            target = GameObject.FindGameObjectsWithTag("Box");
            foreach(GameObject t in target) {
                t.GetComponent<BoxController>().Reset();
            }

            target = GameObject.FindGameObjectsWithTag("Item");
            foreach(GameObject t in target) {
                Destroy(t);
            }

            target = GameObject.FindGameObjectsWithTag("Key");
            foreach(GameObject t in target) {
                t.GetComponent<KeyController>().Reset();
            }

            target = GameObject.FindGameObjectsWithTag("Door");
            foreach(GameObject t in target) {
                t.GetComponent<Animator>().SetInteger("motionIndex", 0);
                t.GetComponent<BoxCollider>().enabled = true;
            }

            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<PlayerController>().Reset();
        }

    }

}
