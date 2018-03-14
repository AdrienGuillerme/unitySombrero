using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EndDoorTrigger : MonoBehaviour {

    public GameObject[] userList;
    private int inUsers;
    public bool flag;


    // Use this for initialization
    void Start () {
		userList = GameObject.FindGameObjectsWithTag("User");
        inUsers = userList.Length;
        flag = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CharacterGroup"))
        {
            ExitUser(other.gameObject);
        }
    }

    private void ExitUser(GameObject user)
    {
        user.SetActive(false);
        inUsers--;
        if(inUsers <= 0)
        {
            flag = true;
            ExitLevel();
        }
    }

    private void ExitLevel()
    {
        SceneManager.LoadScene("Level1"); //TODO: Change level or use level manager
    }
}
