using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float accelerationThrust = 10f;
    
    Rigidbody rocketRigidBody;
    AudioSource thrustSound;
    
    void Start()
    {
        rocketRigidBody = GetComponent<Rigidbody>();
        thrustSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        Thrust();
        Rotate();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Safe": //do nothing. Safe place to land Rocket.
                print("Safe Landing Place!");
                break;
            default: //trigger Crash sequence. NOT SAFE TO LAND.
                print("Unsafe Landing Place!");
                break;
        }
    }

    private void Rotate()
    {
        float rotationSpeed = rcsThrust * Time.deltaTime; 
        
        rocketRigidBody.freezeRotation = true; //takes control of physics
        
        if (Input.GetKey(KeyCode.A)) //can only rotate in one direction at a time
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }

        rocketRigidBody.freezeRotation = false; //release control of physics
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space)) //can thrust while rotating
        {
            float verticalSpeed = accelerationThrust * Time.deltaTime;
            
            rocketRigidBody.AddRelativeForce(Vector3.up * verticalSpeed);
            if (thrustSound.isPlaying == false) //prevents audio from playing on top of itself
            {
                thrustSound.Play();
            }
        }
        else //stops audio when Thrust key is released
        {
            thrustSound.Stop();
        }
    }
}
