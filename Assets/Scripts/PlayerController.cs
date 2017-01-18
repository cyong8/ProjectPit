using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    Rigidbody            playerRigidBody;
    GameObject           playerSword;

    public float movementConstant = 6.0f;

    // Sword Plunge Constants
    static Quaternion    SWORD_PLUNGE_ROTATION = Quaternion.Euler(90.0f, 0.0f, 0.0f);

    // Sword Grab Constants
    static Quaternion    SWORD_HOLD_ROTATION   = Quaternion.Euler(145.0f, 0.0f, 0.0f);
    static Vector3       SWORD_HOLD_POSITION   = new Vector3(0.065f, 0.055f, -0.03f);

    Vector3 movement;

	// Use this for initialization
	void Start () {
        playerRigidBody = this.gameObject.GetComponent<Rigidbody>();
        playerSword = GameObject.Find("SwordPlayer1");
    }
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate () {

        HandleSwordPlunge();

        //HandlePlayerMouseClick();
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        handlePlayerInput(h, v);
    }

    void handlePlayerInput (float h, float v) {
        movement.Set(h, 0f, v);
        movement = movement.normalized * movementConstant * Time.deltaTime;
        playerRigidBody.MovePosition(transform.position + movement);
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
