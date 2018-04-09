using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    List<string> listControllerToCheck = new List<string>();
    List<string> listControllerToChooseCapacity = new List<string>();
    List<string> listControllerUsed = new List<string>();

    List<GameObject> listPanel = new List<GameObject>();
    List<GameObject> listUser = new List<GameObject>();

    List<Image> listImageUsed = new List<Image>();
    List<Image> listImage = new List<Image>();

    Dictionary<string, DontDestroy> hashUser = new Dictionary<string, DontDestroy>();
    Dictionary<string, Image[]> hashUserImages = new Dictionary<string, Image[]>();
    Dictionary<string, bool> hashIsActing = new Dictionary<string, bool>();
    Dictionary<string, Text> hashText = new Dictionary<string, Text>();


    int cptUser = 0;
    int cptCapacity = 0;

    public GameObject uL;
    public GameObject uR;
    public GameObject dL;
    public GameObject dR;

    public GameObject user;
    public GameObject character1;

    public Material sombreroMaterial;

    public Image capacity1;
    public Image capacity2;
    public Image capacity3;
    public Image capacity4;
    //public Image capacity4; have to create a prefab with the same name and the good source image
    private int nbCapacity;
    public Image validation;



    // Use this for initialization
    void Start()
    {
        /*if(sombreroMaterial.Length = 0)
        {
            Debug.Log("missing some sombrero materials");
            Application.Quit();
        }*/

        listControllerToCheck.Add("Joy1");
        listControllerToCheck.Add("Joy2");
        listControllerToCheck.Add("Joy3");
        listControllerToCheck.Add("Joy4");
        listControllerToCheck.Add("Keyboard");

        listPanel.Add(uL);
        listPanel.Add(uR);
        listPanel.Add(dL);
        listPanel.Add(dR);

        listImage.Add(capacity1);
        listImage.Add(capacity2);
        listImage.Add(capacity3);
        listImage.Add(capacity4);

        nbCapacity = listImage.Count;
    }

    // Update is called once per frame
    void Update()
    {
        // try to detect new controller
        if (listControllerToChooseCapacity.Count != 4)
        {
            string[] listControllerInArray = listControllerToCheck.ToArray();

            foreach (string controller in listControllerInArray)
            {
                if (listControllerToChooseCapacity.Count != 4)
                {
                    if (Input.GetButton(controller + "Action"))
                    {
                        StartCoroutine(DoAction(0.2f, controller));

                        listControllerToChooseCapacity.Add(controller);
                        listControllerToCheck.Remove(controller);

                        Text valu = listPanel.ElementAt(cptUser).GetComponentInChildren<Text>();
                        Image[] images = listPanel.ElementAt(cptUser).GetComponentsInChildren<Image>();

                        valu.enabled = false;
                        hashText[controller] = valu;
                        hashUserImages[controller] = images;

                        int i = 0;


                        foreach (Image image in images)
                        {
                            if (image.name == "Capacity")
                            {
                                
                                while (!TestIfImageAvailable(i)) { i++; }
                                image.sprite = listImage[i].sprite;

                            }
                            image.enabled = true;
                        }

                        GameObject newUser = Instantiate(user, new Vector3(cptUser, cptUser, 0), new Quaternion(0, 0, 0, 0));
                        listUser.Add(newUser);
                        hashUser[controller] = newUser.GetComponent<DontDestroy>();
                        hashUser[controller].cptCapacity = i;
                        cptUser++;
                    }
                }

            }
        }


        // ChooseCapacity
        if (cptUser > 0)
        {
            string[] listControllerInArray = listControllerToChooseCapacity.ToArray();
            foreach (string controller in listControllerInArray)
            {
                if (!hashIsActing[controller])
                {
                    float h;
                    if (controller == "Keyboard")
                    {
                        h = Input.GetAxisRaw("Horizontal");
                        StartCoroutine(DoAction(0.1f, controller));
                    }
                    else
                    {
                        h = Input.GetAxisRaw(controller + "LStickX");
                        StartCoroutine(DoAction(0.1f, controller));
                    }

                    if (h != 0)
                    {
                        if (h > 0)
                        {
                            int i  = (hashUser[controller].cptCapacity + 1) % nbCapacity;
                            while (!TestIfImageAvailable(i)) { i = (i + 1) % nbCapacity; }
                            hashUser[controller].cptCapacity = i;
                            ChangeImage(controller);
                        }
                        else
                        {
                            int i = (hashUser[controller].cptCapacity + nbCapacity - 1) % nbCapacity;
                            while (!TestIfImageAvailable(i)) { i = (i + nbCapacity - 1) % nbCapacity; }
                            hashUser[controller].cptCapacity = i;
                            ChangeImage(controller);
                        }
                    }

                    if (Input.GetButton(controller + "Action"))
                    {
                        ChooseCapacity(controller);
                        OtherCapacity(controller, listControllerInArray);
                    }

                }
            }
        }


        if ((cptUser > 0) && (cptCapacity == cptUser))
        {
            string[] listControllerInArray = listControllerUsed.ToArray();

            foreach (string controller in listControllerInArray)
            {

                if (Input.GetButton(controller + "Start"))
                {
                    int cpt = 0;
                    GameObject[] listUserInArray = listUser.ToArray();
                    foreach (GameObject newUser in listUserInArray)
                    {
                        GameObject newCharacter = Instantiate(character1, new Vector3(cpt*5 +2000, 0, cpt*5 +2000), new Quaternion(0, 0, 0, 0));
                        newCharacter.transform.parent = newUser.transform;
                        newCharacter.transform.Find("Player/rig/spine/chest/neck/head/MexicanHat").GetComponent<MeshRenderer>().material = sombreroMaterial;
                        DontDestroy userFunction = newUser.GetComponent<DontDestroy>();
                        userFunction.controllerName = listControllerInArray[cpt];
                        //newCharacter.gameObject.SetActive(false);
                        cpt++;
                    }
                    SceneManager.LoadScene("MapSelection");
                }
            }
        }

    }

    private void ChooseCapacity(string controller)
    {
        cptCapacity++;

        listControllerUsed.Add(controller);
        listControllerToChooseCapacity.Remove(controller);

        listImageUsed.Add(listImage[hashUser[controller].cptCapacity]);

        hashText[controller].enabled = true;
        hashText[controller].text = "Appuyer sur start";

        foreach (Image image in hashUserImages[controller])
        {
            if (image.name == "Capacity")
            {
                image.sprite = validation.sprite;
            }
            else if (image.name.Contains("Arrow"))
            {
                image.enabled = false;
            }
        }
    }

    //Change image if other user are on a new selected capacity
    private void OtherCapacity(string controller, string[] listControllerInArray)
    {
        foreach (string controller2 in listControllerInArray)
        {
            if (controller != controller2)
            {
                if (hashUser[controller].cptCapacity == hashUser[controller2].cptCapacity)
                {
                    hashUser[controller2].cptCapacity = (hashUser[controller2].cptCapacity + 1) % nbCapacity;

                    foreach (Image image in hashUserImages[controller2])
                    {
                        if (image.name == "Capacity")
                        {
                            image.sprite = listImage[hashUser[controller2].cptCapacity].sprite;
                        }
                    }
                }
            }
        }
    }

    private bool TestIfImageAvailable (int i)
    {
        Image[] listImageInArray = listImageUsed.ToArray();
        return !listImageInArray.Contains(listImage[i]);
    }

    private void ChangeImage(string controller)
    {
        foreach (Image image in hashUserImages[controller])
        {
            if (image.name == "Capacity")
            {
                image.sprite = listImage[hashUser[controller].cptCapacity].sprite;
            }
        }
    }

    IEnumerator DoAction(float delay, string controller)
    {
        hashIsActing[controller] = true;
        yield return new WaitForSeconds(delay);
        hashIsActing[controller] = false;
    }
}
