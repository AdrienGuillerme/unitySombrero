using UnityEngine;
using System.Collections;

namespace LittleThief {

    public class RotationController : MonoBehaviour {

        private bool downFlag0 = false;
        private Vector3 mousePoint0;
        private Vector3 mousePoint1;

        void Update() {
            if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
                mousePoint0 = Input.mousePosition;
                downFlag0 = true;
            }
            if(Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) {
                downFlag0 = false;
            }
            rotateBody();
        }

        private void rotateBody() {
            if(downFlag0) {
                mousePoint1 = Input.mousePosition;
                var v = new Vector3(0, mousePoint0.x - mousePoint1.x, 0);
                this.transform.Rotate(v * 50 * Time.deltaTime);
                mousePoint0 = mousePoint1;
            }
        }

    }

}