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
        string controllerName = gameObject.GetComponent<LauncherCapacityBehaviour>().GetControllerName();
        position.y -= orbeAltitude;
        GameObject glyphObject = Instantiate(obj, position, transform.rotation);
        glyphObject.GetComponent<GlyphBehaviour>().SetControllerName(controllerName);
    }
}
