﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapSelection : MonoBehaviour {

    public Image map1;
    public string name1;
    public Image map2;
    public string name2;
    public Image map3;
    public string name3;
    List<Image> listImage = new List<Image>();
    List<string> listName = new List<string>();

    public GameObject objText;   
    public GameObject objMap;
    private Image map;
    private Text text;
    private GameObject[] users;

    private int nbMap;
    private int cpt = 0;
    private List<string> controllers = new List<string>();
    private bool isActing;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1f;
        isActing = false;
        users = GameObject.FindGameObjectsWithTag("User");
        foreach(GameObject user in users)
        {
            controllers.Add(user.GetComponent<DontDestroy>().controllerName);
        }

        listImage.Add(map1);
        listImage.Add(map2);
        listImage.Add(map3);

        listName.Add(name1);
        listName.Add(name2);
        listName.Add(name3);

        nbMap = listImage.Count;
        map = objMap.GetComponent<Image>();
        text = objText.GetComponent<Text>();
        ChangeImage();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActing)
        {
            foreach (string controller in controllers)
            {
                float h;
                if (controller == "Keyboard")
                {
                    h = Input.GetAxisRaw("Horizontal");
                    StartCoroutine(DoAction(0.1f));
                }
                else
                {
                    h = Input.GetAxisRaw(controller + "LStickX");
                    StartCoroutine(DoAction(0.1f));
                }

                if (h != 0)
                {
                    if (h > 0)
                    {
                        cpt = (cpt + 1) % nbMap;
                        ChangeImage();
                    }
                    else
                    {
                        cpt = (cpt + nbMap - 1) % nbMap;
                        ChangeImage();
                    }
                }

                if (Input.GetButton(controller + "Action"))
                {
                  
                   /* foreach(GameObject user in users)
                    {
                        user.gameObject.SetActive(true);
                    }*/
                    SceneManager.LoadScene(listName[cpt]);
                }
            }
           
        }
    }

    private void ChangeImage()
    {
        map.sprite = listImage[cpt].sprite;
        text.text = listName[cpt];
    }

    IEnumerator DoAction(float delay)
    {
        isActing = true;
        yield return new WaitForSeconds(delay);
        isActing = false;
    }
}