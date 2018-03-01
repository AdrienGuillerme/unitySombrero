using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour {
    private bool isPaused = false;
    private bool startPressed = false;
    private bool isActing = false;
    private int delay = 0;
    private int indiceButton = 0;

    public Transform canvas;
    public Transform loadingCanvas;
    public Transform optionCanvas;

    private Button[] buttons;
    private int nbButtons;

    private List<string> listController = new List<string>();

	// Use this for initialization
	void Start () {
        GameObject[] users = GameObject.FindGameObjectsWithTag("User");
        foreach(GameObject user in users)
        {
            listController.Add(user.GetComponent<DontDestroy>().controllerName);
        }

        buttons = canvas.GetComponentsInChildren<Button>();
        nbButtons = buttons.Length;
    }
	
	// Update is called once per frame
	void Update () {
        if (isActing)
        {
            delay--;
            if (delay == 0)
            {
                isActing = false;
                buttons[indiceButton].Select();
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
                    optionCanvas.gameObject.SetActive(false);
                    indiceButton = 0;
                    Delay(30);
                }

                if (isPaused)
                {
                    float v;
                    if (controller == "Keyboard")
                    {
                        v = Input.GetAxisRaw("Vertical");
                        Delay(10);
                    }
                    else
                    {
                        v = Input.GetAxisRaw(controller + "LStickY");
                        Delay(10);
                    }

                    if (v != 0)
                    {
                        if (v < 0)
                        {

                            indiceButton = (indiceButton + 1) % nbButtons;
                            buttons[indiceButton].Select();
                        }
                        else
                        {
                            indiceButton = (indiceButton + nbButtons - 1) % nbButtons;
                            buttons[indiceButton].Select();
                        }

                    }
                    if (Input.GetButton(controller + "Action"))
                    {
                        switch (indiceButton)
                        {
                            case 0:
                                ResumeGame();
                                break ;
                            case 1 :
                                Options();
                                break;
                            case 2:
                                loadingCanvas.gameObject.SetActive(true);
                                SceneManager.LoadScene("MapSelection");
                                break;
                            case 3:
                                QuitGame();
                                break;


                        }
                    }
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

    private void ResumeGame()
    {
        isPaused = !isPaused;
        canvas.gameObject.SetActive(isPaused);
        indiceButton = 0;
        Delay(30);
    }

    private void QuitGame()
    {
        loadingCanvas.gameObject.SetActive(true);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
    private void Options()
    {
        Debug.Log("Options");
        canvas.gameObject.SetActive(false);
        optionCanvas.gameObject.SetActive(true);
    }


    private void Delay(int time)
    {
        isActing = true;
        delay = time;
    }
}
