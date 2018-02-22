using UnityEngine;
using System.Collections;

namespace LittleThief {

    public class EnemyController : MonoBehaviour {

        public GameObject enemyCharacter;
        public int lifeMax = 3;
        public float attackSpeed = 3;
        public float attackSpeedAtFirstAttack = 1;
        public float attackAreaRadius = 2;
        public float causionAreaRadius = 3;
        public float Damage = 10;

        GameObject player;
        Vector3 enemyDirection;
        Animator animator;

        int life = 0;
        float attackCount = 0;
        bool isFighting = false;
        bool isCaution = false;
        bool isAlive = true;
        bool isFirstAttack = true;

        GameObject currentTarget;

        void Start() {
            animator = enemyCharacter.GetComponent<Animator>();
            player = GameObject.FindWithTag("Player");
            Init();
        }

        void Init() {
            life = lifeMax;
            isFighting = false;
            isCaution = false;
            isAlive = true;
        }

        void Update() {
            if(player.GetComponent<PlayerController>().GetIsAlive() && isAlive) {
                Vector3 targetDirection = player.transform.position - transform.position;
                if(targetDirection.magnitude < attackAreaRadius) {
                    isFighting = true;
                    isCaution = true;
                } else {
                    isFirstAttack = true;
                    if(targetDirection.magnitude < causionAreaRadius) {
                        isFighting = false;
                        isCaution = true;
                        attackCount = 0;
                    } else {
                        isFighting = false;
                        isCaution = false;
                    }
                }
                if(isCaution || isFighting) {
                    Quaternion enemyRotation = Quaternion.LookRotation(targetDirection);
                    transform.rotation = Quaternion.Lerp(transform.rotation, enemyRotation, Time.deltaTime * 15);

                }
                if(isFighting) {
                    player.SendMessage("GoFight", this.gameObject);
                    attackCount += Time.deltaTime;
                    if(isFirstAttack) {
                        attackCount = attackSpeed - attackSpeedAtFirstAttack;
                        isFirstAttack = false;
                    }
                    if(attackCount >= attackSpeed) {
                        animator.SetTrigger("attack");
                        attackCount = 0;
                    }
                }
            }
        }

        void HitPlayer() {
            player.SendMessage("Damaged", Damage);
            animator.ResetTrigger("damaged");
        }

        void Damaged(int d) {
            if(isFighting) {
                AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
                if(info.IsName("Base Layer.Idle")) {
                    animator.SetTrigger("damaged");
                }
                GameObject prefab = (GameObject)Resources.Load("EnemyDamagedEffect");
                GameObject damageEffect = Instantiate(prefab, new Vector3(transform.position.x, transform.position.y + 0.6f * transform.localScale.x, transform.position.z), transform.rotation) as GameObject;
                damageEffect.transform.localScale = transform.localScale;
                Destroy(damageEffect, damageEffect.GetComponent<ParticleSystem>().duration);
                life -= d;
                if(life <= 0) {
                    isFighting = false;
                    isCaution = false;
                    isAlive = false;
                    animator.SetTrigger("death");
                    GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = false;
                }
            }
        }

        void Hide() {
            enemyCharacter.GetComponent<MeshRenderer>().enabled = false;
        }

        public void Respawn() {
            if(!isAlive) {
                GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = true;
                enemyCharacter.GetComponent<MeshRenderer>().enabled = true;
                animator.SetTrigger("respawn");
            }
            Init();
        }

        public bool GetIsCaution() {
            return (isCaution);
        }

    }

}
