using UnityEngine;
using System.Collections;

namespace LittleThief {

    public class CameraController : MonoBehaviour {

        public GameObject targetObject;
        Vector3 offset;

        void Start() {
            offset = transform.position - targetObject.transform.position;
        }

        void Update() {
            if(Input.GetMouseButton(1)) {
                float mouseInputX = Input.GetAxis("Mouse X");
                transform.RotateAround(targetObject.transform.position, Vector3.up, mouseInputX * Time.deltaTime * 200);
            }
        }

        void LateUpdate() {
            transform.position = targetObject.transform.position + offset;
            if(targetObject.transform.position - Camera.main.transform.position != Vector3.zero) {
                Vector3 headPosiotion = targetObject.transform.position;
                headPosiotion.y = targetObject.transform.position.y+0.8f;
                Camera.main.transform.rotation = Quaternion.LookRotation(headPosiotion - Camera.main.transform.position);
            }
        }

    }

}