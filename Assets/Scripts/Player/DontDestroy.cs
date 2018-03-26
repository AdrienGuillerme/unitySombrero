using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontDestroy : MonoBehaviour {

    public string controllerName;
    public Text uiText;


    private long score;
    public int cptCapacity;
   

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        score = 0;
    }

    public void AddScore(int i)
    {
        score += i;
        uiText.text = "Score : " + score.ToString();
    }

    public void SubstractScore(int i)
    {
        score -= i;
        uiText.text = "Score : " + score.ToString();
    }
}
