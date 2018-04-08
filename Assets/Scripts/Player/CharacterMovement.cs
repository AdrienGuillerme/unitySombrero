
﻿using UnityEngine;
using System.Collections;

public class CharacterMovement: MonoBehaviour
{

    public float speed = 6f;            // The speed that the player will move at.
    public float speedRotation = 6f;
    private float fixSpeed = 6f;

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.
    private string controllerName ;
    PlayerHealth myHealth;              // Reference to my own health function
    Vector3 playerDirection;
    UnityEngine.AI.NavMeshAgent agent;
    float speedWeight = 1;
    float lerpSpeed = 8;

    Vector3 rotationAxe = new Vector3(0, 1, 0);
    private void Start()
    {
        DontDestroy parentFunction = GetComponentInParent<DontDestroy>();
        controllerName = parentFunction.controllerName;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        fixSpeed = speed;
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
            if(Mathf.Abs(h) < 0.2)
            {
                h = 0;
            }

            v = Input.GetAxisRaw(controllerName + "LStickY");
            if (Mathf.Abs(v) < 0.2)
            {
                v = 0;
            }

            if ((Mathf.Abs(Input.GetAxisRaw(controllerName + "RStickX"))>0.2)||(Mathf.Abs(Input.GetAxisRaw(controllerName + "RStickY")) > 0.2))
            {
                Quaternion stickAngle = Quaternion.AxisAngle(rotationAxe, Mathf.Atan2(Input.GetAxisRaw(controllerName + "RStickX"), Input.GetAxisRaw(controllerName + "RStickY")));
                Turning(stickAngle);
            }
            //Move();
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

    void Move()
    {
        float h, v;
        Transform cameratransform = Camera.main.transform;
        cameratransform.rotation = Quaternion.LookRotation(new Vector3(transform.position.x - cameratransform.position.x, 0, transform.position.z - cameratransform.position.z));
        Vector3 forward = cameratransform.TransformDirection(Vector3.forward);
        Vector3 right = cameratransform.TransformDirection(Vector3.right);

        h = Input.GetAxisRaw(controllerName + "LStickX");
        if (Mathf.Abs(h) < 0.2)
        {
            h = 0;
        }
        else
        {
            if (h < 0)
                h = -1;
            else
                h = 1;
        }

        v = Input.GetAxisRaw(controllerName + "LStickY");
        if (Mathf.Abs(v) < 0.2)
        {
            v = 0;
        }
        else
        {
            if (v < 0)
                v = -1;
            else
                v = 1;
        }

        playerDirection = h * right + v * forward;
        speed = playerDirection.magnitude;


        if (speedWeight < 1.0f)
        {
            speedWeight += Time.deltaTime * 2.0f;
        }
        else
        {
            speedWeight = 1.0f;
        }

        speed = speed * speedWeight;
        if (speed > 0)
        {
            if (playerDirection != Vector3.zero)
            {
                Quaternion playerRotation = Quaternion.LookRotation(playerDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, playerRotation, Time.deltaTime * lerpSpeed);
            }
            agent.Move(transform.forward * Time.deltaTime * speed * 3.0f);
        }
        anim.SetFloat("speed", speed);
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

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    public void setSpeed(float speed, float time)
    {
        StartCoroutine(Freeze(speed, time));
    }

    public float getFixSpeed() { return fixSpeed; }

    IEnumerator Freeze(float speed, float time)
    {
        if(this.speed != 0)
        {
            // we freeze
            float oldSpeed = this.speed;
            float oldRotationSpeed = this.speedRotation;
            this.speed = speed;
            this.speedRotation = speed;
            this.anim.enabled = false;

            yield return new WaitForSeconds(time);
            // we unfreeze
            this.anim.enabled = true;
            this.speed = oldSpeed;
            this.speedRotation = oldRotationSpeed;
        }
        
    }
}
