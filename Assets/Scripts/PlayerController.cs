using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 5.0F;
	public float rotationSpeed = 300.0F;
	public float rotateIntervall;
    public float scrollValue;
    [HideInInspector]
	public bool gameEnded = false;
	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;
	private PlayerHealth playerHealth;
	private Quaternion destRotation;

	void Start() {
		playerHealth = GetComponent<PlayerHealth>();
		controller = GetComponent<CharacterController>();
		destRotation = transform.rotation;
	}
	
	void Update() {
		
		if (playerHealth.health > 0 && !gameEnded){
			moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
			moveDirection = transform.TransformDirection (moveDirection);
			moveDirection = moveDirection * moveSpeed;
			controller.SimpleMove (moveDirection);
            
            if (Input.GetKey (KeyCode.Q))
				destRotation.eulerAngles -= new Vector3 (0, rotateIntervall, 0);
		
			if (Input.GetKey (KeyCode.E))
				destRotation.eulerAngles += new Vector3 (0, rotateIntervall, 0);
            
            destRotation.eulerAngles -= new Vector3(Input.GetAxis("Mouse ScrollWheel") * scrollValue, 0, 0);

                float step = rotationSpeed * Time.deltaTime;
			transform.rotation = Quaternion.RotateTowards (transform.rotation, destRotation, step);
		}
	}

}
	
