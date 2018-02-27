using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLifeManager : MonoBehaviour {

    public GameObject ball;
    public int status; // Status of ball's life:  0--dead;  1--live;  2--pause;

    private Vector3 lastCkptPos;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        lastCkptPos = ball.transform.position;
        rb = ball.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (ball.transform.position.y <= -12) { // play's position too low and touch the magma
            die();
        }

        if (status == 0) {
            returnCkpt();
            revive();
        }

        
	}

    private void die() {
        Debug.Log("the player died\n");
        status = 0;
    }

    private void revive() {
        status = 1;
    }

    private void pause() {
        status = 2;
    }

    private void unpause() {
        if (status == 2)
            status = 1;
    }

    private void reachCkpt() {
        lastCkptPos = ball.transform.position;
    }

    private void returnCkpt() {
        Vector3 tmp = lastCkptPos;
        tmp.y += 10;
        ball.transform.position = tmp;
        rb.velocity = Vector3.zero;
    }
}
