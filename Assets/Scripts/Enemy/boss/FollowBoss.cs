using UnityEngine;
using System.Collections;

public class FollowBoss : MonoBehaviour
{
	public Vector3 offset;			// The offset at which the Health Bar follows the player.
	
	private Transform player;		// Reference to the player.

	private Camera camera;


	void Awake ()
	{
		// Setting up the reference.
		//player = GameObject.FindGameObjectWithTag("Boss").transform;
		camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera> ();
	}

	void Update ()
	{
		// Set the position to the player's position with the offset.
		//transform.position = player.position + offset;
		transform.LookAt (camera.transform.position);
	}
}
