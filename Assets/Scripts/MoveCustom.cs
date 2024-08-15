using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCustom : MonoBehaviour
{
    // A developer-only bool for if we should be in third person or not
    public bool thirdPerson;

    public Transform firstPersonCameraTransform;
    public Transform thirdPersonCameraTransform;

    //how fast we should move when walking
    public float walkSpeed;
    //how fast we should move while running
    public float runSpeed;
    //how fast we should move while crouching
    public float crouchSpeed;
    //how strong our jump should be
    public float jumpPower;
    //how strong gravity should be
    public float gravity;

    //to store how fast we should move this frame
    public float speedThisFrame;

    //to store what our left-right-up-down inputs are this frame
    public Vector2 inputThisFrame;

    //to store our overall movement this frame
    public Vector3 movementThisFrame;

    //to store a reference to the object's rigidbody
    public Rigidbody rb;

    //define which layers are considered solid ground
    public LayerMask groundedMask;

    // Start is called before the first frame update
    void Start()
    {
        //get the rigidbody component and save it in the variable
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //get our inputs this frame
        inputThisFrame.x = Input.GetAxis("Horizontal");
        inputThisFrame.y = Input.GetAxis("Vertical");

        //reset our potential movement to 0, 0, 0
        movementThisFrame = Vector3.zero;

        //apply our new input direction right/left and forward/back
        movementThisFrame.x = inputThisFrame.x;
        movementThisFrame.z = inputThisFrame.y;

        //figure out what our speed should be this frame
        speedThisFrame = walkSpeed;
        //if the "sprint" button is being held
        if (Input.GetButton("Sprint"))
        {
            speedThisFrame = runSpeed;
        }

        //if the "crouch" button is being held
        if (Input.GetButton("Crouch"))
        {
            speedThisFrame = crouchSpeed;
        }

        //multiply movement this frame by speed this frame
        movementThisFrame *= speedThisFrame;

        //recall the up/down speed we were at from the rigidbody, and apply gravity
        movementThisFrame.y = rb.velocity.y - gravity * Time.deltaTime;

        //check if we're on the ground
        if (IsGrounded())
        {
            //if we press the Jump button
            if (Input.GetButton("Jump"))
            {
                movementThisFrame.y = jumpPower;
            }
        }
        
        //call our Move function
        Move(movementThisFrame);
    }

    private void Move(Vector3 movement)
    {
        if (thirdPerson)
        {
            movement = thirdPersonCameraTransform.TransformDirection(movement);
            Vector3 facingDirection = new Vector3(movement.x, 0, movement.z);
            if(Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d")){
                transform.forward = facingDirection;
            }
        } 
        else // First person
        {
            // Match the left-right rotation of the camera
            transform.localEulerAngles = new Vector3(0, firstPersonCameraTransform.localEulerAngles.y, 0);

            // Take our "global" movement direction, and convert it to a local direction
            movement = transform.TransformDirection(movement);
        }

        //set our rigidbody's velocity using the incoming movement value
        rb.velocity = movement;
    }

    private bool IsGrounded()
    {
        //return the result of a raycast (true or false)
        return Physics.Raycast(transform.position, Vector3.down, 1.2f, groundedMask);
    }
}
