using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {


    List<string> listControllerToCheck = new List<string>();
    List<string> listControllerUsed = new List<string>();
    List<GameObject> listPanel = new List<GameObject>();
    List<GameObject> listPlayer = new List<GameObject>();

    int cptPlayer = 0;

    public GameObject uL;
    public GameObject uR;
    public GameObject dL;
    public GameObject dR;

    public GameObject player;
    public GameObject character1;


    // Use this for initialization
    void Start() {
        
        listControllerToCheck.Add("Joy1");
        listControllerToCheck.Add("Joy2");
        listControllerToCheck.Add("Joy3");
        listControllerToCheck.Add("Joy4");
        listControllerToCheck.Add("Keyboard");

        listPanel.Add(uL);
        listPanel.Add(uR);
        listPanel.Add(dL);
        listPanel.Add(dR);
    }
	
	// Update is called once per frame
	void Update () {
        // try to detect new controller
        if (listControllerUsed.Count != 4)
        {
            string[] listControllerInArray = listControllerToCheck.ToArray();

            foreach (string controller in listControllerInArray)
            {
                if (listControllerUsed.Count != 4)
                {
                    if (Input.GetButton(controller + "Action"))
                    {
                        listControllerUsed.Add(controller);
                        listControllerToCheck.Remove(controller);
                        Text valu = listPanel.ElementAt(cptPlayer).GetComponentInChildren<Text>();
                        valu.text = "Appuyez sur Start pour lancer la partie";
                        cptPlayer++;
                        GameObject newPlayer =  Instantiate(player, new Vector3(cptPlayer, cptPlayer,0), new Quaternion(0,0,0,0));
                        listPlayer.Add(newPlayer);
                       

                    }
                }

            }
        }
        if (cptPlayer > 0)
        {
            string[] listControllerInArray = listControllerUsed.ToArray();

            foreach (string controller in listControllerInArray)
            {

                if (Input.GetButton(controller + "Start"))
                {
                    int cpt = 0;
                    GameObject[] listPlayerInArray = listPlayer.ToArray();
                    foreach(GameObject newPlayer in listPlayerInArray)
                    {
                       GameObject newCharacter =  Instantiate(character1, new Vector3(cpt, 0, cpt), new Quaternion(0, 0, 0, 0));
                        newCharacter.transform.parent = newPlayer.transform;
                        DontDestroy playerFunction = newPlayer.GetComponent<DontDestroy>();
                        playerFunction.controllerName = listControllerInArray[cpt];
                        cpt++;
                    }
                    SceneManager.LoadScene("Test");
                }
            }
        }
		
	}
}
