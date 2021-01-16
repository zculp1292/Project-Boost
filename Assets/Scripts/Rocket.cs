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
    GameStatus gameStatus;

    string rocketStatus = "Safe";
    string nextLevel;
    int hullIntegrity;
    bool rocketFlyable = true;
    
    void Start()
    {
        rocketRigidBody = GetComponent<Rigidbody>();
        thrustSound = GetComponent<AudioSource>();
        gameStatus = FindObjectOfType<GameStatus>();

        hullIntegrity = CheckHullIntegrity();
        print(hullIntegrity);
    }

    void Update()
    {
        IsRocketStillFlyable();
        
        if (rocketFlyable == true)
        {
            Thrust();
            Rotate();
        }
        else
        {
            nextLevel = "Restart";
            gameStatus.LevelLoader(nextLevel);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != rocketStatus)
        {
            if (rocketFlyable != true)
            {
                return;
            }
            
            switch (collision.gameObject.tag)
            {
                case "Safe": //do nothing. Safe place to land Rocket.
                    print("Safe Landing Place!");
                    rocketStatus = collision.gameObject.tag;
                    break;
                case "Finish":
                    print("You Win!"); //Level Win Condition
                    rocketStatus = collision.gameObject.tag;
                    nextLevel = "Next";
                    gameStatus.LevelLoader(nextLevel);
                    break;
                case "Wall":
                    print("You hit a Wall!! Ship taking damage!!");
                    gameStatus.ShipStatus(collision.gameObject.tag);  // todo: remove and rework once basic functionality is established
                    hullIntegrity = CheckHullIntegrity();
                    rocketStatus = collision.gameObject.tag;
                    print(hullIntegrity);
                    print(rocketFlyable);
                    break;
                case "Ground":
                    print("You hit the ground!! Ship taking damage!!");
                    gameStatus.ShipStatus(collision.gameObject.tag);  // todo: remove and rework once basic functionality is established
                    hullIntegrity = CheckHullIntegrity();
                    rocketStatus = collision.gameObject.tag;
                    print(hullIntegrity);
                    print(rocketFlyable);
                    break;
                default:
                    print("Unsafe Landing Place!");
                    rocketStatus = "Unsafe";
                    break;
            }
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
            rocketStatus = "Safe";
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

    private int CheckHullIntegrity()
    {
       return gameStatus.HullIntegrityCheck();
    }

    private void IsRocketStillFlyable()
    {
        if (hullIntegrity <= 0)
        {
            rocketFlyable = false;
            thrustSound.Stop();
        }
    }

}
