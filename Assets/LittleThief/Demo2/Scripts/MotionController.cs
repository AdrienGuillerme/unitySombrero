using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace LittleThief {

    public class MotionController : MonoBehaviour {

        public GameObject playerCharacter;
        public GameObject playerMesh;
        public Material[] faceMaterial;
        public Material[] bodyMaterial;

        Animator animator;
        GameObject weaponHolder_R;
        GameObject weaponHolder_L;
        Transform weaponHolder_parentR1; // hand
        Transform weaponHolder_parentL1;
        Transform weaponHolder_parentR2; // back
        Transform weaponHolder_parentL2;

        bool isHoldWeapon = false;

        void Start() {
            animator = playerCharacter.GetComponent<Animator>();
            weaponHolder_R = GameObject.Find("WeaponHolder_R");
            weaponHolder_L = GameObject.Find("WeaponHolder_L");
            weaponHolder_parentR1 = weaponHolder_R.transform.parent;
            weaponHolder_parentL1 = weaponHolder_L.transform.parent;
            weaponHolder_parentR2 = GameObject.Find("Sheath_R").transform;
            weaponHolder_parentL2 = GameObject.Find("Sheath_L").transform;
            ChangeWeaponHolder();
        }

        void Update() {
        }

        public void ChangeMotion(int num) {
            // num= Idle:0 IdleF:1 Death:4 Walk:10 Run:20 GetItem:30
            animator.SetInteger("motionIndex", num);
            if(num == 1 && !isHoldWeapon) {
                ToggleHoldWeapon();
            }
            if(num == 30 && isHoldWeapon) { // GetItem:30
                ToggleHoldWeapon();
            }
        }

        public void ChangeTrigger(string mov) {
            animator.SetTrigger(mov);
            if(mov == "openbox" && isHoldWeapon) {
                ToggleHoldWeapon();
            }
            int motionIndex = animator.GetInteger("motionIndex");
            if(motionIndex == 10 || motionIndex == 20 || motionIndex == 30) { // Walk:10 Run:20 GetItem:30
                animator.SetInteger("motionIndex", 0);
            }
        }

        public void ToggleHoldWeapon() {
            int motionIndex = animator.GetInteger("motionIndex");
            if(motionIndex != 4) { // Death:4
                animator.SetTrigger("holdweapon");
                isHoldWeapon = !isHoldWeapon;
            }
        }

        void ChangeWeaponHolder() {
            Quaternion lrR = weaponHolder_R.transform.localRotation;
            Quaternion lrL = weaponHolder_L.transform.localRotation;
            if(isHoldWeapon) {
                weaponHolder_R.transform.parent = weaponHolder_parentR1;
                weaponHolder_L.transform.parent = weaponHolder_parentL1;
            } else {
                weaponHolder_R.transform.parent = weaponHolder_parentR2;
                weaponHolder_L.transform.parent = weaponHolder_parentL2;
            }
            weaponHolder_R.transform.localRotation = lrR;
            weaponHolder_L.transform.localRotation = lrL;
            weaponHolder_R.transform.localPosition = Vector3.zero;
            weaponHolder_L.transform.localPosition = Vector3.zero;
        }

        public void ChangeThiefMaterial() {
            int num = GameObject.Find("Dropdown_Color").GetComponent<Dropdown>().value;
            playerMesh.GetComponent<Renderer>().materials[0].CopyPropertiesFromMaterial(bodyMaterial[num]);
            playerMesh.GetComponent<Renderer>().materials[1].CopyPropertiesFromMaterial(faceMaterial[num]);
        }

        //State Machine Behaviour

        void AnimationEnter(AnimatorStateInfo stateInfo) {
            if(stateInfo.IsName("HoldWeapon")) {
                Invoke("ChangeWeaponHolder", 0.16f);
            }
        }


    }

}