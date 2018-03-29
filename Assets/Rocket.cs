using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rocketBody;

	// Use this for initialization
	void Start () {
        rocketBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        HandleKey();
	}

    private void HandleKey () {
        // Directions
        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward);
        } else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(-Vector3.forward);
        } 

        // Thrust
        if (Input.GetKey(KeyCode.Space)) {
            rocketBody.AddRelativeForce(Vector3.up);
        }
    }
}
