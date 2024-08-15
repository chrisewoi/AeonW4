using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithPosition : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up"))
        {
            transform.position += Vector3.forward * Time.deltaTime * speed;
        }

        if (Input.GetKey("down"))
        {
            transform.position += Vector3.back * Time.deltaTime * speed;
        }

        if (Input.GetKey("left"))
        {
            transform.position += Vector3.left * Time.deltaTime * speed;
        }

        if (Input.GetKey("right"))
        {
            transform.position += Vector3.right * Time.deltaTime * speed;
        }
    }
}
