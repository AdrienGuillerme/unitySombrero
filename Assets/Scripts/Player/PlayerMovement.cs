using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 6f;            // The speed that the player will move at.
    public string controllerName = "Joy1";

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    string state;                       // The current state of the character (dead, resurrecting a mate...).
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.

    Vector3 rotationAxe = new Vector3(0, 1, 0);

    void Awake()
    {
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");

        // Set up references.
        anim = GetComponentInParent<Animator>();
        playerRigidbody = GetComponentInParent<Rigidbody>();

        // Initiate some stuffs.
        state = "movable";
    }


    void FixedUpdate()
    {
        // Store the input axes.
        float h;
        float v;
        if (controllerName == "Keyboard")
        {
            // Store the input axes.
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");

            // Turn the player to face the mouse cursor.
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

        // Move the player around the scene.
        Move(h, v);

        // Animate the player.
        Animating(h, v);
    }

    void Move(float h, float v)
    {
        if(state == "movable")
        {
            // Set the movement vector based on the axis input.
            movement.Set(h, 0f, v);

            // Normalise the movement vector and make it proportional to the speed per second.
            movement = movement.normalized * speed * Time.deltaTime;

            // Move the player to it's current position plus the movement.
            playerRigidbody.MovePosition(transform.position + movement);
        }
    }

    void Turning(Quaternion stickAngle)
    {
        // Set the player's rotation to this new rotation.
        playerRigidbody.MoveRotation(stickAngle);
    }

    void Turning()
    {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        if (state == "dead")
            anim.SetBool("Die", true);
        else
        {
            // Create a boolean that is true if either of the input axes is non-zero.
            bool walking = h != 0f || v != 0f;

            // Tell the animator whether or not the player is walking.
            anim.SetBool("IsWalking", walking);
        }
    }
}
