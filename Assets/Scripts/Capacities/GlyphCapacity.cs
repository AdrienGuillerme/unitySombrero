using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlyphCapacity : MonoBehaviour {

    // Use this for initialization
    bool inCooldown = false;
    bool inUse = false;
    float timeStamp;
    
    string controllerName;

    private void Start()
    {
        controllerName = GetComponentInParent<DontDestroy>().controllerName;
    }

    private void Update()
    {
        MakeGlyph();
    }

    private void MakeGlyph()
    {
        GameObject obj = Resources.Load("Glyph") as GameObject;
        Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Instantiate(obj, position, transform.rotation);
    }
}
