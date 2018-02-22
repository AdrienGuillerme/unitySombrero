using UnityEngine;
using System.Collections;

namespace LittleThief {

    public class ItemController : MonoBehaviour {

        bool isRotate = false;
        Vector3 goal;

        void Start() {
            goal = transform.position;
        }

        void Update() {
            if(isRotate) {
                Rotation();
            }

            transform.position = Vector3.Lerp(transform.position, goal, Time.deltaTime * 4);
        }

        void MoveTo(Vector3 v) {
            isRotate = true;
            goal = v;
        }

        void Rotation() {
            var v = new Vector3(0, 100, 0);
            this.transform.Rotate(v * Time.deltaTime);
        }

    }

}
