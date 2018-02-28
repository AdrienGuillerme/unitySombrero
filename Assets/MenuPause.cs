using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour {
    private bool isPaused = false;
    private bool startPressed = false;
    private bool isActing = false;

    public Transform canvas;

    private List<string> listController = new List<string>();
	// Use this for initialization
	void Start () {
        GameObject[] users = GameObject.FindGameObjectsWithTag("User");
        foreach(GameObject user in users)
        {
            listController.Add(user.GetComponent<DontDestroy>().controllerName);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
        if (isPaused)
        {
            Time.timeScale = 0f;
            foreach (string controller in listController)
            {
                if ((Input.GetButton(controller + "Action")) && (!isActing))
                {
                    isPaused = !isPaused;
                    DoAction(0.2f);
                    canvas.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            Time.timeScale = 1f;
            foreach (string controller in listController)
            {
                if ((Input.GetButton(controller + "Start")) && (!isActing))
                {
                    isPaused = !isPaused;
                    DoAction(0.2f);
                    canvas.gameObject.SetActive(true);
                }
            }
        }
    }

    IEnumerator DoAction(float delay)
    {
        isActing = true;
        yield return new WaitForSeconds(delay);
        isActing = false;
    }
}
