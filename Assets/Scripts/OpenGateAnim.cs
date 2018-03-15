using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGateAnim : MonoBehaviour {

    public GameObject gateCam;
    public GameObject mainCam;

    private Animation doorAnim;

    private int temp = 100;
	// Use this for initialization
	void Start () {

        doorAnim = GetComponent<Animation>();
    }
	
	// Update is called once per frame
	void Update () {

        //StartCoroutine(CloseGate());

	}

    IEnumerator OpenGate()
    {
        
        mainCam.SetActive(false);
        gateCam.SetActive(true);
        doorAnim.Play("OpenGate");
        yield return new WaitForSeconds(1.5f);
        gateCam.SetActive(false);
        mainCam.SetActive(true);
    }

    IEnumerator CloseGate()
    {

        mainCam.SetActive(false);
        gateCam.SetActive(true);
        doorAnim.Play("CloseGate");
        yield return new WaitForSeconds(1.5f);
        gateCam.SetActive(false);
        mainCam.SetActive(true);
    }
}
