using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    Rigidbody            playerRigidBody;
    GameObject           playerSword;

    public float movementConstant = 5.0f;

    // Sword Plunge Constants
    static Quaternion    SWORD_PLUNGE_ROTATION = Quaternion.Euler(90.0f, 0.0f, 0.0f);

    // Sword Grab Constants
    static Quaternion    SWORD_HOLD_ROTATION   = Quaternion.Euler(145.0f, 0.0f, 0.0f);
    static Vector3       SWORD_HOLD_POSITION   = new Vector3(0.065f, 0.055f, -0.03f);

    // Use this for initialization
    void Start () {
        playerRigidBody = this.gameObject.GetComponent<Rigidbody>();
        playerSword = GameObject.Find("SwordPlayer1");
    }
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate () {
        HandlePlayerMovement();

        HandleSwordPlunge();

        //HandlePlayerMouseClick();
    }

    // Read movement input of the player: {w, a, s, d}
    void HandlePlayerMovement() {
        Vector3 currentVelocity = playerRigidBody.velocity;
        
        // Move Away
        if (Input.GetKey("d")) {
            playerRigidBody.AddForce(new Vector3(0.0f, 0.0f, 1.0f) * movementConstant, ForceMode.Impulse);
        }
        // Move Left
        if (Input.GetKey("w")) {
            playerRigidBody.AddForce(new Vector3(-1.0f, 0.0f, 0.0f) * movementConstant, ForceMode.Impulse);
        }
        // Move Towards
        if (Input.GetKey("a")) {
            playerRigidBody.AddForce(new Vector3(0.0f, 0.0f, -1.0f) * movementConstant, ForceMode.Impulse);
        }
        // Move Right
        if (Input.GetKey("s")) {
            playerRigidBody.AddForce(new Vector3(1.0f, 0.0f, 0.0f) * movementConstant, ForceMode.Impulse);
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
    }

    // Sword Plunge: 'q', Sword Pickup: 'e'
    void HandleSwordPlunge() {
        if (Input.GetKeyDown("q") && playerSword.transform.parent != null) {
            // Detach the sword from the player
            playerSword.transform.parent = null;

            // Adjust the orientation of the sword
            playerSword.transform.rotation = SWORD_PLUNGE_ROTATION;
        }
        else if (Input.GetKeyDown("e") && playerSword.transform.parent == null) {
            // Verify that the player is within distance of the sword
            if (IsObjectClose(this.gameObject, playerSword)) {
                // Attach the sword to the player
                playerSword.transform.parent = this.gameObject.transform;

                // Adjust the orientation and local position of the sword
                playerSword.transform.rotation = SWORD_HOLD_ROTATION;
                playerSword.transform.localPosition = SWORD_HOLD_POSITION;
            }
        }
    }

    bool IsObjectClose(GameObject object1, GameObject object2) {
        if (Mathf.Abs(object1.transform.position.x - object2.transform.position.x) <= 0.5f 
            && Mathf.Abs(object1.transform.position.z - object2.transform.position.z) <= 0.5f) {
            return true;
        }

        return false;
    }


    void HandlePlayerMouseClick() {
        
    }

}
