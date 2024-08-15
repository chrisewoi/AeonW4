using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithForce : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("up"))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.forward * speed);
        }

        if (Input.GetKey("down"))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.back * speed);
        }

        if (Input.GetKey("left"))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.left * speed);
        }

        if (Input.GetKey("right"))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.right * speed);
        }
    }
}
