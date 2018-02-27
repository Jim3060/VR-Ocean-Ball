using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDetector : MonoBehaviour {

    public GameObject player;

    private bool isTriggered;
    private bool inTheZone;
    private Rigidbody rb;


	// Use this for initialization
	void Start () {
        isTriggered = false;
        inTheZone = false;
        rb = player.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isTriggered && inTheZone) {
            Vector3 tmpV = rb.velocity;
            tmpV.x -= 6;
            rb.velocity = tmpV;
        }
	}

    void OnTriggerEnter(Collider collider)
    {
        isTriggered = true;
    }

    void enterAccZone() {
        inTheZone = true;
        Debug.Log("enter the acc zone");
    }

    void exitAccZone() {
        inTheZone = false;
        Debug.Log("exit the acc zone");
    }

}
