using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithCharacterController : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float crouchSpeed;
    public float gravity;
    public float jumpPower;

    public Vector3 moveDirection;

    public Vector2 input;

    // Update is called once per frame
    void Update()
    {
        //get inputs for this frame
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        //apply those inputs to our horizontal plane
        moveDirection.x = input.x * walkSpeed;
        moveDirection.z = input.y * walkSpeed;

        //check for running
        if (Input.GetKey("left shift"))
        {
            moveDirection.x = input.x * runSpeed;
            moveDirection.z = input.y * runSpeed;
        }

        //check for crouching
        if (Input.GetKey("left ctrl"))
        {
            moveDirection.x = input.x * crouchSpeed;
            moveDirection.z = input.y * crouchSpeed;
        }

        //apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        //if our character controller detects ground below it
        if (GetComponent<CharacterController>().isGrounded)
        {
            //clamp our vertical movement so we're not constantly "falling"
            moveDirection.y = Mathf.Clamp(moveDirection.y, -gravity, float.PositiveInfinity);

            //if the player presses jump...
            if (Input.GetKeyDown("space"))
            {
                //they should jump
                moveDirection.y = jumpPower;
            }
        }
        //move based on moveDirection, using time
        GetComponent<CharacterController>().Move(moveDirection * Time.deltaTime);
    }
}
