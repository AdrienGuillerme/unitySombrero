using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour {

    List<string> listControllerToCheck = new List<string>();

    public GameObject loading;
    public Text text;

    private bool textBlink = false;
    private bool textActive = true;

    // Use this for initialization
    void Start () {
        listControllerToCheck.Add("Joy1");
        listControllerToCheck.Add("Joy2");
        listControllerToCheck.Add("Joy3");
        listControllerToCheck.Add("Joy4");
        listControllerToCheck.Add("Keyboard");
    }
	
	// Update is called once per frame
	void Update () {

        foreach (string controller in listControllerToCheck)
        {
            if (Input.GetButton(controller + "Start"))
            {
                loading.SetActive(true);
                SceneManager.LoadScene("CharacterSelect");
            }
        }

        if (!textBlink)
        {
            text.gameObject.SetActive(!textActive);
            textActive = !textActive;
            StartCoroutine(DoAction(0.5f));
        }
    }


    IEnumerator DoAction(float delay)
    {
        textBlink = true;
        yield return new WaitForSeconds(delay);
        textBlink = false;
    }
}
