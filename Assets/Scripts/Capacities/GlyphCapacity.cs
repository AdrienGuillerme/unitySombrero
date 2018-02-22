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
        Instantiate(obj, position, transform.rotation);
    }
}
