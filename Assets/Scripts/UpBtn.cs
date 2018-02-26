using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpBtn : MonoBehaviour {

    public GameObject elevator;
    public float speed = 1;
    private bool trigged = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (trigged && elevator.transform.position.y <= 29.5)
        {
            elevator.transform.Translate(elevator.transform.up * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("enter");
        trigged = true;
    }

    void OnTriggerExit(Collider collider) {
        Debug.Log("quit");
        trigged = false;
    }

}
