using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadUI : MonoBehaviour
{
    public GameObject hudCanvas;
    public GameObject heathUILeft;
    public GameObject heathUIRight;


    // Use this for initialization
    void Start()
    {
        GameObject[] users = GameObject.FindGameObjectsWithTag("User");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        //Player Tag only for CharacterGroup->Player
        int cpt = 0;
        foreach (GameObject player in players)
        {
            GameObject newHealthUI;
            if (cpt % 2 == 1)
            {
                newHealthUI = Instantiate(heathUIRight, hudCanvas.transform);
                if (cpt == 3)
                {
                    newHealthUI.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 60, 1);
                }
            }
            else
            {

                newHealthUI = Instantiate(heathUILeft, hudCanvas.transform);
                if (cpt == 2)
                {

                    newHealthUI.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 60, 1);

                }
            }

            PlayerHealth newPlayerHealth = player.GetComponent<PlayerHealth>();
            newPlayerHealth.healthSlider = newHealthUI.GetComponentInChildren<Slider>();
            GameObject user = users[cpt];
            DontDestroy newUserScore = user.GetComponent<DontDestroy>();
            newUserScore.uiText = newHealthUI.GetComponentInChildren<Text>();
            newUserScore.AddScore(0);
            cpt++;
        }


    }
}
