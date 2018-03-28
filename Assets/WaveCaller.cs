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

    public bool on = false;
	// Use this for initialization
	void Start () {

        relatedManager = this.GetComponentInParent<EnemiesManager>();
        if(waveSize != 0)
        {
            initVector(waveSize);
        }
        if (relatedManager != null && relatedManager.isActiveAndEnabled)
        {
            relatedManager.chosenPatrolPositions = chosenPositions;
            relatedManager.enabled = true;
        }
	}
	
	// Update is called once per frame
	void Update () {

        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            if (nextWave != null)
                nextWave.SetActive(true);
            else if (door != null) { 
                door.GetComponentInChildren<OpenGateAnim>().GateOpen();
            }
            else if (gameManager != null)
                gameManager.GetType();

            this.transform.parent.gameObject.SetActive(false);
        }
		
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
