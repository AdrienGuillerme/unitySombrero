using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    private bool isPaused = false;
    private bool isOptions = false;
    private bool isActing = false;
    private int delay = 0;
    private int indiceButton = 0;
    private int indiceOption = 0;

    public Transform canvas;
    public Transform loadingCanvas;
    public Transform optionCanvas;

    public Slider sliderVolume;
    public Button buttonQuit;
    private int nbInPanel = 2;

    private Button[] buttons;
    private int nbButtons;



    private List<string> listController = new List<string>();

    // Use this for initialization
    void Start()
    {
        GameObject[] users = GameObject.FindGameObjectsWithTag("User");
        foreach (GameObject user in users)
        {
            listController.Add(user.GetComponent<DontDestroy>().controllerName);
        }

        buttons = canvas.GetComponentsInChildren<Button>();
        nbButtons = buttons.Length;
    }

    // Update is called once per frame
    void Update()
    {
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
                    isOptions = false;
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

                    if (isOptions)
                    {
                        if (v != 0)
                        {
                            if (v < 0)
                            {

                                indiceOption = (indiceOption + 1) % nbInPanel;
                            }
                            else
                            {
                                indiceOption = (indiceOption + nbInPanel - 1) % nbInPanel;
                            }

                            switch (indiceOption)
                            {
                                case 0:
                                    sliderVolume.Select();
                                    break;
                                case 1:
                                    buttonQuit.Select();
                                    break;
                            }
                        }


                        switch (indiceOption)
                        {
                            case 1:

                                if (Input.GetButton(controller + "Action"))
                                {
                                    QuitOptions();
                                }
                                break;
                            case 0:

                                float h;
                                if (controller == "Keyboard")
                                {
                                    h = Input.GetAxisRaw("Horizontal");
                                    Delay(10);
                                }
                                else
                                {
                                    h = Input.GetAxisRaw(controller + "LStickX");
                                    Delay(10);
                                }
                                if (h != 0)
                                {
                                    if (h > 0)
                                    {

                                        sliderVolume.value++;
                                    }
                                    else
                                    {
                                        sliderVolume.value--;
                                    }
                                }

                                break;
                        }


                    }
                    else
                    {
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
                                    break;
                                case 1:
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
        isOptions = true;
        canvas.gameObject.SetActive(false);
        optionCanvas.gameObject.SetActive(true);
    }

    private void QuitOptions()
    {
        isOptions = false;
        canvas.gameObject.SetActive(true);
        optionCanvas.gameObject.SetActive(false);
    }


    private void Delay(int time)
    {
        isActing = true;
        delay = time;
    }
}
