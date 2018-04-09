using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FailMenu : MonoBehaviour {

    private bool isActing = false;
    private int delay = 0;
    public bool isEnabled = false;

    private int indiceButton = 0;
    private int indiceOption = 0;

    public Transform loadingCanvas;
    public Transform canvas;

    private Button[] buttons;
    private int nbButtons;

    private List<string> listController = new List<string>();

    private void Start()
    {
        GameObject[] users = GameObject.FindGameObjectsWithTag("User");
        foreach (GameObject user in users)
        {
            listController.Add(user.GetComponent<DontDestroy>().controllerName);
        }
        buttons = canvas.GetComponentsInChildren<Button>();
        nbButtons = buttons.Length;
    }

    private void Update()
    {
        if (isActing)
        {
            DecreaseDelay();
        }
        else
        {
            if (isEnabled)
            {
                foreach (string controller in listController)
                {
                    float v = GetVerticalInput(controller);
                    IfVerticalButton(v, controller);
                }
            }
        }
    }

    private void IfVerticalButton(float v, string controller)
    {
        if (Input.GetButton(controller + "Action"))
        {
            Delay(30);
            SwitchButton();
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
    }

    private float GetVerticalInput(string controller)
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
        return v;
    }

    private void DecreaseDelay()
    {
        delay--;
        if (delay == 0)
        {
            isActing = false;

        }
    }


    private void SwitchButton()
    {
        switch (indiceButton)
        {
            case 0:
                RestartLevel();
                break;
            case 1:
                ReturnMapSelection();
                break;
            case 2:
                QuitGame();
                break;
        }
    }


    public void RestartLevel()
    {
        loadingCanvas.gameObject.SetActive(true);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void QuitGame()
    {
        loadingCanvas.gameObject.SetActive(true);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ReturnMapSelection()
    {
        loadingCanvas.gameObject.SetActive(true);
        GameObject[] characters = GameObject.FindGameObjectsWithTag("CharacterGroup");
        foreach (GameObject charachter in characters)
        {
            charachter.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        SceneManager.LoadScene("MapSelection");
    }

    private void Delay(int time)
    {
        isActing = true;
        delay = time;
    }

}
