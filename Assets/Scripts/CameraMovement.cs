using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject playerRocket; //The object the camera will follow
    [SerializeField] private Vector3 distanceFromRocket; //Camera's distance from the object

    private void LateUpdate()
    {
        Vector3 positionToGo = playerRocket.transform.position + distanceFromRocket; //Target position of the Camera
        Vector3 smoothPosition = Vector3.Lerp(a: transform.position, b: positionToGo, t: 0.125f); // Smooth position of the Camera
        transform.position = smoothPosition;
        transform.LookAt(playerRocket.transform.position); // Camera will look to the object
    }
}
