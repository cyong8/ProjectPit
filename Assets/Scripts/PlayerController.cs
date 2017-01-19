﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    Rigidbody            playerRigidBody;
    GameObject           playerSword;
    private Transform    playerCam;
    private Vector3      playerCamForward;

    /********** MOVEMENT VARIABLES **********/
    public  float        movementConstant = 0.15f;
    private Vector3      movement;

    /********** CONSTANTS **********/
    static Quaternion    SWORD_PLUNGE_ROTATION = Quaternion.Euler(90.0f, 0.0f, 0.0f);
    static Quaternion    SWORD_HOLD_ROTATION   = Quaternion.Euler(145.0f, 0.0f, 0.0f);
    static Vector3       SWORD_HOLD_POSITION   = new Vector3(0.065f, 0.055f, -0.03f);


	// Use this for initialization
	void Start () {
        playerRigidBody = this.gameObject.GetComponent<Rigidbody>();

        playerSword = GameObject.Find("SwordPlayer1");

        if (Camera.main != null)
            playerCam = Camera.main.transform;
    }
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate () {

        HandleSwordPlunge();

        //HandlePlayerMouseClick();

        HandlePlayerMovement();
    }

    void HandlePlayerMovement() {

        // read inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // calculate move direction to pass to character
        if (playerCam != null) {
            // calculate camera relative direction to move:
            playerCamForward = Vector3.Scale(playerCam.forward, new Vector3(1, 0, 1)).normalized;
            movement = v * playerCamForward + h * playerCam.right;
        }

        playerRigidBody.MovePosition(transform.position + (movement * movementConstant));
    }
    
    // Sword Plunge: 'q', Sword Pickup: 'e'
    void HandleSwordPlunge() {
        if (Input.GetKeyDown(KeyCode.Q) && playerSword.transform.parent != null) {
            // Detach the sword from the player
            playerSword.transform.parent = null;

            // Adjust the orientation of the sword
            playerSword.transform.rotation = SWORD_PLUNGE_ROTATION;
        }
        else if (Input.GetKeyDown(KeyCode.E) && playerSword.transform.parent == null) {
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
        if (Mathf.Abs(object1.transform.position.x - object2.transform.position.x) <= 1.0f 
            && Mathf.Abs(object1.transform.position.z - object2.transform.position.z) <= 1.0f) {
            return true;
        }

        return false;
    }


    void HandlePlayerMouseClick() {
        
    }

}    
