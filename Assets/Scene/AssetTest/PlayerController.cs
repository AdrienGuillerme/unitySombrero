using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Sombrero {

    public class PlayerController : MonoBehaviour {

        public GameObject playerCharacter;
        public int lifeMaxDefault = 50;
        public float attackInterval = 1;

        GameObject weaponHolder_R;
        GameObject weaponHolder_L;
        Transform weaponHolder_parentR1; // hand
        Transform weaponHolder_parentL1;
        Transform weaponHolder_parentR2; // back
        Transform weaponHolder_parentL2;

        Vector3 playerDirection;
        Vector3 playerStartPosition;
        Quaternion playerStartRotation;
        Animator animator;
        CharacterController controller;
        UnityEngine.AI.NavMeshAgent agent;
        GameObject cameraObject;
        Quaternion cameraObjectStartRotation;
        RectTransform lifeBar;
        RectTransform lifeBarBG;
        int lifeBarWidth = 5;

        int life;
        int lifeMax;
        int keyItem;
        float speed = 0;
        float speedWeight = 1;
        float attackIntervalCount;
        int attackAnimation;
        int attackAnimationNum = 4;
        float lerpSpeed = 8;

        bool isMovable;
        bool isMoveToTarget;
        bool isFighting;
        bool isAlive;
        bool isHoldWeapon = false;


        GameObject fightingTarget;
        GameObject currentBox;
        bool currentBoxOpenFlag;
        GameObject currentBoxItem;
        GameObject currentBoxTrigger;


        void Start() {
            animator = playerCharacter.GetComponent<Animator>();
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            weaponHolder_R = transform.Find("Thief/rig/spine/chest/R_arm/R_arm2/R_hand/R_item/WeaponHolder_R").gameObject;
            weaponHolder_L = transform.Find("Thief/rig/spine/chest/L_arm/L_arm2/L_hand/L_item/WeaponHolder_L").gameObject;
            weaponHolder_parentR1 = weaponHolder_R.transform.parent;
            weaponHolder_parentL1 = weaponHolder_L.transform.parent;
            weaponHolder_parentR2 = transform.Find("Thief/rig/spine/chest/Sheath_R");
            weaponHolder_parentL2 = transform.Find("Thief/rig/spine/chest/Sheath_L");

            //lifeBar = GameObject.Find("Image_Life").GetComponent<RectTransform>();
            //lifeBarBG = GameObject.Find("Image_LifeBG").GetComponent<RectTransform>();
            playerStartPosition = transform.position;
            playerStartRotation = transform.rotation;
            //cameraObject = GameObject.Find("CameraObject");
            //cameraObjectStartRotation = cameraObject.transform.rotation;
            lifeMax = lifeMaxDefault;
            Init();
        }

        void Init() {
            transform.position = playerStartPosition;
            transform.rotation = playerStartRotation;
            //cameraObject.transform.rotation = cameraObjectStartRotation;
            gameObject.SetActive(true);
            life = lifeMax;
            attackAnimation = -1;
            attackIntervalCount = 0;
            isMovable = true;
            isMoveToTarget = false;
            isFighting = false;
            isAlive = true;
            fightingTarget = null;
            isHoldWeapon = false;
            keyItem = 0;
            //SetLife();
            //SetCameraY(1);
            weaponHolder_R.gameObject.SetActive(true);
            weaponHolder_L.gameObject.SetActive(true);
            ChangeWeaponHolder();
        }

        void Update() {

            //Walk & Run
            if(isMovable) {
                Move();
            }

            //Auto Move
            if(isMoveToTarget) {
                MoveToTarget();
            }

            //Move <-> Fight
            if(isFighting) {
                if(speed == 0 || !isMovable) {
                    Fight();
                } else {
                    attackAnimation = -1;
                }
                isFighting = false;
            } else {
                fightingTarget = null;
            }

            //Attack
            if(attackIntervalCount <= attackInterval) {
                attackIntervalCount += Time.deltaTime;
            }
            if(Input.GetMouseButton(0) && isMovable) {
                if(isHoldWeapon) {
                    if(attackIntervalCount > attackInterval) {
                        attackAnimation = (attackAnimation + 1) % attackAnimationNum;
                        animator.SetFloat("attackNumber", attackAnimation / (1.0f * attackAnimationNum - 1));
                        animator.SetTrigger("attack");
                        attackIntervalCount = 0;
                    }
                } else {
                    ToggleHoldWeapon();
                    attackIntervalCount = attackInterval - 0.3f;
                }
            }

            //toggle Weapon
            if(Input.GetKeyDown("r")) {
                ToggleHoldWeapon();
            }

            //Cheer
            if(Input.GetKeyDown("x") && isMovable) {
                animator.SetTrigger("cheer");
            }
        }

        void ToggleHoldWeapon() {
            animator.SetTrigger("holdweapon");
            isHoldWeapon = !isHoldWeapon;
            animator.SetBool("battleMode", isHoldWeapon);
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


        void Move() {
            Transform cameratransform = Camera.main.transform;
            cameratransform.rotation = Quaternion.LookRotation(new Vector3(transform.position.x - cameratransform.position.x, 0, transform.position.z - cameratransform.position.z));
            Vector3 forward = cameratransform.TransformDirection(Vector3.forward);
            Vector3 right = cameratransform.TransformDirection(Vector3.right);
            playerDirection = Input.GetAxis("Horizontal") * right + Input.GetAxis("Vertical") * forward;
            speed = playerDirection.magnitude;

            if(Input.GetKey("left shift") || Input.GetKey("right shift")) {
                //walking(+Shift Key)
                if(speedWeight > 0.2f) {
                    speedWeight -= Time.deltaTime * 2.0f;
                } else {
                    speedWeight = 0.2f;
                }
            } else {
                //running
                if(speedWeight < 1.0f) {
                    speedWeight += Time.deltaTime * 2.0f;
                } else {
                    speedWeight = 1.0f;
                }
            }
            speed = speed * speedWeight;
            if(speed > 0) {
                if(playerDirection != Vector3.zero) {
                    Quaternion playerRotation = Quaternion.LookRotation(playerDirection);
                    transform.rotation = Quaternion.Lerp(transform.rotation, playerRotation, Time.deltaTime * lerpSpeed);
                }
                agent.Move(transform.forward * Time.deltaTime * speed * 3.0f);
            }
            animator.SetFloat("speed", speed);
        }

        void MoveToTarget() {
            Vector3 triggerPosition = currentBoxTrigger.transform.position;
            triggerPosition.y = transform.position.y;
            transform.position = Vector3.Lerp(transform.position, triggerPosition, Time.deltaTime * lerpSpeed);
            Vector3 targetDirection = currentBox.transform.position - transform.position;
            Vector3 triggerDirection = currentBoxTrigger.transform.position - transform.position;
            targetDirection.y = 0;
            if(targetDirection != Vector3.zero) {
                Quaternion playerRotation = Quaternion.LookRotation(targetDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, playerRotation, Time.deltaTime * lerpSpeed);
            }
            if(currentBoxOpenFlag) {
                speed = speed / 1.5f;
                if(speed < 0.1f) {
                    speed = 0;
                }
            } else {
                speed = (speed + 0.5f) / 2;
            }
            animator.SetFloat("speed", speed);
            if(!currentBoxOpenFlag && (triggerDirection.magnitude < 0.1f)) {
                animator.SetTrigger("openbox");
                currentBoxOpenFlag = true;
            }
        }

        void Fight() {
            if(fightingTarget) {
                Vector3 targetDirection = fightingTarget.transform.position - transform.position;
                targetDirection.y = 0;
                if(targetDirection != Vector3.zero) {
                    Quaternion playerRotation = Quaternion.LookRotation(targetDirection);
                    transform.rotation = Quaternion.Lerp(transform.rotation, playerRotation, Time.deltaTime * lerpSpeed);
                }
            }
        }

        void GoFight(GameObject target) {
            fightingTarget = target;
            isFighting = true;
        }

        void Damaged(int d) {
            life -= d;
            SetLife();
            GameObject prefab = (GameObject)Resources.Load("PlayerDamagedEffect");
            GameObject damageEffect = Instantiate(prefab, new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z), Quaternion.identity) as GameObject;
            Destroy(damageEffect, damageEffect.GetComponent<ParticleSystem>().duration);
            if(life <= 0) {
                Die();
            } else {
                AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
                if(info.IsName("Base Layer.Idle_StateMachine.Idle") || info.IsName("Base Layer.Idle_StateMachine.IdleF")) {
                    animator.SetTrigger("damaged");
                }
            }
        }

        void SetLife() {
            lifeBar.sizeDelta = new Vector2(life * lifeBarWidth, 20);
            lifeBarBG.sizeDelta = new Vector2(lifeMax * lifeBarWidth, 20);
        }


        //Trigger Event

        void OnTriggerEnter(Collider collider) {
            var n = collider.gameObject.name;
            switch(n) {
                case "TriggerChangeCamera1":
                    SetCameraY(1);
                    break;
                case "TriggerChangeCamera2":
                    SetCameraY(5);
                    break;
            }

            if(collider.gameObject.tag == "Key") {
                GameObject key = collider.gameObject;
                keyItem++;
                Vector3 pos = collider.gameObject.transform.position;
                pos.y += 1.3f;
                key.SendMessage("MoveTo", pos);
                collider.enabled = false;
            }

            if(collider.gameObject.tag == "Door") {
                if(keyItem > 0) {
                    keyItem--;
                    Animator animator_door = collider.gameObject.GetComponent<Animator>();
                    animator_door.SetInteger("motionIndex", 1);
                    collider.enabled = false;
                }
            }
        }

        //void OnTriggerStay(Collider collider) {
        //    var n = collider.gameObject.name;
        //    switch(n) {
        //        case "BoxTrigger":
        //            if(!GetNearbyEnemy()) {
        //                currentBoxTrigger = collider.gameObject;
        //                currentBoxOpenFlag = false;
        //                currentBox = currentBoxTrigger.transform.parent.gameObject;
        //                collider.enabled = false;
        //                isMovable = false;
        //                isMoveToTarget = true;
        //            }
        //            break;
        //    }
        //}

        void SetCameraY(float y) {
            GameObject camera = GameObject.Find("Main Camera");
            Vector3 cameraPosition = camera.transform.localPosition;
            cameraPosition.y = y;
            camera.transform.localPosition = cameraPosition;
        }

        void Die() {
            isFighting = false;
            isAlive = false;
            isMovable = false;
            animator.SetTrigger("death");
            animator.SetFloat("speed", 0);
            Invoke("Respawn", 5);
        }

        public void Respawn() {
            gameObject.SetActive(false);
            CancelInvoke();
            Init();
        }

        public void Reset() {
            gameObject.SetActive(false);
            CancelInvoke();
            lifeMax = lifeMaxDefault;
            Init();
        }

        //State Machine Behaviour

        void AnimationEnter(AnimatorStateInfo stateInfo) {
            if(stateInfo.IsName("Attack_Tree")) {
                isMovable = false;
                if(attackAnimation == 3) {
                    Invoke("HitEnemy", 0.7f);
                } else {
                    Invoke("HitEnemy", 0.5f);
                }

            }

            if(stateInfo.IsName("Cheer")) {
                isMovable = false;
            }
            if(stateInfo.IsName("OpenBox")) {
                if(isHoldWeapon) {
                    ToggleHoldWeapon();
                }
                Invoke("OpenBox", 1.0f);
            }
            if(stateInfo.IsName("GetItem")) {
                isMoveToTarget = false;
                Invoke("ShowItem", 0.6f);
            }
            if(stateInfo.IsName("HoldWeapon")) {
                Invoke("ChangeWeaponHolder", 0.16f);
            }

        }

        void AnimationExit(AnimatorStateInfo stateInfo) {
            if(isAlive) {
                if(stateInfo.IsName("Attack_Tree")) {
                    isMovable = true;
                    animator.ResetTrigger("damaged");
                }

                if(stateInfo.IsName("GetItem")) {
                    if(currentBoxItem) {
                        Destroy(currentBoxItem);
                    }
                }

                if(stateInfo.IsName("Cheer")) {
                    isMovable = true;
                }

            }
        }

        void HitEnemy() {
            if(fightingTarget) {
                fightingTarget.SendMessage("Damaged", 1);
            }
        }

        //void OpenBox() {
        //    currentBox.SendMessage("Open");
        //    var itemname = currentBox.GetComponent<BoxController>().GetItemName();
        //    GameObject prefab = (GameObject)Resources.Load(itemname);
        //    currentBoxItem = Instantiate(prefab, currentBox.transform.position, Quaternion.identity) as GameObject;
        //    Vector3 pos = currentBoxItem.transform.position;
        //    pos.y = currentBox.transform.position.y + 0.2f;
        //    currentBoxItem.transform.position = pos;
        //}

        void ShowItem() {
            Vector3 pos = currentBoxItem.transform.position;
            pos.y += 1.3f;
            currentBoxItem.SendMessage("MoveTo", pos);
            Invoke("SetMovable", 1.0f);
            if(currentBoxItem.name.IndexOf("Item_Heart") > -1) {
                lifeMax += 50;
                life = lifeMax;
                SetLife();
            }
        }

        void SetMovable() {
            isMovable = true;
        }


        //------------------

        public bool GetIsAlive() {
            return (isAlive);
        }

        //public bool GetNearbyEnemy() {
        //    GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        //    bool flag = false;
        //    foreach(GameObject e in enemy) {
        //        if(e.GetComponent<EnemyController>().GetIsCaution()) {
        //            flag = true;
        //        }
        //    }
        //    return (flag);
        //}

    }

}
