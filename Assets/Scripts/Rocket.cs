using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rocketRigidBody;
    
    void Start()
    {
        rocketRigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MovementInput();
    }

    private void MovementInput()
    {
        if(Input.GetKey(KeyCode.Space)) //can thrust while rotating
        {
            rocketRigidBody.AddRelativeForce(Vector3.up);
        }
        if (Input.GetKey(KeyCode.A)) //can only rotate in one direction at a time
        {
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward);
        }
    }
}
