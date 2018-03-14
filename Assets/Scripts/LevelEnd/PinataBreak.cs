using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinataBreak : MonoBehaviour {

    private Transform parent;
    private GameObject pinata_normal;
    private GameObject pinata_broken;
    public GameObject endingDoor;
    public bool breakPinata;
    private bool broken = false;
    public int maxhp = 100;
    public int minhp = 10;
    public int hp;

	void Start () {
        parent = transform.parent;
        pinata_broken = parent.Find("Unicorn_Shatters").gameObject;
        pinata_normal = parent.Find("Unicorn").gameObject;
        hp = Random.Range(minhp, maxhp);
    }
	
	void Update () {
        if (breakPinata && !broken)
        {
            BreakPinata();
        }
        if(hp <= 0 && !broken)
        {
            Die();
        }
    }

    public void BreakPinata()
    {
        pinata_normal.SetActive(false);
        pinata_broken.SetActive(true);
        broken = true;
    }

    private void Die()
    {
        BreakPinata();
        endingDoor.SetActive(true);
        this.enabled = false;
    }

    private void LoseHP(int value)
    {
        this.hp -= value;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Weapons"))
        {
            LoseHP(10);
        }
    }
}
