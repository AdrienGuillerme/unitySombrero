using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadUI : MonoBehaviour {
    public GameObject hudCanvas;
    public GameObject heathUILeft;
    public GameObject heathUIRight;


    // Use this for initialization
    void Start () {
        
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        //Player Tag only for CharacterGroup->Player
        int cpt = 1;
        foreach(GameObject player in players)
        { GameObject newHealthUI;
            if (cpt%2 == 0)
            {
                newHealthUI = Instantiate(heathUIRight,hudCanvas.transform);
                if (cpt == 4)
                {
                    newHealthUI.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 60, 1);
                } 
            }
            else
            {

                newHealthUI =Instantiate(heathUILeft,hudCanvas.transform);
                if (cpt == 3)
                {
                    
                        newHealthUI.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 60, 1);
                    
                }
            }

            PlayerHealth newPlayerHealth = player.GetComponent<PlayerHealth>();
            //Debug.Log(newHealthUI.GetComponentInChildren<Slider>());
            newPlayerHealth.healthSlider = newHealthUI.GetComponentInChildren<Slider>();
            cpt++;
        }

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
