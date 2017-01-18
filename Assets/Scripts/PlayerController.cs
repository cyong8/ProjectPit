using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float movementConstant = 6.0f;
    Rigidbody playerRigidBody;

    Vector3 movement;

	// Use this for initialization
	void Start () {
        playerRigidBody = this.gameObject.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate () {
        //handlePlayerInput();
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        handlePlayerInput(h, v);
    }

    void handlePlayerInput (float h, float v) {
        movement.Set(h, 0f, v);
        movement = movement.normalized * movementConstant * Time.deltaTime;
        playerRigidBody.MovePosition(transform.position + movement);
    }
}


/*
 Vector3 currentVelocity = playerRigidBody.velocity;
        
        // Move Away
        if (Input.GetKey("w")) {
            playerRigidBody.AddForce(new Vector3(0.0f, 0.0f, 1.0f) * movementConstant, ForceMode.Impulse);
            print("Input w!");
        }
        // Move Left
        if (Input.GetKey("a")) {
            playerRigidBody.AddForce(new Vector3(-1.0f, 0.0f, 0.0f) * movementConstant, ForceMode.Impulse);
            print("Input a!");
        }
        // Move Towards
        if (Input.GetKey("s")) {
            playerRigidBody.AddForce(new Vector3(0.0f, 0.0f, -1.0f) * movementConstant, ForceMode.Impulse);
            print("Input s!");
        }
        // Move Right
        if (Input.GetKey("d")) {
            playerRigidBody.AddForce(new Vector3(1.0f, 0.0f, 0.0f) * movementConstant, ForceMode.Impulse);
            print("Input d!");
        }

        // Limit Positive z
        if (currentVelocity.z > movementConstant)
            playerRigidBody.velocity.Set(currentVelocity.x, currentVelocity.y, movementConstant);
        // Limit Negative x
        if (currentVelocity.x < -movementConstant)
            playerRigidBody.velocity.Set(-movementConstant, currentVelocity.y, currentVelocity.z);
        // Limit Negative z
        if (currentVelocity.z < -movementConstant)
            playerRigidBody.velocity.Set(currentVelocity.x, currentVelocity.y, -movementConstant);
        // Limit Positive x
        if (currentVelocity.x > movementConstant)
            playerRigidBody.velocity.Set(movementConstant, currentVelocity.y, currentVelocity.z);
     
     
     */
