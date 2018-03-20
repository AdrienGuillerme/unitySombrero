using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailMenu : MonoBehaviour {

    public Transform loadingCanvas;

    private void Start()
    {
        
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


}
