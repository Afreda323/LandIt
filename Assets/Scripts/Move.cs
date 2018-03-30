using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    [SerializeField] Vector3 moveTo = new Vector3(10f, 10, 10f);
    [SerializeField] float duration = 2f;

    Vector3 startingPos;

	// Use this for initialization
	void Start () {
        startingPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (duration <= Mathf.Epsilon) { return; }

        float cycles = Time.time / duration;

        float tau = Mathf.PI * 2f;
        float movement = Mathf.Sin(tau * cycles) / 2 + .5f;


        Vector3 offset = moveTo * movement;

        transform.position = startingPos + offset;
	}
}
