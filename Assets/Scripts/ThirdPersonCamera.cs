using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {
    public float rotateSpeed = 7.0f;
    public float verticalRotateSpeed = 0.1f;

    private float VERTICAL_THRESHOLD = 0.5f;

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

        // Get the Vertical Axes of the mouse to rotate the camera
        float vertical = -Input.GetAxis("Mouse Y") * verticalRotateSpeed;

        if (transform.localPosition.y + vertical <= VERTICAL_THRESHOLD && transform.localPosition.y + vertical >= -VERTICAL_THRESHOLD)
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + vertical, transform.localPosition.z);

        // Make the camera look at the target
        transform.LookAt(target.transform);
    }
}
