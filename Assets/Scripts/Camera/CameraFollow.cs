using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraFollow : MonoBehaviour
{
    public float smoothing = 5f;        // The speed with which the camera will be following.

    int nbPlayer;
    GameObject[] listPlayer;

    Vector3 offset;                     // The initial offset from the target.

    void Start()
    {
        // Get the list of player
        listPlayer = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] listCharacter = GameObject.FindGameObjectsWithTag("CharacterGroup");
        Vector3 target = new Vector3(0,0,0);

        foreach (GameObject charcater in listCharacter)
        {
            charcater.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        foreach (GameObject player in listPlayer)
        {
            target += player.transform.position;
            /*player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;*/
        }
        nbPlayer = listPlayer.Length;
        

        // Calculate the initial offset.
        offset = transform.position - target / nbPlayer;

    }

    void FixedUpdate()
    {
       
        // Create a postion the camera is aiming for based on the offset from the target.
        Vector3 targetCamPos= new Vector3(0,0,0);
         Vector3 lastPlayer = targetCamPos;
         Vector3 newPlayer;
         int notYet = 0;
         foreach (GameObject player in listPlayer)
         {
             newPlayer = player.transform.position;
            targetCamPos += newPlayer;
             if(targetCamPos == newPlayer)
             {
                 lastPlayer = newPlayer;
             }
             else
             {
                 if (Mathf.Max(Mathf.Abs(lastPlayer.x- newPlayer.x),Mathf.Abs(lastPlayer.z- newPlayer.z)) > 15)
                 {
                     notYet = 1;

                 }
                 else { lastPlayer = newPlayer; }
             }
         }
       
        if (notYet != 1)
        {
            targetCamPos = targetCamPos / nbPlayer + offset;

            // Smoothly interpolate between the camera's current position and it's target position.
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }
       
}