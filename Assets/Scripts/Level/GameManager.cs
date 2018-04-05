using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public PlayerHealth[] playerHealths;
    public GameObject FailMenu;

    // Use this for initialization
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
        playerHealths = GameObject.FindObjectsOfType<PlayerHealth>();
        Physics.gravity = new Vector3(0, -100.0F, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckPlayersDead())
        {
            StartCoroutine(SetDeath());
        }
    }

    private bool CheckPlayersDead()
    {
        bool check = true;
        foreach (var playerHealth in playerHealths)
        {
            check &= playerHealth.IsDead();
        }
        return check;
    }

    IEnumerator SetDeath()
    {
        yield return new WaitForSeconds(2);
        Time.timeScale = 0f;
        FailMenu.SetActive(true);
    }
}
