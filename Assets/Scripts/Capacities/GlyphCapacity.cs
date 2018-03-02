using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlyphCapacity : MonoBehaviour, ICapacity {

    // Use this for initialization
    float timeStamp;
    public float distanceToCharacter = 5;

    void Start()
    {

    }

    void Update()
    {
    }

    public void ActivateCapacity()
    {
        MakeGlyph();
    }

    private void MakeGlyph()
    {
        GameObject obj = Resources.Load("Glyph") as GameObject;
         Vector3 position = transform.position;
        float orbeAltitude = this.gameObject.GetComponent<LauncherCapacityBehaviour>().GetAltitude();
        Debug.Log("orbe : " + orbeAltitude);
        position.y -= orbeAltitude;
        Instantiate(obj, position, transform.rotation);
    }
}
