using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {
    public float rotateSpeed = 5;
    private GameObject target;


    void Start() {
        target = GameObject.Find("Player1");
    }

    void LateUpdate() {
        // Get the Horizontal axis of the mouse to rotate the target
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.transform.Rotate(0, horizontal, 0);

        // Match the rotation of the camera with the rotation of the target
        transform.rotation = target.transform.rotation;

        // Make the camera look at the target
        transform.LookAt(target.transform);
    }
}
