using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCaller : MonoBehaviour {

    private Vector3[] chosenPositions;
    public int waveSize;
    EnemiesManager relatedManager;
    public GameObject nextWave;
    public GameObject door;
    public GameObject boss;
    public GameManager gameManager;
    private int delay = 0;
    private bool active = false;
	// Use this for initialization
	void Start () {

        relatedManager = transform.parent.GetComponentInChildren<EnemiesManager>(true);
        if(waveSize != 0)
        {
            initVector(waveSize);
        }
        if (relatedManager != null)
        {
            relatedManager.chosenPatrolPositions = chosenPositions;
            relatedManager.gameObject.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {

        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && active)
        {
            if (nextWave != null)
                nextWave.SetActive(true);
            if (door != null) { 
                door.GetComponentInChildren<OpenGateAnim>().GateOpen();
            }
            if (boss != null) {
                boss.SetActive(true);
            }
            if (gameManager != null)
                gameManager.GetType();

            this.transform.parent.gameObject.SetActive(false);
        }
       
        if(delay<5)
        {
            delay++;
        }

        if (delay == 5 && active == false)
            active = true;
		
	}


    void initVector(int taille)
    {
        chosenPositions = new Vector3[taille];
        double div = 2*System.Math.PI / taille;

        for(int i =0; i<taille; i++)
        {
            chosenPositions[i] = transform.position + new Vector3(taille*(float)System.Math.Cos(div * i), 0, taille*(float)System.Math.Sin(div * i));
        }
       
    }
}
