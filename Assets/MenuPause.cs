using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour {
    private bool isPaused = false;
    private bool startPressed = false;
    private bool isActing = false;
    private int delay = 0;

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
        if (isActing)
        {
            delay--;
            if (delay == 0)
            {
                isActing = false;
            }
        }
        else
        {
            foreach (string controller in listController)
            {
                if (Input.GetButton(controller + "Start"))
                {
                    isPaused = !isPaused;
                    canvas.gameObject.SetActive(isPaused);
                    Delay(30);
                }
            }

        }
        

        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    private void Delay(int time)
    {
        isActing = true;
        delay = time;
    }
    IEnumerator DoAction(float delay)
    {
        isActing = true;
        yield return new WaitForSeconds(delay);
        isActing = false;
    }
}
