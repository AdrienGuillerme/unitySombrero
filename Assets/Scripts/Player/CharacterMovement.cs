
﻿using UnityEngine;

public class CharacterMovement: MonoBehaviour
{

    public float speed = 6f;            // The speed that the player will move at.
    public float speedRotation = 6f;

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.
    private string controllerName ;
    PlayerHealth myHealth;              // Reference to my own health function

    Vector3 rotationAxe = new Vector3(0, 1, 0);
    private void Start()
    {
        DontDestroy parentFunction = GetComponentInParent<DontDestroy>();
        controllerName = parentFunction.controllerName;
    }

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        myHealth = GetComponentInParent<PlayerHealth>();
        anim = GetComponentInParent<Animator>();
        playerRigidbody = GetComponentInParent<Rigidbody>();
        enabled = true;
    }


    void FixedUpdate()
    {
        float h;
        float v;
        if (controllerName == "Keyboard")
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
            Turning();
        }
        else
        {
            h= Input.GetAxisRaw(controllerName + "LStickX");
            v = Input.GetAxisRaw(controllerName + "LStickY");
            if ((Mathf.Abs(Input.GetAxisRaw(controllerName + "RStickX"))>0.2)||(Mathf.Abs(Input.GetAxisRaw(controllerName + "RStickY")) > 0.2))
            {
                Quaternion stickAngle = Quaternion.AxisAngle(rotationAxe, Mathf.Atan2(Input.GetAxisRaw(controllerName + "RStickX"), Input.GetAxisRaw(controllerName + "RStickY")));
                Turning(stickAngle);
            }
        }
        Move(h, v);
        Animating(h, v);
    }

    void Move(float h, float v)
    {
        if(enabled)
        {
            movement.Set(h, 0f, v);
            movement = movement.normalized * speed * Time.deltaTime;
            playerRigidbody.MovePosition(transform.position + movement);
        }
    }

    void Turning(Quaternion stickAngle)
    {
        // Set the player's rotation to this new rotation.
        //playerRigidbody.MoveRotation(stickAngle);
        playerRigidbody.rotation = Quaternion.Slerp(playerRigidbody.rotation, stickAngle, Time.deltaTime * speedRotation);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        anim.SetFloat("speed", Mathf.Abs(h) + Mathf.Abs(v));
    }
}
