using UnityEngine;
using System.Collections;

namespace LittleThief {

    public class KeyController : MonoBehaviour {

        bool isRotate = false;
        bool withPlayer = false;
        Vector3 goal;
        GameObject key;
        BoxCollider keyCollider;
        Vector3 keyPosion;
        Quaternion keyRotation;
        GameObject player;

        void Start() {
            key = this.gameObject;
            keyCollider = key.GetComponent<BoxCollider>();
            keyPosion = key.transform.position;
            keyRotation = key.transform.rotation;
            player = GameObject.FindWithTag("Player");
            goal = keyPosion;
        }

        void Update() {
            if(isRotate) {
                Rotation();
            }
            if(withPlayer) {
                goal = player.transform.position;
                goal.y += 0.5f;
                this.gameObject.transform.localScale = this.gameObject.transform.localScale * 0.98f;
                transform.position = Vector3.Lerp(transform.position, goal, Time.deltaTime * 12);
            } else {
                transform.position = Vector3.Lerp(transform.position, goal, Time.deltaTime * 4);
            }

        }

        void MoveTo(Vector3 v) {
            isRotate = true;
            goal = v;
            Invoke("MoveToPlayer", 0.3f);
        }

        void MoveToPlayer() {
            withPlayer = true;
            Invoke("Disable", 0.3f);
        }

        void Disable() {
            key.GetComponent<MeshRenderer>().enabled = false;
            key.transform.position = keyPosion;
            goal = keyPosion;
            key.transform.localScale = new Vector3(1, 1, 1);
            key.transform.rotation = keyRotation;
            isRotate = false;
            withPlayer = false;
        }

        void Rotation() {
            var v = new Vector3(0, 100, 0);
            this.transform.Rotate(v * Time.deltaTime);
        }

        public void Reset() {
            keyCollider.enabled = true;
            key.GetComponent<MeshRenderer>().enabled = true;
        }

    }

}
